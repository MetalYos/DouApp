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

            containersListView.ItemsSource = App.Database.GetContainers();
            cancelContainersButton.IsVisible = !FirstTime;
        }

        private void SaveContainersButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new TabbedMainPage());
        }

        private async void CancelContainersButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}