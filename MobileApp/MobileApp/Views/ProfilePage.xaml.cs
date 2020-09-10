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

            if (App.CurrentPlayer != null)
            {
                Children.Add(CreatePlayerContent());
            }
            else
            {
                CreateDMContent();
            }

        }

        public void CreateDMContent()
        {
            ContentPage page = new ContentPage();

            Grid grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition {Height = 10},
                    new RowDefinition {Height = 80},
                    new RowDefinition {Height = 10}
                }
            };

            StackLayout stackLayout = new StackLayout
            {
                Margin = new Thickness(20, 0, 20, 0),
                Padding = new Thickness(0, 70, 0, 0)
            };
            
            Label labelCampaign= new Label
            {
                Text = $"Campaign Name: {App.CurrentDM.CampaignName}",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(10, 10, 0, 10)
            };

            Label labelExperience = new Label
            {
                Text = $"Experience Level: {App.CurrentDM.ExperienceLevel}",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(10, 10, 0, 10)
            };

            Label labelCampaignDescription = new Label
            {
                Text = "Campaign Description",
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(10, 10, 0, 0)
            };

            Label editorCampaignDescription = new Label
            {
                TextColor = Color.Gray,
                HeightRequest = 80,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Text = App.CurrentDM.CampaignDesc,
                Margin = new Thickness(10, 5, 0, 0)
            };

            Label labelPersonalBio = new Label
            {
                Text = "Personal Bio",
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(10, 10, 0, 0)
            };

            Label editorPersonalBio = new Label
            {
                TextColor = Color.Gray,
                HeightRequest = 80,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Text = App.CurrentDM.PersonalBio,
                Margin = new Thickness(10, 5, 0, 0)
            };

            Button button = new Button
            {
                Text = "DELETE CHARACTER",
                BackgroundColor = Color.FromHex("#9D0A0E"),
                TextColor = Color.White,
                HeightRequest = 50,
                VerticalOptions = LayoutOptions.Start,
                Margin = new Thickness(0, 10, 0, 0)
            };

            stackLayout.Children.Add(labelCampaign);
            stackLayout.Children.Add(labelExperience);
            stackLayout.Children.Add(labelCampaignDescription);
            stackLayout.Children.Add(editorCampaignDescription);
            stackLayout.Children.Add(labelPersonalBio);
            stackLayout.Children.Add(editorPersonalBio);
            stackLayout.Children.Add(button);

            Grid.SetRow(stackLayout, 1);

            ProfileGrid.Children.Add(stackLayout);
        }


        public void CreatePlayerContent()
        {
            ContentPage page = new ContentPage();

            Grid grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition {Height = 10},
                    new RowDefinition {Height = 80},
                    new RowDefinition {Height = 10}
                }
            };

            StackLayout stackLayout = new StackLayout
            {
                Margin = new Thickness(20, 0, 20, 0),
                Padding = new Thickness(0, 70, 0, 0)
            };

            Label labelCampaign = new Label
            {
                Text = $"Campaign Name: {App.CurrentDM.CampaignName}",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(10, 10, 0, 10)
            };

            Label labelExperience = new Label
            {
                Text = $"Experience Level: {App.CurrentDM.ExperienceLevel}",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(10, 10, 0, 10)
            };

            Label labelCampaignDescription = new Label
            {
                Text = "Campaign Description",
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(10, 10, 0, 0)
            };

            Label editorCampaignDescription = new Label
            {
                TextColor = Color.Gray,
                HeightRequest = 80,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Text = App.CurrentDM.CampaignDesc,
                Margin = new Thickness(10, 5, 0, 0)
            };

            Label labelPersonalBio = new Label
            {
                Text = "Personal Bio",
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(10, 10, 0, 0)
            };

            Label editorPersonalBio = new Label
            {
                TextColor = Color.Gray,
                HeightRequest = 80,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Text = App.CurrentDM.PersonalBio,
                Margin = new Thickness(10, 5, 0, 0)
            };

            Button button = new Button
            {
                Text = "DELETE CHARACTER",
                BackgroundColor = Color.FromHex("#9D0A0E"),
                TextColor = Color.White,
                HeightRequest = 50,
                VerticalOptions = LayoutOptions.Start,
                Margin = new Thickness(0, 10, 0, 0)
            };

            stackLayout.Children.Add(labelCampaign);
            stackLayout.Children.Add(labelExperience);
            stackLayout.Children.Add(labelCampaignDescription);
            stackLayout.Children.Add(editorCampaignDescription);
            stackLayout.Children.Add(labelPersonalBio);
            stackLayout.Children.Add(editorPersonalBio);
            stackLayout.Children.Add(button);

            Grid.SetRow(stackLayout, 1);

            ProfileGrid.Children.Add(stackLayout);
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