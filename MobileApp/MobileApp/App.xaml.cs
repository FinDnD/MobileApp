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
        public static bool IsLight;

        // Globals Immutables
        // API Root
        public static string ApiUrl = "https://espresso401api.azurewebsites.net/api";


        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
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
