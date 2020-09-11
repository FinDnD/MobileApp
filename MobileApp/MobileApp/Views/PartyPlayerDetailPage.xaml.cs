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
        public string UserImage { get; set; }

        public PartyPlayerDetailPage(PartyPlayerDTO partyPlayer)
        {
            InitializeComponent();
            UserImage = partyPlayer.ImageUrl;
            FillPage(partyPlayer);
            BindingContext = this;
        }

        public void FillPage(PartyPlayerDTO partyPlayer)
        {
            StackLayout stackLayout = new StackLayout
            {
                Margin = new Thickness(20, 0, 20, 0),
                Padding = new Thickness(0, 30, 0, 0)
            };

            Label labelCharacterName = new Label
            {
                Text = $"Character Name: {partyPlayer.CharacterName}",
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(10, 10, 0, 10)
            };

            Label labelClass = new Label
            {
                Text = $"Class: {partyPlayer.Race}",
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(10, 10, 0, 10)
            };

            Label labelRace = new Label
            {
                Text = $"Race: {partyPlayer.Race}",
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(10, 10, 0, 10)
            };

            Label labelExperience = new Label
            {
                Text = $"Experience Level: {partyPlayer.ExperienceLevel}",
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(10, 10, 0, 10)
            };
         

            stackLayout.Children.Add(labelCharacterName);
            stackLayout.Children.Add(labelClass);
            stackLayout.Children.Add(labelRace);
            stackLayout.Children.Add(labelExperience);
            ProfileStack.Children.Add(stackLayout);
        }

      
    }
}