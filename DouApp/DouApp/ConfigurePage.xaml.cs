using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DouApp.BindingContexts;

namespace DouApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigurePage : ContentPage
    {
        public bool FirstTime { get; set; }
        public ConfigurePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            containersListView.ItemsSource = (BindingContext as ConfigurePageController).Containers;
            cancelContainersButton.IsVisible = !FirstTime;
        }

        private async void SaveContainersButton_Clicked(object sender, EventArgs e)
        {
            // update containers with ingredients
            (BindingContext as ConfigurePageController).UpdateContainers();

            // Create a new main page
            var tabbedPage = new TabbedMainPage()
            {
                BarBackgroundColor = Color.FromHex("#002060")
            };

            if (FirstTime)
            {
                App.Current.MainPage = new NavigationPage(tabbedPage)
                {
                    BarBackgroundColor = Color.FromHex("#002060")
                };
            }
            else
            {
                await Navigation.PopAsync();
            }
        }

        private async void CancelContainersButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}