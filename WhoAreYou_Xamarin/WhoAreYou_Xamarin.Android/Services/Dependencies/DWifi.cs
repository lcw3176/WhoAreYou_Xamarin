using Android.Content;
using Android.Net.Wifi;
using System;
using WhoAreYou_Xamarin.Droid.Services.Dependencies;
using WhoAreYou_Xamarin.Services.Dependencies;
using Xamarin.Forms;

[assembly: Dependency(typeof(DWifi))]
namespace WhoAreYou_Xamarin.Droid.Services.Dependencies
{
    class DWifi : DIWifi
    {
        private WifiManager wifiManager;

        public DWifi()
        {
            wifiManager = (WifiManager)(Android.App.Application.Context.GetSystemService(Context.WifiService));
        }

        public bool IsEnable()
        {
            return wifiManager.IsWifiEnabled;
        }

        public string GetSSID()
        {

            if (wifiManager != null && !string.IsNullOrEmpty(wifiManager.ConnectionInfo.SSID))
            {
                return wifiManager.ConnectionInfo.SSID;
            }
            else
            {
                return "WiFiManager is NULL";
            }

        }
    }
}