using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : TabbedPage
    {
        public ProfilePage()
        {
            InitializeComponent();

        }

        public void CreateContentPage()
        {
            var page = new ContentPage();

            var grid = new Grid() {  };

        }


        void OnToggled(object sender, ToggledEventArgs e)
        {

        }
    }
}