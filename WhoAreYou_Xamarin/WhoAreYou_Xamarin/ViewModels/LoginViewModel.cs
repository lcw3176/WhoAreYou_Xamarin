using System.Windows.Input;
using WhoAreYou_Xamarin.Models;
using WhoAreYou_Xamarin.Services.Dependencies;
using WhoAreYou_Xamarin.Services;
using WhoAreYou_Xamarin.Views;
using Xamarin.Forms;
using WhoAreYou_Xamarin.Models.Url;
using WhoAreYou_Xamarin.Models.Property;
using WhoAreYou_Xamarin.Models.Response;
using System;
using System.Threading.Tasks;

namespace WhoAreYou_Xamarin.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        private string id = string.Empty;
        private WebService webService = new WebService();
        private JsonService jsonService = new JsonService();
        private PropertyService propertyService = new PropertyService();

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

                if (jsonService.ReadJson(result, Response.code) == Response.Code.success.ToString())
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
                DependencyService.Get<IToastMessage>().Alert(ErrorMessage.empty);
                
                return;
            }
            EncryptoService encryptoService = new EncryptoService();

            pw = encryptoService.Generate(pw); 
            string jsonString = await webService.SendGet(Urls.SIGNIN, id, pw);

            if(string.IsNullOrEmpty(jsonString))
            {
                DependencyService.Get<IToastMessage>().Alert(ErrorMessage.network);
                
                return;
            }

            if (int.Parse(jsonService.ReadJson(jsonString, Response.code)) == Response.Code.success)
            {
                string token = jsonService.ReadJson(jsonString, Response.result);
                propertyService.Write(Property.User.token, token);
                propertyService.Write(Property.User.email, id);

                App.Current.MainPage = new HomeView();  
            }

            else
            {
                DependencyService.Get<IToastMessage>().Alert(ErrorMessage.notMember);
            }

            
        }
    }
}
