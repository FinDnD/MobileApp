using MobileApp.Models.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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