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
            ContentPage newPage = new ContentPage();
            newPage.Title = "Player View";
            newPage.IconImageSource = "";
            Label label = new Label();
            Children.Add(newPage);
        }

        void OnToggled(object sender, ToggledEventArgs e)
        {
        /*    bool isToogled = e.Value;
            if (isToogled)
            {

            }*/
        }
    }
}