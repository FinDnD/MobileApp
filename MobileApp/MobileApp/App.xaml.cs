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
        public static PlayerDTO CurrentPlayer;
        public static DungeonMasterDTO CurrentDM;
        public static string UserToken;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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
