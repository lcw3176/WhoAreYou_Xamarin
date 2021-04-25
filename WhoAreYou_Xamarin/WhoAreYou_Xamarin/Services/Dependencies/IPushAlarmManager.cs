namespace WhoAreYou_Xamarin.Services.Dependencies
{
    public interface IPushAlarmManager
    {
        void Update(string deviceName, bool isOpen);
    }
}
