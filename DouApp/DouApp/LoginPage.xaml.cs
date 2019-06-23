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
        }

        public ICommand ClickCommand => new Command<string>((url) =>
        {
            Device.OpenUri(new System.Uri(url));
        });

        private async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {

            string username = usernameEntry.Text;
            string password = passwordEntry.Text;
            int id = App.Users.GetUserID(username, password);

            if (id == 0)
            {
                await DisplayAlert("Wrong credintials", "The entered Username or Password does not exist in database", "OK");
                return;
            }

            App.UserID = id;

            // Connect to Bluetooth
            bool tryAgain = true;
            while (tryAgain)
            {
                if (!await ConnectToBluetoothDevice(App.DeviceName, 10))
                {
                    string answer = await DisplayActionSheet("Couldn't connect to " + App.DeviceName + "! Try again?",
                        "Cancel", null, "Yes", "No");
                    if (answer == "Yes") tryAgain = true;
                    else if (answer == "No") tryAgain = false;
                    else return;
                }
            }

            //App.UserID = 5;
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
                return true;
            else
                return false;
        }
    }
}