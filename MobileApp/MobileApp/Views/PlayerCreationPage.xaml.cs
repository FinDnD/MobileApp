using MobileApp.Models.DTOs;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MobileApp.Models.Enums;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerCreationPage : ContentPage
    {


        private string PlayerApiUrl = $"{App.ApiUrl}/Players";
        private string ImageApiUrl = $"{App.ApiUrl}/UserImages";

        private MediaFile _imageUpload;

        public ObservableCollection<Class> _ClassList;
        public ObservableCollection<Race> _RaceList;
        public ObservableCollection<ExperienceLevel> _ExperienceList;

        public PlayerCreationPage()
        {
            InitializeComponent();
            _ClassList = new ObservableCollection<Class>(Enum.GetValues(typeof(Class)).OfType<Class>().ToList());
            _RaceList = new ObservableCollection<Race>(Enum.GetValues(typeof(Race)).OfType<Race>().ToList());
            _ExperienceList = new ObservableCollection<ExperienceLevel>(Enum.GetValues(typeof(ExperienceLevel)).OfType<ExperienceLevel>().ToList());
            BindingContext = this;
        }

        public ObservableCollection<Class> ClassList
        {
            get => _ClassList;
            set
            {
                _ClassList = value;
            }
        }

        public ObservableCollection<Race> RaceList
        {
            get => _RaceList;
            set
            {
                _RaceList = value;
            }
        }

        public ObservableCollection<ExperienceLevel> ExperienceList
        {
            get => _ExperienceList;
            set
            {
                _ExperienceList = value;
            }
        }



        private async void OnSubmit(object sender, EventArgs e)
        {
            Busy();

            if (_imageUpload == null)
            {
                await DisplayAlert("Upload Image", "We need an image of your character!", "X");
                return;
            };

            var imageUploadResult = await SendImageToAPI(App.UserName);

            if (imageUploadResult != "")
            {

                PlayerDTO newPlayer = new PlayerDTO
                {
                    CharacterName = characterNameEntry.Text,
                    Class = ((Class)SelectedClass.SelectedItem).ToString(),
                    Race = ((Race)SelectedRace.SelectedItem).ToString(),
                    ExperienceLevel = ((ExperienceLevel)SelectedExperience.SelectedItem).ToString(),
                    GoodAlignment = (int)GoodAlignment.Value,
                    LawAlignment = (int)LawfulAlignment.Value,
                    ImageUrl = imageUploadResult
                };

                HttpClient client = new HttpClient();

                string playerJson = JsonConvert.SerializeObject(newPlayer);

                HttpContent content = new StringContent(playerJson);

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.UserToken);

                HttpResponseMessage result = await client.PostAsync(PlayerApiUrl, content);

                NotBusy();

                if (result.IsSuccessStatusCode)
                {
                    var playerString = await result.Content.ReadAsStringAsync();
                    var player = JsonConvert.DeserializeObject<PlayerDTO>(playerString);
                    App.CurrentPlayer = player;
                    App.CurrentDM = null;
                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    await DisplayAlert("Oh No!", "Something went wrong creating your character. Please try again.", "OK");
                }

            }
            else
            {
                NotBusy();
                await DisplayAlert("Oh No!", "Something went wrong uploading your image. Please try again.", "OK");
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
