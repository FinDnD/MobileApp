using System.ComponentModel;
using Xamarin.Forms;
using MobileApp.ViewModels;

namespace MobileApp.Views
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