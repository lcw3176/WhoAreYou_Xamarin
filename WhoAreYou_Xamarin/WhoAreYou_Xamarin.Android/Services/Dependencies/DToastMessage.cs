using Android.Widget;
using WhoAreYou_Xamarin.Droid.Services.Dependencies;
using WhoAreYou_Xamarin.Services.Dependencies;
using Xamarin.Forms;

[assembly: Dependency(typeof(DToastMessage))]
namespace WhoAreYou_Xamarin.Droid.Services.Dependencies
{
    class DToastMessage : DIToastMessage
    {
        public void Alert(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}
