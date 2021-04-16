using System.Windows.Input;
using WhoAreYou_Xamarin.Models;
using WhoAreYou_Xamarin.Services;
using WhoAreYou_Xamarin.Services.Dependencies;
using WhoAreYou_Xamarin.Views;
using Xamarin.Forms;

namespace WhoAreYou_Xamarin.ViewModels
{
    class SignUpViewModel : BaseViewModel
    {
        private string id = string.Empty;
        public ICommand GoToLogInCommand { get; set; }
        public ICommand SignUpCommand { get; set; }

        public string Id 
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyUpdate("Id");
            }
        }

        private WebService webService = new WebService();
        
        public SignUpViewModel()
        {
            GoToLogInCommand = new Command(GoToLogInExecuteMethod);
            SignUpCommand = new Command(SignUpExecuteMethod);
        }


        /// <summary>
        /// 로그인 페이지로 이동
        /// </summary>
        /// <param name="obj"></param>
        private void GoToLogInExecuteMethod(object obj)
        {
            App.Current.MainPage = new LoginView();
        }

        /// <summary>
        /// 회원가입 시도
        /// </summary>
        /// <param name="obj">패스워드 Entry 객체</param>
        private void SignUpExecuteMethod(object obj)
        {
            string pw = (obj as Entry).Text;

            if(string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pw))
            {
                DependencyService.Get<IToastMessage>().Alert(ErrorMessage.emptyError);
            }

            if(webService.Send(null))
            {
                App.Current.MainPage = new LoginView();
            }
        }
    }
}
