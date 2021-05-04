using System.Windows.Input;
using WhoAreYou_Xamarin.Models.Property;
using WhoAreYou_Xamarin.Services;
using WhoAreYou_Xamarin.Services.Dependencies;
using WhoAreYou_Xamarin.Views;
using Xamarin.Forms;

namespace WhoAreYou_Xamarin.ViewModels
{
    class OptionViewModel : BaseViewModel
    {
        private bool openAlert;
        private bool closeAlert;

        public ICommand SignOutCommand { get; set; }
        private readonly PropertyService propertyService = new PropertyService();
        public bool OpenAlert 
        {
            get { return openAlert; }
            set
            {
                openAlert = value;
                propertyService.Write(Property.User.openAlert, value);
                OnPropertyUpdate("OpenAlert");
            }
        }
        public bool CloseAlert
        {
            get { return closeAlert; }
            set
            {
                closeAlert = value;
                propertyService.Write(Property.User.closeAlert, value);
                OnPropertyUpdate("CloseAlert");
            }
        }


        public OptionViewModel()
        {
            SignOutCommand = new Command(SignOutExecuteMethod);
            object isOpenAlert = propertyService.Read(Property.User.openAlert);
            object isCloseAlert = propertyService.Read(Property.User.closeAlert);

            if(isOpenAlert == null || isCloseAlert == null)
            {
                propertyService.Write(Property.User.openAlert, true);
                propertyService.Write(Property.User.closeAlert, true);

                OpenAlert = true;
                CloseAlert = true;
            }

            else
            {
                OpenAlert =  bool.Parse(propertyService.Read(Property.User.openAlert).ToString());
                CloseAlert = bool.Parse(propertyService.Read(Property.User.closeAlert).ToString());
            }


        }

        /// <summary>
        /// 로그아웃
        /// </summary>
        /// <param name="obj"></param>
        private void SignOutExecuteMethod(object obj)
        {
            propertyService.Delete(Property.User.email);
            propertyService.Delete(Property.User.token);
            DependencyService.Get<DIForeground>().StopRun();
            App.Current.MainPage = new LoginView();
        }
    }
}
