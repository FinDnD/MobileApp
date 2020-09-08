using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MobileApp.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
     
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();

        }

      
        async void UserLogIn(object sender , EventArgs e)
        {
            await DisplayAlert("Login","Login was successful" , "OK");
       
        
              
               Application.Current.MainPage = new AppShell();

           
        }

        void UserSignUp(object sender, EventArgs e)
        {
            DisplayAlert("Register", "You will be redirected to the Register Page", "OK");
            Application.Current.MainPage = new NavigationPage(new AboutPage());
        }
    }
}