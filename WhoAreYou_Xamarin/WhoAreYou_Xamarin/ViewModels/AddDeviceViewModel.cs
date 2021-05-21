using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WhoAreYou_Xamarin.Models;
using WhoAreYou_Xamarin.Services.Dependencies;
using Xamarin.Forms;

namespace WhoAreYou_Xamarin.ViewModels
{
    public class AddDeviceViewModel : BaseViewModel
    {
        private bool run = false;  // Activity Indicator 컨트롤
        private string _placeHolder;
        public string bluetoothPlaceHolder = "기기 별칭 입력  ex) 현관문, 창문 감시";
        public string wifiPlaceHolder = "와이파이 비밀번호 입력";
        public string placeHolder
        {
            get { return _placeHolder; }
            set
            {
                _placeHolder = value;
                OnPropertyUpdate("placeHolder");
            }
        }
        private string deviceNickName = string.Empty;
        private string _connectionInfo;
        public string connectionInfo
        {
            get { return _connectionInfo; }
            set
            {
                _connectionInfo = value;
                OnPropertyUpdate("connectionInfo");
            }
        }
        public ObservableCollection<Wireless> wirelessCollection { get; set; } = new ObservableCollection<Wireless>();
        public ICommand bluetoothClickCommand { get; set; }
        public ICommand wifiClickCommand { get; set; }
        public bool isRun 
        {
            get { return run; }
            set
            {
                run = value;
                OnPropertyUpdate("isRun");
            }
        }

        private static AddDeviceViewModel instance;

        public static AddDeviceViewModel GetInstance()
        {
            if(instance == null)
            {
                instance = new AddDeviceViewModel();
            }

            return instance;
        }

        private AddDeviceViewModel()
        {
            Init();
        }

        private void Init()
        {
            placeHolder = bluetoothPlaceHolder;
            if (!DependencyService.Get<DIBluetooth>().IsEnable())
            {
                DependencyService.Get<DIToastMessage>().Alert(ErrorMessage.Bluetooth.Disabled);
                return;
            }

            if (!DependencyService.Get<DIWifi>().IsEnable())
            {
                DependencyService.Get<DIToastMessage>().Alert(ErrorMessage.Wifi.Disabled);
                return;
            }

            bluetoothClickCommand = new Command(ItemClickBluetoothMethod);
            wifiClickCommand = new Command(ItemClickWifiMethod);

            List<string> names = DependencyService.Get<DIBluetooth>().GetAllDevicesName();
            List<string> types = DependencyService.Get<DIBluetooth>().GetAllDevicesType();

            for (int i = 0; i < names.Count; i++)
            {
                wirelessCollection.Add(new Wireless()
                {
                    name = names[i],
                    type = types[i],
                    itemClickCommand = this.bluetoothClickCommand
                });
            }
        }

        public async void ItemClickWifiMethod(object wifiName)
        {
            isRun = true;

            //if (!await DependencyService.Get<DIBluetooth>().SetDevice(wifiName.ToString(), connectionInfo))
            //{
            //    DependencyService.Get<DIToastMessage>().Alert(ErrorMessage.Wifi.ConnectionFailed);
            //    isRun = false;

            //    return;
            //}

            EnqueueDevice(deviceNickName);
            isRun = false;
            Init();
            await App.Current.MainPage.Navigation.PopToRootAsync();
        }

        /// <summary>
        /// listview 아이템 클릭 시 장치의 블루투스와 연결 
        /// </summary>
        /// <param name="deviceName">장치 이름</param>
        private async void ItemClickBluetoothMethod(object deviceName)
        {
            isRun = true;

            if (string.IsNullOrEmpty(connectionInfo))
            {
                DependencyService.Get<DIToastMessage>().Alert(ErrorMessage.empty);
                isRun = false;

                return;
            }

            //foreach(var i in DevicesViewModel.GetInstance().deviceCollection)
            //{
            //    if(i.name == connectionInfo)
            //    {
            //        DependencyService.Get<DIToastMessage>().Alert(ErrorMessage.Bluetooth.alreadyExist);
            //        isRun = false;

            //        return;
            //    }

            //}

            //if (DependencyService.Get<DIBluetooth>().isConnect())
            //{
            //    DependencyService.Get<DIBluetooth>().DisconnectDevice();
            //}

            //if (!await DependencyService.Get<DIBluetooth>().ConnectDevice(deviceName.ToString()))
            //{
            //    DependencyService.Get<DIToastMessage>().Alert(ErrorMessage.Bluetooth.ConnectionFailed);
            //    isRun = false;

            //    return;
            //}

            deviceNickName = connectionInfo;
            wirelessCollection.Clear();
            isRun = false;
            placeHolder = wifiPlaceHolder;
            DependencyService.Get<DIWifi>().StartScanWiFi();
            connectionInfo = string.Empty;
            //DependencyService.Get<DIWifi>().GetSSID();

        }

     
    }
}
