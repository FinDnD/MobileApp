using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Models;
using MobileApp.Models.DTOs;
using MobileApp.Models.ViewModels;
using MobileApp.ViewModels;
using MobileApp.Views.Navigation;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public string APIRoute = $"{App.ApiUrl}/Account/Login";

        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();

        }

        /// <summary>
        /// Event handler for Login button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void UserLogIn(object sender, EventArgs e)
        {
            var result = await SendLoginToAPI();

            if (result.IsSuccessStatusCode)
            {
                await DisplayAlert("Login", "Login was successful", "OK");
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                // Failed Login Logic
            }

        }

        /// <summary>
        /// Send login information to the API
        /// </summary>
        /// <param name="loginInfo">Login information</param>
        /// <returns>Task of completion with HttpResponseMessage from Login</returns>
        public async Task<HttpResponseMessage> SendLoginToAPI()
        {
            LoginVM loginInfo = new LoginVM
            {
                Username = Entry_Name.Text,
                Password = Entry_Password.Text
            };

            HttpClient client = new HttpClient();

            var loginInfoJson = JsonConvert.SerializeObject(loginInfo);

            HttpContent content = new StringContent(loginInfoJson);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var loginPostResponse = await client.PostAsync(APIRoute, content);

            if (loginPostResponse.IsSuccessStatusCode)
            {
                string rawToken = await loginPostResponse.Content.ReadAsStringAsync();

                AccountInfo responseInfo = JsonConvert.DeserializeObject<AccountInfo>(rawToken);

                App.UserToken = responseInfo.Jwt;

                var profileRequest = await HandleGettingProfile(responseInfo);

                if (profileRequest)
                {
                    await DisplayAlert("Login", "Login was successful", "OK");
                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    await DisplayAlert("Profile Not Found", "Error fetching profile", "X");
                }
            }
            await DisplayAlert("Login Failed", "Invalid Username or Password", "X");
            return loginPostResponse;
        }

        /// <summary>
        /// Handle getting a user's profile once they've logged in
        /// </summary>
        /// <param name="responseInfo">Response info from the Login Post Request</param>
        /// <returns>Task of completion for Profile handling</returns>
        public async Task<bool> HandleGettingProfile(AccountInfo responseInfo)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseInfo.Jwt);

            var playerRequest = await client.GetAsync($"{App.ApiUrl}/Players/UserId/{responseInfo.UserId}");

            if (playerRequest.StatusCode == HttpStatusCode.OK)
            {
                string rawPlayer = await playerRequest.Content.ReadAsStringAsync();
                App.CurrentPlayer = JsonConvert.DeserializeObject<PlayerDTO>(rawPlayer);
                App.CurrentDM = null;
                return true;
            }
            else
            {
                var dmRequest = await client.GetAsync($"{App.ApiUrl}/DungeonMasters/UserId/{responseInfo.UserId}");
                if (dmRequest.IsSuccessStatusCode)
                {
                    string rawDM = await dmRequest.Content.ReadAsStringAsync();
                    App.CurrentDM = JsonConvert.DeserializeObject<DungeonMasterDTO>(rawDM);
                    App.CurrentPlayer = null;
                    return true;
                }
            };
            return false;
        }


        void UserSignUp(object sender, EventArgs e)
        {
            DisplayAlert("Register", "You will be redirected to the Register Page", "OK");
            Application.Current.MainPage = new NavigationPage(new AboutPage());
        }
    }
}