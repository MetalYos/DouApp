using System.Collections.Generic;
using System.Threading.Tasks;

using DouApp.Models;

namespace DouApp.Interfaces
{
    public interface IBluetoothHelper
    {
        bool IsConnected();
        List<MyBluetoothDevice> GetPairedDevices();
        Task<bool> Connect(string name);
        void WriteStringToDevice(string message);
        Task<string> ReadStringFromDevice(int maxSeconds);
    }
}