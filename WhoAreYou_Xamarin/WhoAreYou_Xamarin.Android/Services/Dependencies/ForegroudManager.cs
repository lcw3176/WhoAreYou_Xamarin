
using Android.App;
using Android.Content;
using Android.OS;
using WhoAreYou_Xamarin.Droid.Services.Dependencies;
using WhoAreYou_Xamarin.Services.Dependencies;

[assembly: Xamarin.Forms.Dependency(typeof(ForegroudManager))]
namespace WhoAreYou_Xamarin.Droid.Services.Dependencies
{
    class ForegroudManager : IForegroundManager
    {

        public void Start()
        {
            Intent intent = new Intent(Application.Context, typeof(ForegroundService));

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                Application.Context.StartForegroundService(intent);
            }
            else
            {
                Application.Context.StartService(intent);
            }
        }

        public void Stop()
        {
            Intent intent = new Intent(Application.Context, typeof(ForegroundService));

            Application.Context.StopService(intent);
        }

        public void Update(string content)
        {
            throw new System.NotImplementedException();
        }

    }
}