using System;
using System.Collections.ObjectModel;
using WhoAreYou_Xamarin.Models;
using WhoAreYou_Xamarin.Models.Property;
using WhoAreYou_Xamarin.Models.Response;
using WhoAreYou_Xamarin.Models.Url;
using WhoAreYou_Xamarin.Services;

namespace WhoAreYou_Xamarin.ViewModels
{
    class DevicesViewModel : BaseViewModel
    {
        public ObservableCollection<Device> deviceCollection { get; set; } = new ObservableCollection<Device>();
        private WebService webService = new WebService();
        private PropertyService propertyService = new PropertyService();
        private JsonService jsonService = new JsonService();

        public DevicesViewModel()
        {
            Init();
        }

        private async void Init()
        {
            string email = propertyService.Read(Property.email).ToString();
            string token = propertyService.Read(Property.token).ToString();

            string result = await webService.SendGetWithToken(Urls.DEVICE, token, email);

            
            if(jsonService.ReadJson(result, Response.code) == Response.Code.success.ToString())
            {
                Console.WriteLine(jsonService.ReadJson(result, Response.result));
            }
        }
    }
}
