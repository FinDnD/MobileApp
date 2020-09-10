using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileApp.Views;
using MobileApp.Models.DTOs;

namespace MobileApp
{
    public partial class App : Application
    {
        // App-Wide Mutable Values
        // Instantiations NOT declarations
        public static PlayerDTO CurrentPlayer;
        public static DungeonMasterDTO CurrentDM;
        public static string UserToken;
        public static string UserId;
        public static string UserName;

        // Globals Immutables
        // API Root
        public static string ApiUrl = "https://espresso401api.azurewebsites.net/api";


        public App()
        {
            InitializeComponent();

            var loggedIn = Xamarin.Essentials.SecureStorage.GetAsync("isLogged").Result;
            MainPage = new AppShell();
            if (App.UserName == null || App.UserId == null)
            {
                Shell.Current.GoToAsync($"LoginPage").Wait();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
