using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

using DouApp.Interfaces;
using DouApp.Models;
using System.Threading.Tasks;

namespace DouApp.iOS
{
    class BluetoothHelper : IBluetoothHelper
    {
        public Task<bool> Connect(string name)
        {
            throw new NotImplementedException();
        }

        public List<MyBluetoothDevice> GetPairedDevices()
        {
            throw new NotImplementedException();
        }

        public bool IsConnected()
        {
            throw new NotImplementedException();
        }

        public Task<string> ReadStringFromDevice(int maxSeconds)
        {
            throw new NotImplementedException();
        }

        public void WriteStringToDevice(string message)
        {
            throw new NotImplementedException();
        }
    }
}