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
    public partial class AddToContainersPage : ContentPage
    {
        public AddToContainersPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            containersListView.ItemsSource = (BindingContext as AddToContainersPageController).Containers;
        }

        private async void SaveContainersButton_Clicked(object sender, EventArgs e)
        {
            // update containers with ingredients
            (BindingContext as AddToContainersPageController).UpdateContainers();

            await Navigation.PopAsync();
        }

        private async void CancelContainersButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}