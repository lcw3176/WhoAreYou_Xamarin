namespace WhoAreYou_Xamarin.Services.Dependencies
{
    public interface IForegroundManager
    {
        void Update(string deviceName, bool isOpen);
    }
}
