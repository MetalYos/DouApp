using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DouApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        private async void ResetPassButton_Clicked(object sender, EventArgs e)
        {
            string email = emailEntry.Text;

            // Check if email is empty
            if (email == string.Empty || email == null)
            {
                await DisplayAlert("Email error", "Email must be entered", "OK");
                return;
            }

            // Check if email is valid
            System.Net.Mail.MailAddress address = null;
            try
            {
                address = new System.Net.Mail.MailAddress(email);
            }
            catch (FormatException)
            {
                await DisplayAlert("Email error", "Email is not valid", "OK");
                return;
            }

            // Checl if email belongs to registered user
            if (!App.Users.IsEmailExists(email))
            {
                await DisplayAlert("Email error", "Email does not belong to any registered user", "OK");
                return;
            }

            // Send mail
            await DisplayAlert("Email", "An email was sent to " + email + " with the new password.", "OK");

            // Return to previous page
            await Navigation.PopAsync();
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}