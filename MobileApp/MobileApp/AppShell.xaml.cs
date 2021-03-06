﻿using MobileApp.Views;
using Xamarin.Forms;

namespace MobileApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            CurrentItem.CurrentItem = SwipeTab;
        }

        /// <summary>
        /// Register non-Tab routes for the app
        /// </summary>
        private void RegisterRoutes()
        {
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));
            Routing.RegisterRoute("PlayerCreationPage", typeof(PlayerCreationPage));
            Routing.RegisterRoute("DMCreationPage", typeof(DMCreationPage));
        }
    }
}
