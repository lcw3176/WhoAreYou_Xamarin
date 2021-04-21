using System.Windows.Input;
using WhoAreYou_Xamarin.Models.Property;
using WhoAreYou_Xamarin.Services;
using WhoAreYou_Xamarin.Views;
using Xamarin.Forms;

namespace WhoAreYou_Xamarin.ViewModels
{
    class OptionViewModel : BaseViewModel
    {
        public ICommand SignOutCommand { get; set; }
        private PropertyService propertyService = new PropertyService();

        public OptionViewModel()
        {
            SignOutCommand = new Command(SignOutExecuteMethod);
        }

        private void SignOutExecuteMethod(object obj)
        {
            propertyService.Delete(Property.User.token);
            App.Current.MainPage = new LoginView();
        }
    }
}
