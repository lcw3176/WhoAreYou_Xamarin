using Android.Bluetooth;
using Android.Locations;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhoAreYou_Xamarin.Models.Enum;
using WhoAreYou_Xamarin.Services.Dependencies;
using Xamarin.Forms;

[assembly: Dependency(typeof(WhoAreYou_Xamarin.Droid.Services.Dependencies.DBluetooth))]
namespace WhoAreYou_Xamarin.Droid.Services.Dependencies
{
    class DBluetooth : DIBluetooth
    {
        private BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
        private BluetoothSocket socket;


        public bool IsEnable()
        {
            LocationManager locationManager = (LocationManager)Android.App.Application.Context.GetSystemService(Android.App.Service.LocationService);

            return adapter.IsEnabled && locationManager.IsLocationEnabled;
        }

        public async Task<bool> ConnectDevice(string deviceName)
        {
            if(isConnect())
            {
                DisconnectDevice();
            }

            BluetoothDevice device = null;

            foreach(var i in adapter.BondedDevices)
            {
                if(i.Name == deviceName)
                {
                    device = adapter.GetRemoteDevice(i.Address);
                    break;
                }
            }

            try
            {
                if(device != null)
                {
                    socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));
                    await socket.ConnectAsync();

                    return true;
                }

                return false;

            }

            catch
            {
                return false;
            }
            
        }

        public List<string> GetAllDevicesName()
        {
            List<string> lst = new List<string>();

            foreach (var i in adapter.BondedDevices)
            {                
                lst.Add(i.Name);
            }

            return lst;
        }

        public List<string> GetAllDevicesType()
        {
            List<string> lst = new List<string>();

            foreach (var i in adapter.BondedDevices)
            {
                lst.Add(i.BluetoothClass.MajorDeviceClass.ToString());
            }

            return lst;
        }

        public async Task<bool> SendInfo(Dictionary<BLECommand, string> data)
        {
            string startFlag = "<start>";
            string endFlag = "<end>";

            StringBuilder sb = new StringBuilder();

            try
            {
                foreach(var i in data)
                {
                    sb.Append(startFlag);
                    sb.Append("/");
                    sb.Append((int)i.Key);
                    sb.Append("/");
                    sb.Append(i.Value);
                    sb.Append("/");
                    sb.Append(endFlag);

                    Java.Lang.String msg = new Java.Lang.String(sb.ToString());

                    byte[] buffer = msg.GetBytes();

                    await socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                    sb.Clear();

                    Thread.Sleep(1000);

                }
               
                
                return true;
            }

            catch
            {
                return false;
            }
            
        }

        private bool isConnect()
        {
            if(socket != null)
            {
                return socket.IsConnected;
            }

            return false;
        }   

        private bool DisconnectDevice()
        {
            try
            {
                if(socket != null && socket.IsConnected)
                {
                    socket.Close();
                    socket.Dispose();
                }

                return true;
            }

            catch
            {
                return false;
            }

        }
    }

}