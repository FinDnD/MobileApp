using Microsoft.WindowsAzure.Storage;
using MobileApp.Models;
using MobileApp.Models.DTOs;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MobileApp.Models.Enums;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DMCreationPage : ContentPage
    {
        private string DMApiUrl = $"{App.ApiUrl}/DungeonMasters";

        private MediaFile _imageUpload;

        public ObservableCollection<ExperienceLevel> _ExperienceList;




        public DMCreationPage()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
            Shell.SetNavBarIsVisible(this, false);
            _ExperienceList = new ObservableCollection<ExperienceLevel>(Enum.GetValues(typeof(ExperienceLevel)).OfType<ExperienceLevel>().ToList());
            BindingContext = this;
        }

        /// <summary>
        /// Used for Binding Experience Levels to Front End
        /// </summary>
        public ObservableCollection<ExperienceLevel> ExperienceList
        {
            get => _ExperienceList;
            set
            {
                _ExperienceList = value;
            }
        }

        /// <summary>
        /// Much of this page was inspired by : https://www.c-sharpcorner.com/article/xamarin-forms-upload-image-to-blob-storage/
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void btnSelectPic_Clicked(object sender, EventArgs e)
        {
            PermissionStatus status = await Permissions.RequestAsync<Permissions.Photos>();

            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Error", "This is not support on your device.", "OK");
                return;
            }
            else
            {
                PickMediaOptions mediaOption = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                };
                _imageUpload = await CrossMedia.Current.PickPhotoAsync();
                if (_imageUpload == null) return;

                // TODO: Show the User their image before submitting?
                // imageView.Source = ImageSource.FromStream(() => _imageUpload.GetStream());
            }
        }

        public async Task<string> SendImageToAPI(string username)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.UserToken);

            Stream imageStream = _imageUpload.GetStream();

            HttpContent content = new StreamContent(imageStream);

            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "file",
                FileName = $"{username}(TempImage)"
            };

            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            using (var contentAsFormData = new MultipartFormDataContent())
            {
                contentAsFormData.Add(content);
                var response = await client.PostAsync($"{App.ApiUrl}/UserImages", contentAsFormData);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                return "";
            }
        }

        private async void OnSubmit(object sender, EventArgs e)
        {
            if (_imageUpload == null)
            {
                await DisplayAlert("Upload Image", "We need an image of your character!", "X");
                return;
            };
            Busy();

            var imageUploadResult = await SendImageToAPI(App.UserName);

            if (imageUploadResult != "")
            {
                DungeonMasterDTO newDm = new DungeonMasterDTO
                {
                    CampaignName = campaignNameEntry.Text,
                    CampaignDesc = CampaignDescription.Text,
                    ExperienceLevel = ((ExperienceLevel)
                    SelectedExperience.SelectedItem).ToString(),
                    PersonalBio = PersonalBio.Text,
                    ImageUrl = imageUploadResult
                };
                HttpClient client = new HttpClient();


                string dmJson = JsonConvert.SerializeObject(newDm);

                HttpContent content = new StringContent(dmJson);

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.UserToken);

                HttpResponseMessage result = await client.PostAsync(DMApiUrl, content);

                NotBusy();

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var dmString = await result.Content.ReadAsStringAsync();
                    var dm = JsonConvert.DeserializeObject<DungeonMasterDTO>(dmString);
                    App.CurrentDM = dm;
                    App.CurrentPlayer = null;
                    //await Shell.Current.GoToAsync("//NavTabBar/SwipeTab/SwipePage");

                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    await DisplayAlert("Oh No!", "Something went wrong creating your DM. Please try again.", "OK");
                }
            }
            else
            {
                NotBusy();
                await DisplayAlert("Oh No!", "Something went wrong uploading your image. Please try again.", "OK");
            }
        }


        public void Busy()
        {
            uploadIndicator.IsVisible = true;
            uploadIndicator.IsRunning = true;
            ButtonSelectPic.IsEnabled = false;
            SubmitButton.IsEnabled = false;
        }

        public void NotBusy()
        {
            uploadIndicator.IsVisible = false;
            uploadIndicator.IsRunning = false;
            ButtonSelectPic.IsEnabled = true;
            SubmitButton.IsEnabled = true;
        }
    }
}