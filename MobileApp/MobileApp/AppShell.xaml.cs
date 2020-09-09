using System;
using System.Collections.Generic;
using MobileApp.Views;
using Xamarin.Forms;

namespace MobileApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            this.CurrentItem.CurrentItem = SwipeTab;

        }

    }
}
