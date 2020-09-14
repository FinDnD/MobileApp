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

        /// <summary>
        /// Event handler for submitting a registration, sends provided info to the Web Service and checks response, forwards user to Profile creation if successful.
        /// </summary>
        /// <param name="sender">Generic Sender object for Event handler</param>
        /// <param name="e">Generic Event args for Event handler</param>
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
                App.UserEmail = userEmailEntry.Text;
                NotBusy();

                if (profileType)
                {
                    await Shell.Current.GoToAsync($"DMCreationPage");
                }
                else
                {
                    await Shell.Current.GoToAsync($"PlayerCreationPage");
                }
            }
            else
            {
                NotBusy();
                await DisplayAlert("Error", "Looks like something went wrong, make sure all the info is sound!", "X");
            }
        }

        /// <summary>
        /// Event handler for the Return To Login button, sends user back to login page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void ReturnToLogin(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"LoginPage");
        }

        /// <summary>
        /// Shows activity indicator and disabled buttons
        /// </summary>
        public void Busy()
        {
            uploadIndicator.IsVisible = true;
            uploadIndicator.IsRunning = true;
            RegisterButton.IsEnabled = false;
            BackToLoginButton.IsEnabled = false;
        }

        /// <summary>
        /// Hides activity indicator and enables buttons
        /// </summary>
        public void NotBusy()
        {
            uploadIndicator.IsVisible = false;
            uploadIndicator.IsRunning = false;
            RegisterButton.IsEnabled = true;
            BackToLoginButton.IsEnabled = true;
        }
    }

}