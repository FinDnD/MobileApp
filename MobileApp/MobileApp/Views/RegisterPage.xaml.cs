using MobileApp.Models;
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
            Shell.SetTabBarIsVisible(this, false);
            Shell.SetNavBarIsVisible(this, false);
        }

        async void SubmitRegistration(object sender, EventArgs e)
        {
            Busy();
            RegisterVM registrationInfo = new RegisterVM
            {
                Email = userEmailEntry.Text,
                Username = userNameEntry.Text,
                Password = userPassword.Text,
                ConfirmPassword = userPasswordConfirmed.Text
            };

            // true = Dungeon Master
            // false = Player

            bool profileType = await DisplayAlert("", "Would you like to sign up with a Dungeon Master or a Player profile?", "Dungeon Master", "Player");

            registrationInfo.ProfileType = profileType ? "DungeonMaster" : "Player";

            HttpClient client = new HttpClient();

            string registrationString = JsonConvert.SerializeObject(registrationInfo);

            HttpContent content = new StringContent(registrationString);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage registerPostResponse = await client.PostAsync(APIRoute, content);

            if (registerPostResponse.IsSuccessStatusCode)
            {
                string rawToken = await registerPostResponse.Content.ReadAsStringAsync();

                AccountInfo responseInfo = JsonConvert.DeserializeObject<AccountInfo>(rawToken);

                App.UserToken = responseInfo.Jwt;
                App.UserName = userNameEntry.Text;

                NotBusy();

                if (profileType)
                {
                    await Shell.Current.GoToAsync($"DMCreationPage");
                    //await Shell.Current.GoToAsync("//DMCreationPage");

                    // Application.Current.MainPage = new DMCreationPage();
                }
                else
                {
                    await Shell.Current.GoToAsync($"PlayerCreationPage");
                    //await Shell.Current.GoToAsync("//PlayerCreationPage");

                    // Application.Current.MainPage = new PlayerCreationPage();
                }
            }
            else
            {
                NotBusy();
                await DisplayAlert("Error", "Looks like something went wrong, make sure all the info is sound!", "X");
            }
        }


        async void ReturnToLogin(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        public void Busy()
        {
            uploadIndicator.IsVisible = true;
            uploadIndicator.IsRunning = true;
            RegisterButton.IsEnabled = false;
            BackToLoginButton.IsEnabled = false;
        }

        public void NotBusy()
        {
            uploadIndicator.IsVisible = false;
            uploadIndicator.IsRunning = false;
            RegisterButton.IsEnabled = true;
            BackToLoginButton.IsEnabled = true;
        }
    }

}