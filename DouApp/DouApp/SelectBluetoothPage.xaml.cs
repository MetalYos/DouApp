using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DouApp.Models;
using DouApp.BindingContexts;

namespace DouApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectBluetoothPage : ContentPage
    {
        public SelectBluetoothPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            btDevicesListView.ItemsSource = null;
            btDevicesListView.ItemsSource = (BindingContext as SelectBluetoothPageController).BluetoothDevices;
        }

        private async void BtDevicesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var controller = (BindingContext as SelectBluetoothPageController);
            if (controller == null)
                return;

            // Get selected name
            MyBluetoothDevice device = (e.Item as MyBluetoothDevice);
            if (device == null)
                return;

            // Connect to Bluetooth
            bool tryAgain = true;
            while (tryAgain)
            {
                if (!await controller.ConnectToBluetoothDevice(device.Name, 30))
                {
                    string answer = await DisplayActionSheet("Couldn't connect to " + device.Name + "! Try again?",
                        "Cancel", null, "Yes", "No");
                    if (answer == "Yes") tryAgain = true;
                    else if (answer == "No") tryAgain = false;
                    else return;
                }
                else
                {
                    App.Current.Properties["DeviceName"] = device.Name;
                    await App.Current.SavePropertiesAsync();
                    tryAgain = false;
                }
            }

            await Navigation.PopAsync();
        }
    }
}