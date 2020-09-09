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
        List<string> members;
       

        public PartyPage()
        {
            InitializeComponent();
            MyPartyMembers();
         
          
        }

        void MyPartyMembers()
        {
            members = new List<string>();
            members.Add("one");
            members.Add("two");
            members.Add("three");
            members.Add("one");
            members.Add("two");
            members.Add("three");
            members.Add("one");
            members.Add("two");
            members.Add("three");
            MyParty.ItemsSource = members;
           
        }

        /*async void PlayerMemberInfo(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((string)sender).SelectedItem = null;
           await  Navigation.PushAsync(new PartyPlayerDetailPage());
        }*/
    }
}