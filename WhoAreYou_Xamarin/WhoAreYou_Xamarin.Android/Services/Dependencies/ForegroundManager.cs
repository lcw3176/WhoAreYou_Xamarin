using Android.Content;
using WhoAreYou_Xamarin.Services.Dependencies;

[assembly: Xamarin.Forms.Dependency(typeof(WhoAreYou_Xamarin.Droid.Services.Dependencies.ForegroundManager))]
namespace WhoAreYou_Xamarin.Droid.Services.Dependencies
{
    class ForegroundManager : IForegroundManager
    {
        private static bool isRun = false;

        public bool IsRunning()
        {
            return isRun;
        }

        public void StopRun()
        {
            isRun = false;
            Intent intent = new Intent(Android.App.Application.Context, typeof(ForegroundService));
            Android.App.Application.Context.StopService(intent);
        }

        public void StartService()
        {
            if(!isRun)
            {
                Intent intent = new Intent(Android.App.Application.Context, typeof(ForegroundService));

                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
                {
                    Android.App.Application.Context.StartForegroundService(intent);
                }
                else
                {
                    Android.App.Application.Context.StartService(intent);
                }

                isRun = true;
            }

        }
    }
}