using System;
using System.Collections.Generic;
using System.Text;

using DouApp.Models;
using DouApp.Interfaces;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace DouApp.BindingContexts
{
    public class SelectBluetoothPageController
    {
        public List<MyBluetoothDevice> BluetoothDevices { get; set; }

        public SelectBluetoothPageController()
        {
            try
            {
                BluetoothDevices = DependencyService.Get<IBluetoothHelper>().GetPairedDevices();
            }
            catch
            {
                // Do nothing (this is a temporary solution).
                BluetoothDevices = new List<MyBluetoothDevice>();
            }
        }

        public async Task<bool> ConnectToBluetoothDevice(string name, int numTimes)
        {
            DependencyService.Get<IBluetoothHelper>().Disconnect();

            while (!DependencyService.Get<IBluetoothHelper>().IsConnected() && numTimes > 0)
            {
                numTimes--;
                try
                {
                    await DependencyService.Get<IBluetoothHelper>().Connect(name);
                }
                catch
                {
                    return false;
                }
            }

            if (numTimes == 0)
                return false;
            else
                return true;
        }
    }
}
