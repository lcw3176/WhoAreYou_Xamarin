using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using System.Threading;
using System.Threading.Tasks;
using WhoAreYou_Xamarin.Models.Property;
using WhoAreYou_Xamarin.Services;

namespace WhoAreYou_Xamarin.Droid.Services
{
    [Service]
    public class ForegroundService : Service
    {
        private static readonly string channelName = "socketChannel";
        private static readonly string channelId = "socketId";
        private const int ID = 2000;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }



        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Init();

            if (!SocketService.GetInstance().IsConnect())
            {
                SocketService.GetInstance().StartReceive();
            }

            return StartCommandResult.RedeliverIntent;
        }

        private void Init()
        {
            Intent defaultIntent = new Intent(Application.Context, typeof(MainActivity));
            defaultIntent.PutExtra("goToMain", "goToMain");

            PendingIntent defaultPendingIntent = PendingIntent.GetActivity(Application.Context, 1, defaultIntent, PendingIntentFlags.UpdateCurrent);


            var notification = new Notification.Builder(Application.Context, channelId)
                .SetContentIntent(defaultPendingIntent)
                .SetSmallIcon(Resource.Drawable.noti_logo)
                .SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.bell))
                .SetContentTitle("알람 수신 중")
                .SetContentText("장치가 작동하면 알려드릴게요")
                .SetShowWhen(false)
                .Build();

            var manager = (Android.App.NotificationManager)Application.Context.GetSystemService(Context.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelNameJava = new Java.Lang.String(channelName);
                var channel = new NotificationChannel(channelId, channelNameJava, NotificationImportance.Default);

                manager.CreateNotificationChannel(channel);

            }

            StartForeground(ID, notification);
        }
    }
}