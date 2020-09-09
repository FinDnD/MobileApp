using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileApp.Services;
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

        // Globals Immutables
        // API Root
        public static string ApiUrl = "https://espresso401api.azurewebsites.net/api";
        // Localhost versions:
        // public static string ApiUrl = "https://localhost:44335/api";


        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
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
