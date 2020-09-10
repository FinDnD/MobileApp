using MobileApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartyPlayerDetailPage : ContentPage
    {
        public PartyPlayerDetailPage(PartyPlayerDTO partyPlayer)
        {
            InitializeComponent();
            Name.Text = partyPlayer.CharacterName;
            Race.Text = partyPlayer.Race;
        }
    }
}