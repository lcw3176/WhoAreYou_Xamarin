using System.Collections.ObjectModel;
using System.Windows.Input;
using WhoAreYou_Xamarin.Models;
using WhoAreYou_Xamarin.Models.Property;
using WhoAreYou_Xamarin.Models.Url;
using WhoAreYou_Xamarin.Services;
using WhoAreYou_Xamarin.Services.Dependencies;
using WhoAreYou_Xamarin.Views;
using Xamarin.Forms;

namespace WhoAreYou_Xamarin.ViewModels
{
    class DevicesViewModel : BaseViewModel
    {
        public ObservableCollection<Devices> deviceCollection { get; set; } = new ObservableCollection<Devices>();
        private readonly WebService webService = new WebService();
        private readonly PropertyService propertyService = new PropertyService();
        private readonly JsonService jsonService = new JsonService();

        public ICommand addDeviceCommand { get; set; }
        public ICommand searchLogCommand { get; set; }
        public ICommand deleteCommand { get; set; }
        private static DevicesViewModel instance;

        public static DevicesViewModel GetInstance()
        {
            if(instance == null)
            {
                instance = new DevicesViewModel();
            }

            return instance;
        }

        private DevicesViewModel()
        {
            addDeviceCommand = new Command(AddDeviceExecuteMethod);
            searchLogCommand = new Command(SearchLogExecuteMethod);
            deleteCommand = new Command(DeleteExecuteMethod);
        }


        private async void SearchLogExecuteMethod(object deviceName)
        {
            EnqueueLog(deviceName.ToString());
            await Shell.Current.GoToAsync("//viewLog");
        }

        private async void AddDeviceExecuteMethod(object obj)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new AddDeviceView());
        }

        private async void DeleteExecuteMethod(object obj)
        {
            string deviceName = obj.ToString();
            string email = propertyService.Read(Property.User.email).ToString();
            string token = propertyService.Read(Property.User.token).ToString();

            bool result = await webService.SendDeleteWithToken(Urls.DEVICE, token, email, deviceName);

            if(result)
            {
                Init();
            }
        }

        public async void Init()
        {
            deviceCollection.Clear();

            if (!DependencyService.Get<DIForeground>().IsRunning())
            {
                DependencyService.Get<DIForeground>().StartService();
            }

            string email = propertyService.Read(Property.User.email).ToString();
            string token = propertyService.Read(Property.User.token).ToString();

            string result = await webService.SendGetWithToken(Urls.DEVICE, token, email);

            if(string.IsNullOrEmpty(result))
            {
                deviceCollection.Add(new Devices()
                {
                    name = "추가된 기기가 없습니다.",
                });

                return;
            }

            var nameList = jsonService.ReadJArray(result, Property.Device.name);

            for (int i = 0; i < nameList.Count; i++)
            {
                deviceCollection.Add(new Devices()
                {
                    index = i + 1,
                    name = nameList[i],
                    searchLogCommand = searchLogCommand,
                    deleteCommand = deleteCommand
                });
            }

        }
    }
}
