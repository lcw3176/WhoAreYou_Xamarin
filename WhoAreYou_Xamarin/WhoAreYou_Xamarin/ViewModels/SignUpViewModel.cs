using System.Collections.Generic;
using System.Windows.Input;
using WhoAreYou_Xamarin.Models;
using WhoAreYou_Xamarin.Models.Property;
using WhoAreYou_Xamarin.Models.Url;
using WhoAreYou_Xamarin.Services;
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

        private readonly WebService webService = new WebService();

        
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
        private async void SignUpExecuteMethod(object obj)
        {
            string pw = (obj as Entry).Text;

            if(string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pw))
            {
                MessageService.Show(ErrorMessage.empty);
                
                return;
            }

            EncryptoService encryptoService = new EncryptoService();
            pw = encryptoService.Generate(pw);


            Dictionary<string, string> value = new Dictionary<string, string>();
            value.Add(Property.User.email, id);
            value.Add(Property.User.password, pw);

            string result = await webService.SendPost(Urls.SIGNUP, value);


            if (string.IsNullOrEmpty(result))
            {
                MessageService.Show(ErrorMessage.existUser);

                return;
            }

            MessageService.Show(SuccessMessage.signUp);
            App.Current.MainPage = new LoginView();

        }
    }
}
