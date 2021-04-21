using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WhoAreYou_Xamarin.Models;
using WhoAreYou_Xamarin.Models.Property;
using WhoAreYou_Xamarin.Models.Response;
using WhoAreYou_Xamarin.Models.Url;
using WhoAreYou_Xamarin.Services;
using WhoAreYou_Xamarin.Views;
using Xamarin.Forms;

namespace WhoAreYou_Xamarin.ViewModels
{
    class DevicesViewModel : BaseViewModel
    {
        public ObservableCollection<Devices> deviceCollection { get; set; } = new ObservableCollection<Devices>();
        private WebService webService = new WebService();
        private PropertyService propertyService = new PropertyService();
        private JsonService jsonService = new JsonService();

        public ICommand addDeviceCommand { get; set; }
        public ICommand searchLogCommand { get; set; }
        public DevicesViewModel()
        {
            Init();
            addDeviceCommand = new Command(AddDeviceExecuteMethod);
            searchLogCommand = new Command(SearchLogExecuteMethod);
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


        private async void Init()
        {
            string email = propertyService.Read(Property.User.email).ToString();
            string token = propertyService.Read(Property.User.token).ToString();

            string result = await webService.SendGetWithToken(Urls.DEVICE, token, email);

            
            if(jsonService.ReadJson(result, Response.code) == Response.Code.success.ToString())
            {
                string jarr = jsonService.ReadJson(result, Response.result);
                var nameList = jsonService.ReadJArray(jarr, Property.Device.name);
                //var timeList = jsonService.ReadJArray(jarr, Property.Device.time);
                //DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

                for (int i = 0; i < nameList.Count; i++)
                {
                    deviceCollection.Add(new Devices()
                    {
                        index = i + 1,
                        name = nameList[i],
                        searchLogCommand = searchLogCommand
                        //lastLog = dt.AddMilliseconds(double.Parse(timeList[i]))
                        //            .ToLocalTime()
                    });
                }
            }

            else
            {
                deviceCollection.Add(new Devices()
                {
                    name = "추가된 기기가 없습니다.",
                    //lastLog = DateTime.Today
                });
            }
        }
    }
}
