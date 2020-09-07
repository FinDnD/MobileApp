using System;
using System.Collections.Generic;
using Espresso401_MobileApp.ViewModels;
using Espresso401_MobileApp.Views;
using Xamarin.Forms;

namespace Espresso401_MobileApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
