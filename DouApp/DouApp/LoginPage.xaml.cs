using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DouApp.Interfaces;

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

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            /*
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;
            int id = App.Users.GetUserID(username, password);

            if (id == 0)
            {
                DisplayAlert("Wrong credintials", "The entered Username or Password does not exist in database", "OK");
                return;
            }

            App.UserID = id;

            // Connect to Bluetooth
            bool connected = DependencyService.Get<IBluetoothHelper>().Connect("Sharon").Result;
            int count = 0;
            while (!connected)
            {
                connected = DependencyService.Get<IBluetoothHelper>().Connect("Sharon").Result;
                count++;
                if (count == 10)
                {
                    DisplayAlert("Connection Error", "Can't connect to bluetooth!", "OK");
                    return;
                }
            }
            */

            App.UserID = 5;
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
    }
}