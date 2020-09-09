using MobileApp.Models.DTOs;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public partial class DMCreationPage : ContentPage
    {
        private string DMApiUrl = $"{App.ApiUrl}/DungeonMasters";
        private string ImageApiUrl = $"{App.ApiUrl}/UserImages";
        private MediaFile _imageUpload;


        public DMCreationPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSelectPic_Clicked(object sender, EventArgs e)
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

                // imageView.Source = ImageSource.FromStream(() => _imageUpload.GetStream());
                // UploadedUrl.Text = "Image URL:";
            }
        }

        public async Task OnSubmit()
        {
            DungeonMasterDTO newDm = new DungeonMasterDTO
            {
                CampaignName = campaignNameEntry.Text,
                CampaignDesc = CampaignDescription.Text,
                ExperienceLevel = ((ExperienceLevel)
                SelectedExperience.SelectedItem).ToString(),
                PersonalBio = PersonalBio.Text
            };
            HttpClient client = new HttpClient();

            string dmJson = JsonConvert.SerializeObject(newDm);

            HttpContent content = new StringContent(dmJson);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.UserToken);

            HttpResponseMessage result = await client.PostAsync(DMApiUrl, content);

            if (result.IsSuccessStatusCode)
            {
                await DisplayAlert("Success!", "Dungeon Master created successfully, start building that party!", "OK");
                Application.Current.MainPage = new AppShell();
            }
        }

    }
}