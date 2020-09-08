using MLToolkit.Forms.SwipeCardView.Core;
using MobileApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            // If the Current User has a Player assigned to them, the Requests will be built off of the DungeonMasters in their ActiveRequests list
            if (App.CurrentPlayer != null)
            {
                foreach (RequestDTO request in App.CurrentPlayer.ActiveRequests)
                {
                    _Profiles.Add(new UserProfile
                    {
                        ProfileType = "Dungeon Master",
                        UserName = request.DungeonMaster.UserName,
                        ExperienceLevel = request.DungeonMaster.ExperienceLevel
                    });
                }
                _Requests = App.CurrentPlayer.ActiveRequests;
            }
            // If the Current User has a Dungeon Master assigned to them, the Requests will be built off of the Players in their ActiveRequests list
            else
            {
                foreach (RequestDTO request in App.CurrentDM.ActiveRequests)
                {
                    _Profiles.Add(new UserProfile
                    {
                        ProfileType = "Player",
                        UserName = request.Player.UserName,
                        ExperienceLevel = request.Player.ExperienceLevel
                    });
                }
                _Requests = App.CurrentDM.ActiveRequests;
            }
        }

        //public Label DMCardLabel()
        //{
        //    Label label = new Label
        //    {
        //        FontSize = 21,
        //        FontAttributes = FontAttributes.Bold,
        //        TextColor = Color.WhiteSmoke
        //    };
        //    var formattedString = new FormattedString();
        //    // Add Inner content
        //    formattedString.Spans.Add(new Span
        //    {

        //    });
        //    return label;
        //}


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
            UserProfile profile = (UserProfile)e.Item;
            RequestDTO request = _Requests.Where(x => x.DungeonMaster.UserName == profile.UserName || x.Player.UserName == profile.UserName).FirstOrDefault();

            switch (e.Direction)
            {
                case SwipeCardDirection.None:
                    break;
                case SwipeCardDirection.Right:
                    // await HandleSwipeRight(request);
                    break;
                case SwipeCardDirection.Left:
                    // await HandleSwipeLeft(request);
                    break;
                case SwipeCardDirection.Up:
                    // Go to Info View for user?
                    break;
            }
        }

        /// <summary>
        /// Handle logic for right swipes or "accepted"
        /// </summary>
        /// <param name="request">Request that the swipe occured on</param>
        private async void HandleSwipeRight(RequestDTO request)
        {
            if (App.CurrentPlayer != null)
            {
                request.PlayerAccepted = true;
            }
            else
            {
                request.DungeonMasterAccepted = true;
            }

            // Post request to API
            // LOGIC

            if (request.PlayerAccepted && request.DungeonMasterAccepted)
            {
                // Handle Match
            }
        }

        private async void HandleSwipeLeft(RequestDTO request)
        {
            request.Active = false;
            // Post request to API
        }

        private async void HandleSwipeUp(RequestDTO request)
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

        private void OnInfoClicked(object sender, EventArgs e)
        {
            SwipeCard.InvokeSwipe(SwipeCardDirection.Up);
        }

        public class UserProfile
        {
            public string ProfileType { get; set; }
            public string UserName { get; set; }
            public string ExperienceLevel { get; set; }
            public string Image { get; set; }
        }
    }
}