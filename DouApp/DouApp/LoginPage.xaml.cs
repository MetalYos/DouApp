using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DouApp.Interfaces;
using DouApp.Models;

namespace DouApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            forgotPassLabel.GestureRecognizers.Add(
                new TapGestureRecognizer()
                {
                    Command = new Command(() =>
                    {
                        Navigation.PushAsync(new ForgotPasswordPage());
                    })
                });
        }

        private async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {

            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            // Check if username is empty or not
            if (username == string.Empty || username == null)
            {
                await DisplayAlert("Error", "Enter a username", "OK");
                return;
            }

            // Check if password is empty or not
            if (password == string.Empty || password == null)
            {
                await DisplayAlert("Error", "Enter a password", "OK");
                return;
            }

            // Get if username exists
            bool usernameExists = App.Users.IsUsernameExists(username);
            
            // Check if he has any login attempts left
            if (usernameExists && App.Current.Properties.ContainsKey(username))
            {
                if ((int)App.Current.Properties[username] == 0)
                {
                    await DisplayAlert("Error", "You have reached your maximum login attemps, use \"Forgot Password?\" link to reset password.", "OK");
                    return;
                }
            }

            // Get User ID from database
            int id = App.Users.GetUserID(username, password);
            if (id == 0)
            {
                await DisplayAlert("Wrong credintials", "The entered Username or Password are incorrect", "OK");

                // Save the remaining number of times the user can login
                if (usernameExists)
                {
                    if (!App.Current.Properties.ContainsKey(username))
                        App.Current.Properties[username] = (App.MaxLoginTries - 1);
                    else
                    {
                        int count = (int)App.Current.Properties[username];
                        App.Current.Properties[username] = (count - 1);
                    }
                    await App.Current.SavePropertiesAsync();
                }

                return;
            }

            // Set user id
            App.UserID = id;

            // Load Containers
            App.Containers.LoadContainers();

            // Connect to Bluetooth
            string deviceName = App.Current.Properties["DeviceName"].ToString();
            bool tryAgain = true;
            while (tryAgain)
            {
                if (!await ConnectToBluetoothDevice(deviceName, 30))
                {
                    string answer = await DisplayActionSheet("Couldn't connect to " + deviceName + "! Try again?",
                        "Cancel", null, "Yes", "No");
                    if (answer == "Yes") tryAgain = true;
                    else if (answer == "No") tryAgain = false;
                    else return;
                }
                else
                    tryAgain = false;
            }

            // Move to main page
            var tabbedPage = new TabbedMainPage()
            {
                BarBackgroundColor = Color.FromHex("#002060")
            };
            var page = new NavigationPage(tabbedPage)
            {
                BarBackgroundColor = Color.FromHex("#002060")
            };
            Application.Current.MainPage = page;
        }

        private async Task<bool> ConnectToBluetoothDevice(string name, int numTimes)
        {
            while (!DependencyService.Get<IBluetoothHelper>().IsConnected() && numTimes > 0)
            {
                numTimes--;
                try
                {
                    await DependencyService.Get<IBluetoothHelper>().Connect(name);
                }
                catch
                {
                    return false;
                }
            }

            if (numTimes == 0)
                return false;
            else
                return true;
        }
    }
}