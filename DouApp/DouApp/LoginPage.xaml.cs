using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DouApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private string mockUsername = "User";
        private string mockPassword = "1234";

        public LoginPage()
        {
            InitializeComponent();
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
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;
            int id = App.Users.GetUserID(username, password);

            if (id == 0)
            {
                DisplayAlert("Wrong credintials", "The entered Username or Password does not exist in database", "OK");
                return;
            }

            App.UserID = id;
            Navigation.PushAsync(new TabbedMainPage());
        }
    }
}