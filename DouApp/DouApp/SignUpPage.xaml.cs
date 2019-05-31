using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DouApp.Models;

namespace DouApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string email = emailEntry.Text;
            string password = passwordEntry.Text;
            string rePassword = passwordReEntry.Text;

            if (username == string.Empty)
            {
                // Username not entered alert
                DisplayAlert("Username error", "Username must be entered", "OK");
                return;
            }

            if (password == string.Empty)
            {
                // Password not entered alert
                DisplayAlert("Password error", "Password must be entered", "OK");
                return;
            }

            if (password != rePassword)
            {
                // Password and re-entered password do not match alert
                DisplayAlert("Password error", "Re-entered password does not match the entered password", "OK");
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
                DisplayAlert("Register error", "Username and/or Email already exist", "OK");
                return;
            }

            App.UserID = id;
            Navigation.PushAsync(new ConfigurePage
            {
                FirstTime = true
            });
        }
    }
}