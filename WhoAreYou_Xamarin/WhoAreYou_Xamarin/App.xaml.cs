using WhoAreYou_Xamarin.Models;
using WhoAreYou_Xamarin.Services;
using WhoAreYou_Xamarin.Services.Dependencies;
using WhoAreYou_Xamarin.Views;
using Xamarin.Forms;

namespace WhoAreYou_Xamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new LoginView();
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
  
        }

        protected override void OnResume()
        {

        }
    }
}
