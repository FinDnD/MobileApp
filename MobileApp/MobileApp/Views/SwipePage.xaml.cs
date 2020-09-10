using MLToolkit.Forms.SwipeCardView.Core;
using MobileApp.Models.DTOs;
using MobileApp.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SwipePage : ContentPage
    {
        public List<UserProfile> _Profiles = new List<UserProfile>();
        public List<RequestDTO> _Requests = new List<RequestDTO>();
        public string APIRoute = $"{App.ApiUrl}/Swipes";

        public ICommand SwipedCommand { get; }


        public SwipePage()
        {
            InitializeComponent();
            CardBinding();
            SwipedCommand = new Command<SwipedCardEventArgs>(OnSwiped);
            BindingContext = this;
        }

        /// <summary>
        /// Bind the Cards that the user will see
        /// </summary>
        public void CardBinding()
        {
            // If the Current User has a Player assigned to them, the Profiles will be built off of the DungeonMasters in their ActiveRequests list
            if (App.CurrentPlayer != null)
            {
                foreach (RequestDTO request in App.CurrentPlayer.ActiveRequests)
                {
                    _Profiles.Add(new UserProfile
                    {
                        ProfileType = "Dungeon Master",
                        UserName = request.DungeonMaster.UserName,
                        ExperienceLevel = request.DungeonMaster.ExperienceLevel,
                        Image = request.DungeonMaster.ImageUrl
                    });
                }
                _Requests = App.CurrentPlayer.ActiveRequests;
            }
            
            // If the Current User has a Dungeon Master assigned to them, the Profiles will be built off of the Players in their ActiveRequests list
            else
            {
                foreach (RequestDTO request in App.CurrentDM.ActiveRequests)
                {
                    _Profiles.Add(new UserProfile
                    {
                        ProfileType = "Player",
                        UserName = request.Player.UserName,
                        ExperienceLevel = request.Player.ExperienceLevel,
                        Image = request.Player.ImageUrl
                    });
                }
                _Requests = App.CurrentDM.ActiveRequests;
            }
        }

        /// <summary>
        /// Used for Binding Profiles to Front End
        /// </summary>
        public List<UserProfile> Profiles
        {
            get => _Profiles;
            set
            {
                _Profiles = value;
            }
        }

        /// <summary>
        /// Handle the Swiping Event when it occurs
        /// </summary>
        /// <param name="e">Event details for the Swipe </param>
        public async void OnSwiped(SwipedCardEventArgs e)
        {
            var request = _Requests[0];
            _Requests.RemoveAt(0);
            switch (e.Direction)
            {
                case SwipeCardDirection.None:
                    break;
                case SwipeCardDirection.Right:
                    await HandleSwipeRight(request);
                    break;
                case SwipeCardDirection.Left:
                    await HandleSwipeLeft(request);
                    break;
                case SwipeCardDirection.Up:
                    await HandleSwipeUp(request);
                    break;
            }
        }

        /// <summary>
        /// Handle logic for right swipes or "accepted"
        /// </summary>
        /// <param name="request">Request that the swipe occured on</param>
        private async Task HandleSwipeRight(RequestDTO request)
        {
            if (App.CurrentPlayer != null)
            {
                request.PlayerAccepted = true;
            }
            else
            {
                request.DungeonMasterAccepted = true;
            }

            var response = await PutRequest(request);

            if (request.PlayerAccepted && request.DungeonMasterAccepted)
            {
                if(App.CurrentPlayer != null)
                {
                    await DisplayAlert("Matched!", "A whole new party awaits!", "X");
                }
                else
                {
                    await DisplayAlert("Matched!", "A new player has joined your party!", "X");
                }
            }
        }

        private async Task HandleSwipeLeft(RequestDTO request)
        {
            request.Active = false;
            await PutRequest(request);
        }

        private async Task<HttpResponseMessage> PutRequest(RequestDTO request)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.UserToken);

            string requestString = JsonConvert.SerializeObject(request);

            HttpContent content = new StringContent(requestString);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await client.PutAsync(APIRoute, content);

            return response;
        }


        private async Task HandleSwipeUp(RequestDTO request)
        {
            if (App.CurrentPlayer != null)
            {
                DungeonMasterDTO dm = request.DungeonMaster;
                await DisplayAlert("Dungeon Master Info", $"Campaign Name: {dm.CampaignName}\nDescription:\n{dm.CampaignDesc}\nAbout Me:\n{dm.PersonalBio}", "Back to Swiping");
            }
            else
            {
                PlayerDTO player = request.Player;
                await DisplayAlert("Player Info", $"Name: {player.CharacterName}\nClass: {player.Class}\nRace: {player.Race}\nGood: {player.GoodAlignment}%\nLawful: {player.LawAlignment}%", "Back to Swiping");
            }
        }


        private void OnDislikeClicked(object sender, EventArgs e)
        {
            SwipeCard.InvokeSwipe(SwipeCardDirection.Left);
        }


        private void OnLikeClicked(object sender, EventArgs e)
        {
            SwipeCard.InvokeSwipe(SwipeCardDirection.Right);
        }

        /// <summary>
        /// When a user clicks the info button this method displays the current Profiles information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnInfoClicked(object sender, EventArgs e)
        {
            RequestDTO request = _Requests[0];
            if (App.CurrentPlayer != null)
            {
                DungeonMasterDTO dm = request.DungeonMaster;
                await DisplayAlert("Dungeon Master Info", $"Campaign Name: {dm.CampaignName}\nDescription:\n{dm.CampaignDesc}\nAbout Me:\n{dm.PersonalBio}", "Back to Swiping");
            }
            else
            {
                PlayerDTO player = request.Player;
                await DisplayAlert("Player Info", $"Name: {player.CharacterName}\nClass: {player.Class}\nRace: {player.Race}\nGood: {player.GoodAlignment}%\nLawful: {player.LawAlignment}%", "Back to Swiping");
            }
        }

        /// <summary>
        /// Logs the user out and sets all App Info to null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logout(object sender, EventArgs e)
        {
            App.CurrentDM = null;
            App.CurrentPlayer = null;
            App.UserToken = null;
            App.UserName = null;
            App.UserId = null;
            Application.Current.MainPage = new LoginPage();
        }

    }
}