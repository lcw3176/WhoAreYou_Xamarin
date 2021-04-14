using System;
using System.Text;
using System.Windows.Input;
using WhoAreYou_Xamarin.Views;
using Xamarin.Forms;

namespace WhoAreYou_Xamarin.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        public string phone;

        public ICommand SigninCommand { get; set; }
        public string phoneNumber 
        {
            get { return phone; }

            set 
            {
                if(value.Length <= 11)
                {
                    phone = value;
                }
                
                OnPropertyUpdate("phoneNumber");
            }
        }

        public LoginViewModel()
        {
            SigninCommand = new Command(SigninExecuteMethod);
        }

        private void SigninExecuteMethod(object obj)
        {
            App.Current.MainPage = new HomeView();
        }
    }
}
