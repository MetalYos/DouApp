using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Bluetooth;

using DouApp.Interfaces;
using DouApp.Droid;
using DouApp.Models;
using Java.Util;
using System.IO;
using System.Threading;
using System.Text;
using Java.IO;

[assembly: Dependency(typeof(BluetoothHelper))]
namespace DouApp.Droid
{
    public class BluetoothHelper : IBluetoothHelper
    {
        private BluetoothSocket _socket = null;

        public async Task<bool> Connect(string name)
        {
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            checkDefaultAdapter(adapter);

            BluetoothDevice device = (from bd in adapter.BondedDevices
                                      where bd.Name == name
                                      select bd).FirstOrDefault();

            if (device == null)
                throw new Exception(name + " device was not found.");

            ParcelUuid[] uuids = null;
            if (device.FetchUuidsWithSdp())
                uuids = device.GetUuids();

            Thread.Sleep(200);

            if ((uuids != null) && (uuids.Length > 0))
            {
                foreach (var uuid in uuids)
                {
                    try
                    {
                        if ((int)Android.OS.Build.VERSION.SdkInt >= 10)
                            _socket = device.CreateInsecureRfcommSocketToServiceRecord(uuid.Uuid);
                        else
                            _socket = device.CreateRfcommSocketToServiceRecord(uuid.Uuid);

                        await _socket.ConnectAsync();
                        break;
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.Source + "\n" + e.Message);
                    }
                }
            }
            else
            {
                if (uuids == null)
                    throw new Exception("uuids is NULL!");
                if (uuids.Length == 0)
                    throw new Exception("uuids.Length == 0!");
            }

            if (_socket == null)
                throw new Exception("socket is NULL!");

            if (!_socket.IsConnected)
                throw new Exception("_socket is not connected!");

            return _socket.IsConnected;
        }

        public bool IsConnected()
        {
            if (_socket == null)
                return false;

            return _socket.IsConnected;
        }

        public List<MyBluetoothDevice> GetPairedDevices()
        {
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            checkDefaultAdapter(adapter);

            List<MyBluetoothDevice> devices = new List<MyBluetoothDevice>();
            if (adapter.BondedDevices.Count > 0)
            {
                foreach (var item in adapter.BondedDevices)
                {
                    devices.Add(new MyBluetoothDevice
                    {
                        Name = item.Name,
                        Address = item.Address
                    });
                }
            }

            return devices;
        }

        public async void WriteStringToDevice(string message)
        {
            if (IsConnected())
            {
                byte[] buffer = Encoding.ASCII.GetBytes(message.ToArray());
                await _socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            }
        }

        private void checkDefaultAdapter(BluetoothAdapter adapter)
        {
            if (adapter == null)
                throw new Exception("No bluetooth adapter found.");

            if (!adapter.IsEnabled)
                throw new Exception("Bluetooth adapter is not enabled.");
        }

        public async Task<string> ReadStringFromDevice(int maxSeconds)
        {
            byte[] buffer = new byte[1024];  // buffer store for the stream
            int bytes; // bytes returned from read()
            string message = "";

            if (!_socket.InputStream.CanRead)
                return message;

            try
            {
                DataInputStream mmInStream = new DataInputStream(_socket.InputStream);

                System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
                stopWatch.Start();
                TimeSpan maxTime = new TimeSpan(0, 0, maxSeconds);
                while (!message.Contains('\n'))
                {
                    // Delay for 0.1 seconds
                    Thread.Sleep(100);

                    // Read from input string
                    buffer = new byte[1024];
                    bytes = await mmInStream.ReadAsync(buffer);
                    message += Encoding.ASCII.GetString(buffer, 0, bytes);
                    if (stopWatch.Elapsed == maxTime)
                    {
                        message = "";
                        stopWatch.Stop();
                        break;
                    }
                }
                stopWatch.Stop();
            }
            catch
            {
                return message;
            }

            return message;
        }

        public void Disconnect()
        {
            _socket.Close();
        }
    }
}