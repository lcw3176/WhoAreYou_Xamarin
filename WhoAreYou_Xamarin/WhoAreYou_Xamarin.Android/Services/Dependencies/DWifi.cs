using Android.Content;
using Android.Net.Wifi;
using System;
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
            wifiReceiver = new WifiReceiver();
            IntentFilter intent = new IntentFilter(WifiManager.ScanResultsAvailableAction);
            intent.AddAction(WifiManager.NetworkStateChangedAction);

            Android.App.Application.Context.RegisterReceiver(wifiReceiver, intent);
            wifiManager.StartScan();
        }

        public class WifiReceiver : BroadcastReceiver
        {
            
            public override void OnReceive(Context context, Intent intent)
            {
                IList<ScanResult> scanResults = wifiManager.ScanResults;
                string dbm;

                foreach (ScanResult i in scanResults)
                {
                    if (!string.IsNullOrEmpty(i.Ssid))
                    {
                        foreach(var j in AddDeviceViewModel.GetInstance().wirelessCollection)
                        {
                            if(j.name == i.Ssid)
                            {
                                return;
                            }
                        }

                        if (i.Level < 0 && i.Level >= -80)
                        {
                            dbm = "좋음";
                        }

                        else if(i.Level < -80 && i.Level >= -90)
                        {
                            dbm = "보통";
                        }

                        else
                        {
                            dbm = "나쁨";
                        }

                        AddDeviceViewModel.GetInstance().wirelessCollection.Add(new Wireless()
                        {
                            name = i.Ssid,
                            type = dbm,
                            itemClickCommand = AddDeviceViewModel.GetInstance().wifiClickCommand
                        });
                    }
                }

            }
        }
        
    }
}