using Android.Bluetooth;
using System;
using System.Collections.Generic;
using WhoAreYou_Xamarin.Services.Dependencies;
using Xamarin.Forms;

[assembly: Dependency(typeof(WhoAreYou_Xamarin.Droid.Services.Dependencies.BluetoothManager))]
namespace WhoAreYou_Xamarin.Droid.Services.Dependencies
{
    class BluetoothManager : IBluetoothManager
    {
        private BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
        public BluetoothManager()
        {
            

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

            foreach(var i in adapter.BondedDevices)
            {
                lst.Add(i.BluetoothClass.MajorDeviceClass.ToString());
            }

            return lst;
        }
    }
}