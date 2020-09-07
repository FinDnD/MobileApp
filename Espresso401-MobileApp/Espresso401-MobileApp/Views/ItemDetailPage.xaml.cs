using System.ComponentModel;
using Xamarin.Forms;
using Espresso401_MobileApp.ViewModels;

namespace Espresso401_MobileApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}