using System.Windows.Input;
using WhoAreYou_Xamarin.Models;
using WhoAreYou_Xamarin.Services.Dependencies;
using WhoAreYou_Xamarin.Services;
using WhoAreYou_Xamarin.Views;
using Xamarin.Forms;
using WhoAreYou_Xamarin.Models.Url;
using WhoAreYou_Xamarin.Models.Property;
using System;

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

        public bool rememberCheck { get; set; }


        public LoginViewModel()
        {
            LoginCommand = new Command(LoginExecuteMethod);
            GoToSignUpCommand = new Command(GoToSignUpExecuteMethod);

            object isRemember = propertyService.Read(LocalProperties.isRememberInfo);

            if(isRemember == null)
            {
                return;
            }

            if (bool.Parse(isRemember.ToString()))
            {
                Id = propertyService.Read(ServerProperties.email).ToString();
                Entry entry = new Entry();
                entry.Text = propertyService.Read(ServerProperties.password).ToString();

                LoginExecuteMethod(entry);
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

            string jsonString = await webService.SendToGet(Urls.SIGNIN.ToString(), id, pw);

            if(string.IsNullOrEmpty(jsonString))
            {
                DependencyService.Get<IToastMessage>().Alert(ErrorMessage.network);
                
                return;
            }

            if (int.Parse(jsonService.ReadJson(jsonString, ServerProperties.code)) == ServerProperties.success)
            {
                string token = jsonService.ReadJson(jsonString, ServerProperties.result);
                propertyService.Write(LocalProperties.token, token);

                if(rememberCheck)
                {
                    propertyService.Write(ServerProperties.email, id);
                    propertyService.Write(ServerProperties.password, pw);
                    propertyService.Write(LocalProperties.isRememberInfo, rememberCheck);
                }

                App.Current.MainPage = new HomeView();  
            }

            
        }
    }
}
