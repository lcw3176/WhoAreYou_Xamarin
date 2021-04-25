
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using WhoAreYou_Xamarin.Services.Dependencies;

[assembly: Xamarin.Forms.Dependency(typeof(WhoAreYou_Xamarin.Droid.Services.Dependencies.PushAlarmManager))]
namespace WhoAreYou_Xamarin.Droid.Services.Dependencies
{
    class PushAlarmManager : IPushAlarmManager
    {
        private int NOTIFICATION_ID = 9000;
        private const string channelId = "whoareyou";
        private const string channelName = "whoAreYou";
        private const string channelDescription = "the default channel for whoareyou";
        public static PushAlarmManager Instance { get; private set; }

        private Android.App.NotificationManager manager;

        public PushAlarmManager()
        {
            if (Instance == null)
            {
                CreateChannel();
                Instance = this;
            }

        }

        private void CreateChannel()
        {
            manager = (Android.App.NotificationManager)Application.Context.GetSystemService(Context.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelNameJava = new Java.Lang.String(channelName);
                var channel = new NotificationChannel(channelId, channelNameJava, NotificationImportance.Default)
                {
                    Description = channelDescription
                };

                channel.SetSound(null, null);
                channel.EnableVibration(true);
                manager.CreateNotificationChannel(channel);
            }
        }

        public void Update(string deviceName, bool isOpen)
        {
            string content;

            if(isOpen)
            {
                content = deviceName + "(이)가 열렸습니다";
            }

            else
            {
                content = deviceName + "(이)가 닫혔습니다";
            }

            Intent defaultIntent = new Intent(Application.Context, typeof(MainActivity));
            defaultIntent.PutExtra("goToMain", "goToMain");

            PendingIntent defaultPendingIntent = PendingIntent.GetActivity(Application.Context, 1, defaultIntent, PendingIntentFlags.UpdateCurrent);

            var notification = new Notification.Builder(Application.Context, channelId)
                .SetContentIntent(defaultPendingIntent)
                .SetSmallIcon(Resource.Drawable.noti_logo)
                .SetLargeIcon(BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.bell))
                .SetContentTitle("장치 작동")
                .SetContentText(content)
                .SetShowWhen(true)
                .Build();
            

            manager.Notify(NOTIFICATION_ID++, notification);
        }

    }
}