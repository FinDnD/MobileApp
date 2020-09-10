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

            StackLayout stackLayout1 = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(10, 10, 0, 10),
                Padding = new Thickness(0, 20, 0, 0)
            };

            StackLayout stackLayout2 = new StackLayout
            {
                Margin = new Thickness(10, 10, 0, 10),
                Padding = new Thickness(0, 20, 0, 0)
            };

            Image imageLogo = new Image
            {
                Source = "dice.icon50.png",
                //VerticalOptions = LayoutOptions.Center
            };

            Label labelHeadline = new Label
            {
                Text = "ProfilePage.xaml.cs CreateDMContent",
                Padding = new Thickness(0, 10, 0, 0),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black
            };

            
            Label labelCampaign= new Label
            {
                Text = "Campaign Name",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(10, 10, 0, 10)
            };

            Label labelExperience = new Label
            {
                Text = "Experience Level",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(10, 10, 0, 10)
            };

            Label labelCampaignDescription = new Label
            {
                Text = "Campaign Description",
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(0, 10, 0, 0)
            };

            Editor editorCampaignDescription = new Editor
            {
                TextColor = Color.Gray,
                HeightRequest = 80,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Placeholder  = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Leo vel fringilla est ullamcorper eget nulla facilisi etiam. Vitae elementum curabitur vitae nunc sed velit dignissim sodales ut. Fusce id velit ut tortor. Cras adipiscing enim eu turpis. Non enim praesent elementum facilisis leo vel fringilla. Suspendisse potenti nullam ac tortor vitae purus. Tristique et egestas quis ipsum suspendisse ultrices gravida."
            };

            Label labelPersonalBio = new Label
            {
                Text = "Personal Bio",
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(0, 10, 0, 0)
            };

            Editor editorPersonalBio = new Editor
            {
                TextColor = Color.Gray,
                HeightRequest = 80,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Placeholder = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Leo vel fringilla est ullamcorper eget nulla facilisi etiam. Vitae elementum curabitur vitae nunc sed velit dignissim sodales ut. Fusce id velit ut tortor. Cras adipiscing enim eu turpis. Non enim praesent elementum facilisis leo vel fringilla. Suspendisse potenti nullam ac tortor vitae purus. Tristique et egestas quis ipsum suspendisse ultrices gravida."
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

            // STACK1
            stackLayout1.Children.Add(imageLogo);
            stackLayout1.Children.Add(labelHeadline);

            // STACK2
            //stackLayout2.Children.Add(imageLogo);
            //stackLayout2.Children.Add(labelHeadline);
            stackLayout2.Children.Add(labelCampaign);
            stackLayout2.Children.Add(labelExperience);
            stackLayout2.Children.Add(labelCampaignDescription);
            stackLayout2.Children.Add(editorCampaignDescription);
            stackLayout2.Children.Add(labelPersonalBio);
            stackLayout2.Children.Add(editorPersonalBio);
            stackLayout2.Children.Add(button);

            Grid.SetRow(stackLayout2, 1);

            ProfileGrid.Children.Add(stackLayout2);

            //Grid.SetRow(stackLayout1, 0);
            //Grid.SetRow(stackLayout2, 1);
            //Grid.SetRow((BindableObject)view, row);

            //GRID
            //grid.Children.Add(stackLayout1);
            //grid.Children.Add(stackLayout2);

            //page.Content = grid;

            //page.IconImageSource = "SettingsIcon.png";

            //return page;
        }


        public ContentPage CreatePlayerContent()
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

            StackLayout stackLayout1 = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(10, 10, 0, 10),
                Padding = new Thickness(0, 20, 0, 0)
            };

            Image imageLogo = new Image
            {
                Source = "dice.icon50.png",
                VerticalOptions = LayoutOptions.Center
            };

            Label labelHeadline = new Label
            {
                Text = "ProfilePage.xaml.cs Player!!!",
                Padding = new Thickness(0, 10, 0, 0),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.White
            };

            StackLayout stackLayout2 = new StackLayout
            {
                Margin = new Thickness(10, 10, 0, 10),
                Padding = new Thickness(0, 20, 0, 0)
            };

            Label labelCampaign = new Label
            {
                Text = "ProfilePage.xaml.cs Player!!!",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(10, 10, 0, 10)
            };

            Label labelExperience = new Label
            {
                Text = "Experience Level",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(10, 10, 0, 10)
            };

            Label labelCampaignDescription = new Label
            {
                Text = "Campaign Description",
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(0, 10, 0, 0)
            };

            Editor editorCampaignDescription = new Editor
            {
                TextColor = Color.Gray,
                HeightRequest = 80,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Placeholder = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Leo vel fringilla est ullamcorper eget nulla facilisi etiam. Vitae elementum curabitur vitae nunc sed velit dignissim sodales ut. Fusce id velit ut tortor. Cras adipiscing enim eu turpis. Non enim praesent elementum facilisis leo vel fringilla. Suspendisse potenti nullam ac tortor vitae purus. Tristique et egestas quis ipsum suspendisse ultrices gravida."
            };

            Label labelPersonalBio = new Label
            {
                Text = "Personal Bio",
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(0, 10, 0, 0)
            };

            Editor editorPersonalBio = new Editor
            {
                TextColor = Color.Gray,
                HeightRequest = 80,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Placeholder = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Leo vel fringilla est ullamcorper eget nulla facilisi etiam. Vitae elementum curabitur vitae nunc sed velit dignissim sodales ut. Fusce id velit ut tortor. Cras adipiscing enim eu turpis. Non enim praesent elementum facilisis leo vel fringilla. Suspendisse potenti nullam ac tortor vitae purus. Tristique et egestas quis ipsum suspendisse ultrices gravida."
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

            // STACK1
            stackLayout1.Children.Add(imageLogo);
            stackLayout1.Children.Add(labelHeadline);

            // STACK2
            stackLayout2.Children.Add(labelCampaign);
            stackLayout2.Children.Add(labelExperience);
            stackLayout2.Children.Add(labelCampaignDescription);
            stackLayout2.Children.Add(editorCampaignDescription);
            stackLayout2.Children.Add(labelPersonalBio);
            stackLayout2.Children.Add(editorPersonalBio);
            stackLayout2.Children.Add(button);

            //Grid.SetRow(stackLayout1, 0);
            //Grid.SetRow(stackLayout2, 1);
            //Grid.SetRow((BindableObject)view, row);

            //GRID
            grid.Children.Add(stackLayout1);
            //grid.Children.Add(stackLayout2);

            page.Content = grid;

            return page;
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