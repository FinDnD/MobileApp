using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Espresso401_MobileApp.Services;
using Espresso401_MobileApp.Views;

namespace Espresso401_MobileApp
{
    public partial class App : Application
    {

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
