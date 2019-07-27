using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DouApp.Models;
using DouApp.BindingContexts;

namespace DouApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string email = emailEntry.Text;
            string password = passwordEntry.Text;
            string rePassword = passwordReEntry.Text;

            // Check if Username is not empty
            if (username == string.Empty || username == null)
            {
                await DisplayAlert("Username error", "Username must be entered", "OK");
                return;
            }

            // Check if Email is not empty
            if (email == string.Empty || email == null)
            {
                await DisplayAlert("Email error", "Email must be entered", "OK");
                return;
            }

            // Check that Email is valid
            try
            {
                System.Net.Mail.MailAddress address = new System.Net.Mail.MailAddress(email);
            }
            catch (FormatException)
            {
                await DisplayAlert("Email error", "Email is not valid", "OK");
                return;
            }

            // Check if Password is not empty
            if (password == string.Empty || password == null)
            {
                await DisplayAlert("Password error", "Password must be entered", "OK");
                return;
            }

            // Check password is valid
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinMaxChars = new Regex(@".{6,255}");
            if (!hasMinMaxChars.IsMatch(password))
            {
                await DisplayAlert("Password error", "Password length must be at least 6 characters long", "OK");
                return;
            }
            if (!hasUpperChar.IsMatch(password))
            {
                await DisplayAlert("Password error", "Password must contain at least one Capittal letter", "OK");
                return;
            }
            if (!hasNumber.IsMatch(password))
            {
                await DisplayAlert("Password error", "Password must contain at least one digit", "OK");
                return;
            }

            // Check if Password and re-entered password match
            if (password != rePassword)
            {
                await DisplayAlert("Password error", "Re-entered password does not match the entered password", "OK");
                return;
            }

            User newUser = new User
            {
                ID = 0,
                Username = username,
                Email = email,
                Password = password
            };

            int id = App.Users.RegisterUser(newUser);
            if (id == 0)
            {
                // Username and/or email already exist alert
                await DisplayAlert("Register error", "Username and/or Email already exist", "OK");
                return;
            }

            App.UserID = id;

            // Create new SelectBluetooth Page
            var page = new SelectBluetoothPage()
            {
                BindingContext = new SelectBluetoothPageController(),
                FirstTime = true
            };

            // Create new Navigation Page
            App.Current.MainPage = new NavigationPage(page)
            {
                BarBackgroundColor = Color.FromHex("#002060")
            };
        }
    }
}