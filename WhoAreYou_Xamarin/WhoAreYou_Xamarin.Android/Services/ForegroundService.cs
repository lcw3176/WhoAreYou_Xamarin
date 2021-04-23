
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using WhoAreYou_Xamarin.Models;

namespace WhoAreYou_Xamarin.Droid.Services
{
    [Service]
    class ForegroundService : Service
    {
        const int NOTIFICATION_ID = 9000;
        const string channelId = "whoareyou";
        const string channelName = "whoAreYou";
        const string channelDescription = "the default channel for whoareyou";

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Intent defaultIntent = new Intent(Application.Context, typeof(MainActivity));
            defaultIntent.PutExtra("goToMain", "goToMain");

            PendingIntent defaultPendingIntent = PendingIntent.GetActivity(Application.Context, 1, defaultIntent, PendingIntentFlags.UpdateCurrent);

            var notification = new Notification.Builder(Application.Context, channelId)
                .SetContentIntent(defaultPendingIntent)
                .SetSmallIcon(Resource.Drawable.noti_logo)
                .SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.bell))
                .SetContentTitle(Foreground.title)
                .SetContentText(Foreground.content)
                .Build();

            var manager = (NotificationManager)Application.Context.GetSystemService(Context.NotificationService);

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

            StartForeground(NOTIFICATION_ID, notification);

            return StartCommandResult.Sticky;
        }
    }
}