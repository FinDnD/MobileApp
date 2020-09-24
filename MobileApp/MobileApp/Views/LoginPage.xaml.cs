using MobileApp.Models;
using MobileApp.Models.DTOs;
using MobileApp.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public string APIRoute = $"{App.ApiUrl}/Account/Login";

        /// <summary>
        /// Constructor argument for the Login Page, sets Nav and Tab bars to be invisible
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
            Shell.SetNavBarIsVisible(this, false);
        }

        /// <summary>
        /// Event handler for Login button clicked
        /// </summary>
        /// <param name="sender">Generic Sender object for Event handler</param>
        /// <param name="e">Generic Event args for Event handler</param>
        public async void UserLogIn(object sender, EventArgs e)
        {
            Busy();
            await SendLoginToAPI();
        }

        /// <summary>
        /// Send login information to the API and check response, if successful send user to Homepage
        /// </summary>
        /// <param name="loginInfo">Login information</param>
        /// <returns>Task of completion with HttpResponseMessage from Login</returns>
        private async Task<HttpResponseMessage> SendLoginToAPI()
        {
            LoginVM loginInfo = new LoginVM
            {
                Username = Entry_Name.Text,
                Password = Entry_Password.Text
            };

            HttpClient client = new HttpClient();

            string loginInfoJson = JsonConvert.SerializeObject(loginInfo);

            HttpContent content = new StringContent(loginInfoJson);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage loginPostResponse = await client.PostAsync(APIRoute, content);

            if (loginPostResponse.IsSuccessStatusCode)
            {
                string rawToken = await loginPostResponse.Content.ReadAsStringAsync();

                AccountInfo responseInfo = JsonConvert.DeserializeObject<AccountInfo>(rawToken);

                App.UserToken = responseInfo.Jwt;

                bool profileRequest = await HandleGettingProfile(responseInfo);
                NotBusy();
                if (profileRequest)
                {
                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    await DisplayAlert("Profile Not Found", "Error fetching profile", "X");
                }
            }
            else
            {
                NotBusy();
                await DisplayAlert("Login Failed", "Invalid Username or Password", "X");
            }
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

            HttpResponseMessage playerRequest = await client.GetAsync($"{App.ApiUrl}/Players/UserId/{responseInfo.UserId}");

            if (playerRequest.StatusCode == HttpStatusCode.OK)
            {
                string rawPlayer = await playerRequest.Content.ReadAsStringAsync();
                App.CurrentPlayer = JsonConvert.DeserializeObject<PlayerDTO>(rawPlayer);
                App.CurrentDM = null;
                App.UserId = App.CurrentPlayer.UserId;
                App.UserEmail = App.CurrentPlayer.UserEmail;
                App.UserName = Entry_Name.Text;
                return true;
            }
            else
            {
                HttpResponseMessage dmRequest = await client.GetAsync($"{App.ApiUrl}/DungeonMasters/UserId/{responseInfo.UserId}");
                if (dmRequest.IsSuccessStatusCode)
                {
                    string rawDM = await dmRequest.Content.ReadAsStringAsync();
                    App.CurrentDM = JsonConvert.DeserializeObject<DungeonMasterDTO>(rawDM);
                    App.CurrentPlayer = null;
                    App.UserId = App.CurrentDM.UserId;
                    App.UserEmail = App.CurrentDM.UserEmail;
                    App.UserName = Entry_Name.Text;
                    return true;
                }
            };
            return false;
        }

        /// <summary>
        /// Send the user to the RegisterPage
        /// </summary>
        /// <param name="sender">Generic Sender object for Event handler</param>
        /// <param name="e">Generic Event args for Event handler</param>
        async void UserSignUp(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"RegisterPage");
        }

        /// <summary>
        /// Shows activity indicator and disabled buttons
        /// </summary>
        public void Busy()
        {
            uploadIndicator.IsVisible = true;
            uploadIndicator.IsRunning = true;
            Button_Login.IsEnabled = false;
            Button_CreateAccount.IsEnabled = false;
        }

        /// <summary>
        /// Hides activity indicator and enables buttons
        /// </summary>
        public void NotBusy()
        {
            uploadIndicator.IsVisible = false;
            uploadIndicator.IsRunning = false;
            Button_Login.IsEnabled = true;
            Button_CreateAccount.IsEnabled = true;
        }
    }
}