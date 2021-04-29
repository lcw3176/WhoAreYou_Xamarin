using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Input;
using WhoAreYou_Xamarin.Models;
using WhoAreYou_Xamarin.Services.Dependencies;
using Xamarin.Forms;

namespace WhoAreYou_Xamarin.ViewModels
{
    class AddDeviceViewModel : BaseViewModel
    {
        private bool run = false;  // Activity Indicator 컨트롤

        public ObservableCollection<Wireless> wirelessCollection { get; set; } = new ObservableCollection<Wireless>();
        public ICommand itemClickCommand { get; set; }        
        public bool isRun 
        {
            get { return run; }
            set
            {
                run = value;
                OnPropertyUpdate("isRun");
            }
        }


        public AddDeviceViewModel()
        {
            if(!DependencyService.Get<DIBluetooth>().IsEnable())
            {
                DependencyService.Get<DIToastMessage>().Alert(ErrorMessage.Bluetooth.Disabled);
                return;
            }

            if (!DependencyService.Get<DIWifi>().IsEnable())
            {
                DependencyService.Get<DIToastMessage>().Alert(ErrorMessage.Wifi.Disabled);
                return;
            }

            itemClickCommand = new Command(ItemClickExecuteMethod);

            List<string> names = DependencyService.Get<DIBluetooth>().GetAllDevicesName();
            List<string> types = DependencyService.Get<DIBluetooth>().GetAllDevicesType();

            for (int i = 0; i < names.Count; i++)
            {
                wirelessCollection.Add(new Wireless()
                {
                    name = names[i],
                    type = types[i],
                    itemClickCommand = this.itemClickCommand
                });
            }
        }

        /// <summary>
        /// listview 아이템 클릭 시 장치의 블루투스와 연결 
        /// </summary>
        /// <param name="deviceName">장치 이름</param>
        private async void ItemClickExecuteMethod(object deviceName)
        {
            isRun = true;

            if (DependencyService.Get<DIBluetooth>().isConnect())
            {
                DependencyService.Get<DIBluetooth>().DisconnectDevice();
            }

            if (!await DependencyService.Get<DIBluetooth>().ConnectDevice(deviceName.ToString()))
            {
                DependencyService.Get<DIToastMessage>().Alert(ErrorMessage.Bluetooth.ConnectionFailed);
                isRun = false;

                return;
            }

            string ssid = DependencyService.Get<DIWifi>().GetSSID();
            Console.WriteLine(ssid);

            if (!await DependencyService.Get<DIBluetooth>().SetDevice(ssid, "123123"))
            {
                DependencyService.Get<DIToastMessage>().Alert(ErrorMessage.Wifi.ConnectionFailed);
                isRun = false;

                return;
            }

            await App.Current.MainPage.Navigation.PopToRootAsync();
            isRun = false;
        }

     
    }
}
