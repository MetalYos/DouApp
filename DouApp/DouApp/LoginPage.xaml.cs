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

        private void SignUpButton_Clicked(object sender, EventArgs e)
        {

        }

        async private void LoginButton_Clicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            if (username != mockUsername)
            {
                await DisplayAlert("Wrong username", "The entered Username does not exist", "OK");
                return;
            }

            if (password != mockPassword)
            {
                await DisplayAlert("Wrong password", "The entered Password does not match the given username", "OK");
                return;
            }

            await Navigation.PushAsync(new TabbedMainPage());
        }
    }
}