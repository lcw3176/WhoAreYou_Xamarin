namespace WhoAreYou_Xamarin.Services.Dependencies
{
    public interface IForegroundManager
    {
        void Start();
        void Stop();
        void Update(string content);
    }
}
