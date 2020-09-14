using MobileApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartyPage : ContentPage
    {
       
        public List<PartyPlayerDTO> _PartyMembers;
        public string Campaign { get; set; }
        public string CampaignImage { get; set; }

        /// <summary>
        /// Constructor for Party Page, displays info based on what user is logged in (different profile types have different places where the necessary information is stored)
        /// </summary>
        public PartyPage()
        {
            InitializeComponent();
            MyPartyMembers();

            if (App.CurrentPlayer != null)
            {
                LabelCampaign.Text = App.CurrentPlayer.Party.DungeonMasterDTO.CampaignName;
                CampaignImage = App.CurrentPlayer.Party.DungeonMasterDTO.ImageUrl;
                _PartyMembers = App.CurrentPlayer.Party.PlayersInParty;
                LabelDMName.Text = App.CurrentPlayer.Party.DungeonMasterDTO.UserName;
                LabelDMEmail.Text = App.CurrentPlayer.Party.DungeonMasterDTO.UserEmail;
            }
            else
            {
                LabelCampaign.Text = App.CurrentDM.CampaignName;
                CampaignImage = App.CurrentDM.ImageUrl;
                _PartyMembers = App.CurrentDM.Party.PlayersInParty;
                LabelDMName.Text = App.UserName;
                LabelDMEmail.Text = App.UserEmail;
            }
            BindingContext = this;
           
        }

        /// <summary>
        /// Bind party members
        /// </summary>
        public List<PartyPlayerDTO> PartyMembers
        {
            get => _PartyMembers;
            set 
            {
                _PartyMembers = value;
            }
        }

        /// <summary>
        /// Get current party for the user
        /// </summary>
        void MyPartyMembers()
        {
            if (App.CurrentPlayer != null)
            {
                _PartyMembers = App.CurrentPlayer.Party.PlayersInParty;
            }
            else
            {
                _PartyMembers = App.CurrentDM.Party.PlayersInParty;
            }
        }

        /// <summary>
        /// Send a user to a specific player's party details, activated when a user clicks a player in the party list.
        /// </summary>
        /// <param name="sender">Generic Sender object for Event handler</param>
        /// <param name="e">Generic Event args for Event handler</param>
        async void PlayerMemberInfo(object sender, ItemTappedEventArgs e)
        {

            if (e.Item != null)
            {
                bool confirmation = await DisplayAlert("", "Do you want to view member's details", "Yes", "No");
                
                if (confirmation)
                {
                    PartyPlayerDTO selectedPlayer = (PartyPlayerDTO)e.Item;
                    await Navigation.PushAsync(new PartyPlayerDetailPage(selectedPlayer));
                }
                else
                {

                    ((ListView)sender).SelectedItem = null;
                }
            }
            else return;

        }


    }
}