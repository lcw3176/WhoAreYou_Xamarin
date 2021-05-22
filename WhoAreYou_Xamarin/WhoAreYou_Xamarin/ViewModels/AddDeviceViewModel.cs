using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WhoAreYou_Xamarin.Models;
using WhoAreYou_Xamarin.Services;
using WhoAreYou_Xamarin.Services.Dependencies;
using Xamarin.Forms;

namespace WhoAreYou_Xamarin.ViewModels
{
    public class AddDeviceViewModel : BaseViewModel
    {
        private bool run = false;  // Activity Indicator 컨트롤
        private string _placeHolder;
        private string _statusText;
        public string bluetoothStatus = "페어링 된 기기목록";
        public string bluetoothPlaceHolder = "기기 별칭 입력  ex) 현관문, 창문 감시";

        public string wifiStatus = "와이파이 검색 목록";
        public string wifiPlaceHolder = "와이파이 비밀번호 입력";

        public string statusText
        {
            get { return _statusText; }
            set
            {
                _statusText = value;
                OnPropertyUpdate("statusText");
            }
        }

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
        public ICommand refreshCommand { get; set; }
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
            if (instance == null)
            {
                instance = new AddDeviceViewModel();
            }

            return instance;
        }

        private AddDeviceViewModel()
        {
            bluetoothClickCommand = new Command(ItemClickBluetoothMethod);
            wifiClickCommand = new Command(ItemClickWifiMethod);
            refreshCommand = new Command(RefreshExecuteMethod);
        }

        private void RefreshExecuteMethod(object obj)
        {
            if(statusText == bluetoothStatus)
            {
                Init();
            }

            else
            {
                wirelessCollection.Clear();
                DependencyService.Get<DIWifi>().StartScanWiFi();
            }
        }

        public void Init()
        {
            wirelessCollection.Clear();
            placeHolder = bluetoothPlaceHolder;
            statusText = bluetoothStatus;

            if (!DependencyService.Get<DIBluetooth>().IsEnable())
            {
                MessageService.Show(ErrorMessage.Bluetooth.Disabled);
                return;
            }

            if (!DependencyService.Get<DIWifi>().IsEnable())
            {
                MessageService.Show(ErrorMessage.Wifi.Disabled);
                return;
            }

            
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

        /// <summary>
        /// 연결된 블루투스 장치에게 와이파이 정보 넘겨주기
        /// </summary>
        /// <param name="wifiName"></param>
        public async void ItemClickWifiMethod(object wifiName)
        {
            isRun = true;

            //if (!await DependencyService.Get<DIBluetooth>().SetDevice(wifiName.ToString(), connectionInfo))
            //{
            //    MessageService.Show(ErrorMessage.Wifi.ConnectionFailed);
            //    isRun = false;

            //    return;
            //}

            EnqueueDevice(deviceNickName);
            isRun = false;
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
                MessageService.Show(ErrorMessage.empty);
                isRun = false;

                return;
            }

            //foreach(var i in DevicesViewModel.GetInstance().deviceCollection)
            //{
            //    if(i.name == connectionInfo)
            //    {
            //        MessageService.Show(ErrorMessage.Bluetooth.alreadyExist);
            //        isRun = false;

            //        return;
            //    }

            //}

            //if (!await DependencyService.Get<DIBluetooth>().ConnectDevice(deviceName.ToString()))
            //{
            //    MessageService.Show(ErrorMessage.Bluetooth.ConnectionFailed);
            //    isRun = false;

            //    return;
            //}

            deviceNickName = connectionInfo;
            wirelessCollection.Clear();
            isRun = false;
            
            placeHolder = wifiPlaceHolder;
            statusText = wifiStatus;
            connectionInfo = string.Empty;

            DependencyService.Get<DIWifi>().StartScanWiFi();
            
        }

     
    }
}
