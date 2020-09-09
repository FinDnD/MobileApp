using MobileApp.Models.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MobileApp.Models.Enums;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerCreationPage : ContentPage
    {
        private string PlayerApiUrl = $"{App.ApiUrl}/DungeonMasters";
        private string ImageApiUrl = $"{App.ApiUrl}/UserImages";


        public PlayerCreationPage()
        {
            InitializeComponent();
        }

        public async Task OnSubmit()
        {
            PlayerDTO newPlayer = new PlayerDTO
            {
                CharacterName = characterNameEntry.Text,
                Class = ((Class)SelectedClass.SelectedItem).ToString(),
                Race = ((Race)SelectedRace.SelectedItem).ToString(),
                ExperienceLevel = ((ExperienceLevel)SelectedExperience.SelectedItem).ToString(),
                GoodAlignment = (int)GoodAlignment.Value,
                LawAlignment = (int)LawfulAlignment.Value
            };

            HttpClient client = new HttpClient();

            string playerJson = JsonConvert.SerializeObject(newPlayer);

            HttpContent content = new StringContent(playerJson);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.UserToken);

            HttpResponseMessage result = await client.PostAsync(PlayerApiUrl, content);

            if (result.IsSuccessStatusCode)
            {
                // Upload Image to Blob


                await DisplayAlert("Success!", "Player created successfully, get out there and find a party!", "OK");
                Application.Current.MainPage = new AppShell();
            }
        }
    }
}