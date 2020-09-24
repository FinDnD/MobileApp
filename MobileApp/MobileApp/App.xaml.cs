using MobileApp.Models.DTOs;
using Xamarin.Forms;

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
        public static string UserEmail;
        public static bool IsLight;

        // Globals Immutables
        // API Root
        public static string ApiUrl = "https://espresso401api.azurewebsites.net/api";


        public App()
        {
            InitializeComponent();

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
