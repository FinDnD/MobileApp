using MobileApp.Models.ViewModels;
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

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public string APIRoute = $"{App.ApiUrl}/Account/Register";

        public RegisterPage()
        {
            InitializeComponent();
        }

        public async Task SubmitRegistration()
        {
            RegisterVM registrationInfo = new RegisterVM
            {
                Email = userEmailEntry.Text,
                Username = userNameEntry.Text,
                Password = userPassword.Text
            };

            HttpClient client = new HttpClient();

            string registrationString = JsonConvert.SerializeObject(registrationInfo);

            HttpContent content = new StringContent(registrationString);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage registerPostResponse = await client.PostAsync(APIRoute, content);

            if (registerPostResponse.IsSuccessStatusCode)
            {
                // true = Dungeon Master
                // false = Player
                bool profileType = await DisplayAlert("", "Would you like to sign up as a Dungeon Master or a Player profile?", "Dungeon Master", "Player");

                if (profileType)
                {
                    Application.Current.MainPage = new DMCreationPage();
                }
                else
                {
                    Application.Current.MainPage = new PlayerCreationPage();
                }
            }
        }


        public static void ReturnToLogin()
        {
            Application.Current.MainPage = new LoginPage();
        }
    }

}