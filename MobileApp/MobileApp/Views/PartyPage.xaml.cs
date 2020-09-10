using MobileApp.Models.DTOs;
using MobileApp.ViewModels;
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
    public partial class PartyPage : ContentPage
    {
      
        List<PartyPlayerDTO> PartyMembers;
        public string Campaign{ get; set; }

        public PartyPage()
        {
            InitializeComponent();
            MyPartyMembers();

            LabelCampaign.Text = "Attack on Titans";
         
          
        }
        void MyPartyMembers()
        {
           
            PartyMembers = new List<PartyPlayerDTO>();
            PartyMembers.Add(new PartyPlayerDTO
            {
                CharacterName = "Mortae",
                Race = "Alien",
            });
            PartyMembers.Add(new PartyPlayerDTO
            {
                CharacterName = "Lantae",
                Race = "Alien",
            });
            PartyMembers.Add(new PartyPlayerDTO
            {
                CharacterName = "Mort",
                Race = "Rip",
            });
            PartyMembers.Add(new PartyPlayerDTO
            {
                CharacterName = "tae",
                Race = "Dip",
            });
            PartyMembers.Add(new PartyPlayerDTO
            {
                CharacterName = "Draco",
                Race = "Nymph",
            });

            MyParty.ItemsSource = PartyMembers;

           
        }

        async void PlayerMemberInfo(object sender, ItemTappedEventArgs e)
        {

            if (e.Item != null)
            {
                DisplayAlert("Member Selected", "Do you want to view member's details", "Yes", "No");
                PartyPlayerDTO P = (PartyPlayerDTO)e.Item;
                await Navigation.PushAsync(new PartyPlayerDetailPage(P));
            }
            ((ListView)sender).SelectedItem = null;
        }
    }
}