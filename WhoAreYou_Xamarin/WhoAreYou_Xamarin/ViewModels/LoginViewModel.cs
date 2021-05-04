using System.Windows.Input;
using WhoAreYou_Xamarin.Models;
using WhoAreYou_Xamarin.Services.Dependencies;
using WhoAreYou_Xamarin.Services;
using WhoAreYou_Xamarin.Views;
using Xamarin.Forms;
using WhoAreYou_Xamarin.Models.Url;
using WhoAreYou_Xamarin.Models.Property;

namespace WhoAreYou_Xamarin.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        private string id = string.Empty;
        private readonly WebService webService = new WebService();
        private readonly PropertyService propertyService = new PropertyService();

        public ICommand LoginCommand { get; set; }
        public ICommand GoToSignUpCommand { get; set; }
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyUpdate("Id");
            }
        }


        public LoginViewModel()
        {
            Init();
            LoginCommand = new Command(LoginExecuteMethod);
            GoToSignUpCommand = new Command(GoToSignUpExecuteMethod);            
        }


        private async void Init()
        {
            object token = propertyService.Read(Property.User.token);

            if (token != null)
            {
                string result = await webService.SendGet(Urls.AUTHCHECK, token.ToString());

                if(!string.IsNullOrEmpty(result))
                {
                    App.Current.MainPage = new HomeView();
                }

            }
        }

        /// <summary>
        /// 회원가입 페이지로 이동
        /// </summary>
        /// <param name="obj"></param>
        private void GoToSignUpExecuteMethod(object obj)
        {
            App.Current.MainPage = new SignUpView();
        }

        /// <summary>
        /// 로그인 시도
        /// </summary>
        /// <param name="obj">패스워드 Entry 객체</param>
        private async void LoginExecuteMethod(object obj)
        {
            string pw = (obj as Entry).Text;

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pw))
            {
                DependencyService.Get<DIToastMessage>().Alert(ErrorMessage.empty);
                
                return;
            }

            EncryptoService encryptoService = new EncryptoService();

            pw = encryptoService.Generate(pw); 
            string responseToken = await webService.SendGet(Urls.SIGNIN, id, pw);

            if(string.IsNullOrEmpty(responseToken))
            {
                DependencyService.Get<DIToastMessage>().Alert(ErrorMessage.notMember);
                
                return;
            }

            propertyService.Write(Property.User.token, responseToken);
            propertyService.Write(Property.User.email, id);

            App.Current.MainPage = new HomeView();
        }
    }
}
