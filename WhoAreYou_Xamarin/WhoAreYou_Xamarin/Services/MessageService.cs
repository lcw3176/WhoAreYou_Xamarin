using WhoAreYou_Xamarin.Services.Dependencies;
using Xamarin.Forms;

namespace WhoAreYou_Xamarin.Services
{
    public static class MessageService
    {
        public static void Show(string msg)
        {
            DependencyService.Get<DIToastMessage>().Alert(msg);
        }
    }
}
