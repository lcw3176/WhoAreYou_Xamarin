using WhoAreYou_Xamarin.Models.Property;
using WhoAreYou_Xamarin.Services;

namespace WhoAreYou_Xamarin.ViewModels
{
    class HomeViewModel
    {
        public HomeViewModel()
        {
            Init();     
        }

        private async void Init()
        {
            if(!SocketService.IsConnect())
            {
                PropertyService propertyService = new PropertyService();
                bool result = await SocketService.Connect(propertyService.Read(Property.User.token).ToString());

                if (result)
                {
                    SocketService.StartReceive();
                }
            }

        }

    }
}
