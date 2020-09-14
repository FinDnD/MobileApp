using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : TabbedPage
    {
        private string DynamicPrimaryTextColor;

        public string UserName { get; set; }
        public string UserEmail { get; set; }

        /// <summary>
        /// ProfilePage constructor, binds information about the user and creates and displays content based on what kind of profile the user has
        /// </summary>
        public ProfilePage()
        {
            InitializeComponent();
            UserName = App.UserName;
            UserEmail  = App.UserEmail;

            if (App.CurrentPlayer != null)
            {
                CreatePlayerContent();
                UserImage.Source = App.CurrentPlayer.ImageUrl;
            }
            else
            {
                CreateDMContent();
                UserImage.Source = App.CurrentDM.ImageUrl;
            }
            BindingContext = this;
        }

        /// <summary>
        /// Create the content a DM sees when visiting the page and add elements as children to the stacklayout, then add the stacklayout to the page's grid.
        /// </summary>
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

            Label labelCampaign = new Label
            {
                Text = $"Campaign Name: {App.CurrentDM.CampaignName}",
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(10, 10, 0, 10)
            };
            
     
            Label labelExperience = new Label
            {
                Text = $"Experience Level: {App.CurrentDM.ExperienceLevel}",
                TextColor = Color.Gray,
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

            button.Clicked += async (sender, args) => await OnDeleteClicked(button);

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

        /// <summary>
        /// Create the content a player sees when visiting the page and add elements as children to the stacklayout, then add the stacklayout to the page's grid.
        /// </summary>
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

            Label labelCharacterName = new Label
            {
                Text = $"Character Name: {App.CurrentPlayer.CharacterName}",
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(10, 10, 0, 10)
            };
          
            Label labelClass = new Label
            {
                Text = $"Class: {App.CurrentPlayer.Class}",
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(10, 10, 0, 10)
            };

            Label labelRace = new Label
            {
                Text = $"Race: {App.CurrentPlayer.Race}",
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(10, 10, 0, 10)
            };

            Label labelExperience = new Label
            {
                Text = $"Experience Level: {App.CurrentPlayer.ExperienceLevel}",
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(10, 10, 0, 10)
            };

            Label labelGoodAlignment = new Label
            {
                Text = $"Good Alignment: {App.CurrentPlayer.GoodAlignment}",
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(10, 10, 0, 10)
            };

            Label labelLawfulAlignment = new Label
            {
                Text = $"Lawful Alignment Level: {App.CurrentPlayer.LawAlignment}",
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = new Thickness(10, 10, 0, 10)
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

            button.Clicked += async (sender, args) => await OnDeleteClicked(button);

            stackLayout.Children.Add(labelCharacterName);
            stackLayout.Children.Add(labelClass);
            stackLayout.Children.Add(labelRace);
            stackLayout.Children.Add(labelExperience);
            stackLayout.Children.Add(labelGoodAlignment);
            stackLayout.Children.Add(labelLawfulAlignment);
            stackLayout.Children.Add(button);

            Grid.SetRow(stackLayout, 1);
           
            ProfileGrid.Children.Add(stackLayout);
        }

        /// <summary>
        /// Event handler for clicking the delete button, sends a delete request to the web service for the current profile
        /// </summary>
        /// <param name="button">Button to bind click event to</param>
        /// <returns>Task of completion</returns>
        async Task OnDeleteClicked(Button button)
        {
            Busy(button);
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.UserToken);
            bool result = false;
            if (App.CurrentPlayer != null)
            {
                result = await DeletePlayer(client);
            }
            else
            {
                result = await DeleteDM(client);
            }

            NotBusy(button);
            if (result)
            {
                App.CurrentPlayer = null;
                App.CurrentDM = null;
                // True for New Profile
                // False for logout
                var choice = await DisplayAlert("Success", "Profile successfully deleted. Would you like to Logout or Create a new Profile?", "New Profile", "Logout");
                if (choice)
                {
                    // True for DM
                    // False for Player
                    bool profileType = await DisplayAlert("", "Would you like to sign up with a Dungeon Master or a Player profile?", "Dungeon Master", "Player");
                    if (profileType)
                    {
                        Application.Current.MainPage = new NavigationPage(new DMCreationPage());


                        // Application.Current.MainPage = new DMCreationPage();
                    }
                    else
                    {
                        Application.Current.MainPage = new NavigationPage(new PlayerCreationPage());

                        //await Shell.Current.GoToAsync("//PlayerCreationPage");


                        // Application.Current.MainPage = new PlayerCreationPage();
                    }
                }
                else
                {
                    App.UserToken = null;
                    App.UserName = null;
                    App.UserId = null;
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
            }
            else
            {
                await DisplayAlert("Oh No!", "Something went wrong deleting your Profile. Please try again.", "OK");
            }
        }


        /// <summary>
        /// Handles deleting a Player profile
        /// </summary>
        /// <param name="client">HttpClient for API request</param>
        /// <returns>Task of completion with boolean representing if the delete was successful</returns>
        public async Task<bool> DeletePlayer(HttpClient client)
        {
            HttpResponseMessage result = await client.DeleteAsync($"{App.ApiUrl}/Players");

            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Handles deleting a DM profile
        /// </summary>
        /// <param name="client">HttpClient for API request</param>
        /// <returns>Task of completion with boolean representing if the delete was successful</returns>
        public async Task<bool> DeleteDM(HttpClient client)
        {
            HttpResponseMessage result = await client.DeleteAsync($"{App.ApiUrl}/DungeonMasters");

            return result.IsSuccessStatusCode;
        }

        /// <summary>
        /// Shows activity indicator and disables buttons
        /// </summary>
        public void Busy(Button button)
        {
            uploadIndicator.IsVisible = true;
            uploadIndicator.IsRunning = true;
            button.IsEnabled = false;
        }

        /// <summary>
        /// Hides activity indicator and enables buttons
        /// </summary>
        public void NotBusy(Button button)
        {
            uploadIndicator.IsVisible = false;
            uploadIndicator.IsRunning = false;
            button.IsEnabled = true;
        }

        /// <summary>
        /// Toggle Darkmode event handler
        /// </summary>
        /// <param name="sender">Generic Sender object for Event handler</param>
        /// <param name="e">Generic Event args for Event handler</param>
        void OnToggled(object sender, ToggledEventArgs e)
        {
            bool isToogled = e.Value;
            if (isToogled)
            {
                ThemeHelper.SetDarkMode();
                App.IsLight = false;
            }
            else
            {
                ThemeHelper.SetLightMode();
                App.IsLight = true;
            }
        }



        /// <summary>
        /// Logs the user out and sets all App Info to null
        /// </summary>
        /// <param name="sender">Generic Sender object for Event handler</param>
        /// <param name="e">Generic Event args for Event handler</param>
        async void Logout(object sender, EventArgs e)
        {
            App.CurrentDM = null;
            App.CurrentPlayer = null;
            App.UserToken = null;
            App.UserName = null;
            App.UserId = null;
            App.UserEmail = null;
            await Xamarin.Essentials.SecureStorage.SetAsync("loggedIn", "0");
            await Shell.Current.GoToAsync($"LoginPage");
        }
    }
}