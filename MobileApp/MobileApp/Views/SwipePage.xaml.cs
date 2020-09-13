using MLToolkit.Forms.SwipeCardView.Core;
using MobileApp.Models;
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
                if (App.CurrentPlayer.ActiveRequests[0].DungeonMaster.CampaignName == "Campaign Sample") App.CurrentPlayer.ActiveRequests.RemoveAt(0);

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
        /// <param name="e">Event details for the Swipe including current item and direction</param>
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
        /// Handle logic for right swipes or "accepted", sends a request to the Web Service with the new request after setting the Accepted value for the current user to True
        /// </summary>
        /// <param name="request">Request that the swipe occurred on</param>
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
                await HandleUpdatingProfile();
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

        /// <summary>
        /// Update a user's profile by sending a new request for their information to the Web Service, used to check for any updated requests or party changes after the user's initial login/registration
        /// </summary>
        /// <returns>Task of completion</returns>
        public async Task HandleUpdatingProfile()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.UserToken);

            if (App.CurrentPlayer != null)
            {
                HttpResponseMessage playerRequest = await client.GetAsync($"{App.ApiUrl}/Players/UserId/{App.UserId}");
                if (playerRequest.IsSuccessStatusCode)
                {
                    string rawPlayer = await playerRequest.Content.ReadAsStringAsync();
                    App.CurrentPlayer = JsonConvert.DeserializeObject<PlayerDTO>(rawPlayer);
                }
            }
            else
            {
                HttpResponseMessage dmRequest = await client.GetAsync($"{App.ApiUrl}/DungeonMasters/UserId/{App.UserId}");
                if (dmRequest.IsSuccessStatusCode)
                {
                    string rawDM = await dmRequest.Content.ReadAsStringAsync();
                    App.CurrentDM = JsonConvert.DeserializeObject<DungeonMasterDTO>(rawDM);
                }
            };
        }

        /// <summary>
        /// Handle swiping left or rejecting the current profile, sets the Active value to false and sends a request to update the request in the database
        /// </summary>
        /// <param name="request">Request that the swipe occurred on</param>
        /// <returns>Task of completion</returns>
        private async Task HandleSwipeLeft(RequestDTO request)
        {
            request.Active = false;
            await PutRequest(request);
        }

        /// <summary>
        /// Send the supplied request to the Web Service
        /// </summary>
        /// <param name="request">Updated request to be sent</param>
        /// <returns>Task of completion with response message from Web Service</returns>
        private async Task<HttpResponseMessage> PutRequest(RequestDTO request)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.UserToken);

            string requestString = JsonConvert.SerializeObject(request);

            HttpContent content = new StringContent(requestString);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await client.PutAsync($"{APIRoute}/{request.Id}", content);

            return response;
        }

        /// <summary>
        /// Invoke a left swipe when Dislike button is clicked
        /// </summary>
        /// <param name="sender">Generic Sender object for Event handler</param>
        /// <param name="e">Generic Event args for Event handler</param>
        private void OnDislikeClicked(object sender, EventArgs e)
        {
            SwipeCard.InvokeSwipe(SwipeCardDirection.Left);
        }

        /// <summary>
        /// Invoke a right swipe when Like button is clicked
        /// </summary>
        /// <param name="sender">Generic Sender object for Event handler</param>
        /// <param name="e">Generic Event args for Event handler</param>
        private void OnLikeClicked(object sender, EventArgs e)
        {
            SwipeCard.InvokeSwipe(SwipeCardDirection.Right);
        }

        /// <summary>
        /// When a user clicks the info button this method displays the current Profiles information
        /// </summary>
        /// <param name="sender">Generic Sender object for Event handler</param>
        /// <param name="e">Generic Event args for Event handler</param>
        private async void OnInfoClicked(object sender, EventArgs e)
        {
            if (_Requests.Any())
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
        }
    }
}