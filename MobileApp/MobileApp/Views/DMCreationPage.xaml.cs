using MobileApp.Models.DTOs;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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



        /// <summary>
        /// Constructor function for DM Creation. Hides tab bars
        /// </summary>
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
        /// Event handler for the Upload Image button, handles getting permissions and prompting user to select a photo from their storage.
        /// Much of this page was inspired by : https://www.c-sharpcorner.com/article/xamarin-forms-upload-image-to-blob-storage/
        /// </summary>
        /// <param name="sender">Generic Sender object for Event handler</param>
        /// <param name="e">Generic Event args for Event handler</param>
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
            }
        }

        /// <summary>
        /// Handles sending the image to the API when the DM is being created.
        /// </summary>
        /// <returns>Task of completion with new image URL or empty string if upload failed</returns>
        public async Task<string> SendImageToAPI()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.UserToken);

            Stream imageStream = _imageUpload.GetStream();

            HttpContent content = new StreamContent(imageStream);

            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "file",
                FileName = $"{App.UserName}(TempImage)"
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

        /// <summary>
        /// Event handler for the CREATE CHARACTER button. Sends the provided DM information to the Web Service to be stored in the SQL database.
        /// </summary>
        /// <param name="sender">Generic Sender object for event</param>
        /// <param name="e">Generic Event args object for event</param>
        private async void OnSubmit(object sender, EventArgs e)
        {
            if (_imageUpload == null)
            {
                await DisplayAlert("Upload Image", "We need an image of your character!", "X");
                return;
            };
            Busy();

            var imageUploadResult = await SendImageToAPI();

            if (imageUploadResult != "")
            {
                DungeonMasterDTO newDm = new DungeonMasterDTO
                {
                    CampaignName = campaignNameEntry.Text,
                    CampaignDesc = CampaignDescription.Text,
                    ExperienceLevel = ((ExperienceLevel)
                    SelectedExperience.SelectedItem).ToString(),
                    PersonalBio = PersonalBio.Text,
                    ImageUrl = imageUploadResult,
                    UserEmail = App.UserEmail
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

        /// <summary>
        /// Shows activity indicator and disabled buttons
        /// </summary>
        public void Busy()
        {
            uploadIndicator.IsVisible = true;
            uploadIndicator.IsRunning = true;
            ButtonSelectPic.IsEnabled = false;
            SubmitButton.IsEnabled = false;
        }

        /// <summary>
        /// Hides activity indicator and enables buttons
        /// </summary>
        public void NotBusy()
        {
            uploadIndicator.IsVisible = false;
            uploadIndicator.IsRunning = false;
            ButtonSelectPic.IsEnabled = true;
            SubmitButton.IsEnabled = true;
        }
    }
}