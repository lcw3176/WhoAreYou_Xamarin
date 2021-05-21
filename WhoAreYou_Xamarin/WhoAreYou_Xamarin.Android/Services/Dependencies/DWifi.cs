using Android.Content;
using Android.Net.Wifi;
using System.Collections.Generic;
using WhoAreYou_Xamarin.Droid.Services.Dependencies;
using WhoAreYou_Xamarin.Models;
using WhoAreYou_Xamarin.Services.Dependencies;
using WhoAreYou_Xamarin.ViewModels;
using Xamarin.Forms;

[assembly: Dependency(typeof(DWifi))]
namespace WhoAreYou_Xamarin.Droid.Services.Dependencies
{
    class DWifi : DIWifi
    {
        private static WifiManager wifiManager;
        private WifiReceiver wifiReceiver;
        public static List<string> wifiNetworks;

        public DWifi()
        {
            wifiManager = (WifiManager)(Android.App.Application.Context.GetSystemService(Context.WifiService));
        }

        public bool IsEnable()
        {
            return wifiManager.IsWifiEnabled;
        }

        public void StartScanWiFi()
        {
            wifiNetworks = new List<string>();
            wifiReceiver = new WifiReceiver();

            Android.App.Application.Context.RegisterReceiver(wifiReceiver, new IntentFilter(WifiManager.ScanResultsAvailableAction));
            wifiManager.StartScan();
        }

        public class WifiReceiver : BroadcastReceiver
        {
            
            public override void OnReceive(Context context, Intent intent)
            {
                
                IList<ScanResult> scanResults = wifiManager.ScanResults;

                foreach(ScanResult i in scanResults)
                {
                    if(!string.IsNullOrEmpty(i.Ssid))
                    {
                        AddDeviceViewModel.GetInstance().wirelessCollection.Add(new Wireless()
                        {
                            name = i.Ssid,
                            type = i.Frequency.ToString(),
                            itemClickCommand = AddDeviceViewModel.GetInstance().wifiClickCommand
                        });
                    }

                    //wifiNetworks.Add(i.Ssid);
                }
            }
        }
        
    }
}