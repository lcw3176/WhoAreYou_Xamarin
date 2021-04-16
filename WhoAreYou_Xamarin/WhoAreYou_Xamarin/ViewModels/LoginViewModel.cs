﻿using System.Windows.Input;
using WhoAreYou_Xamarin.Models;
using WhoAreYou_Xamarin.Services.Dependencies;
using WhoAreYou_Xamarin.Services;
using WhoAreYou_Xamarin.Views;
using Xamarin.Forms;

namespace WhoAreYou_Xamarin.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        private string id = string.Empty;
        private WebService webService = new WebService();

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
            LoginCommand = new Command(LoginExecuteMethod);
            GoToSignUpCommand = new Command(GoToSignUpExecuteMethod);
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
        private void LoginExecuteMethod(object obj)
        {
            string pw = (obj as Entry).Text;

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pw))
            {
                DependencyService.Get<IToastMessage>().Alert(ErrorMessage.emptyError);
            }

            if (webService.Send(null))
            {
                App.Current.MainPage = new HomeView();
            }
            
        }
    }
}
