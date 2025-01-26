using CasusVictuzMobile.Enums;
using CasusVictuzMobile.MVVM.View;
using CasusVictuzMobile.Services;
using CasusVictuzMobile.Session;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CasusVictuzMobile.MVVM.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        public ICommand LoginCommand { get; set; }
        public ICommand NavigateToMainPageAsGuestCommand { get; set; }
        //public INavigation Navigation { get; set; }

        public ICommand NavigateToRegisterCommand { get; set; }

        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private string password;
                

        public LoginViewModel(INavigation navigation)
        {
            
            UserSession.Instance.Initialize();           

            if(UserSession.Instance.IsLoggedIn)
            {
                navigation.PushModalAsync(new MainPage());
            }

            LoginCommand = new Command(async () =>
            {
                try
                {
                    UserService userService = new UserService();
                    LoginResult loginResult = userService.Login(Email, Password);

                    if (loginResult == LoginResult.Success)
                    {
                        await navigation.PushModalAsync(new MainPage());
                    }
                    else if (loginResult == LoginResult.UserNotFound)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "User not found", "OK");
                    }
                    else if (loginResult == LoginResult.WrongPassword)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Invalid email or password", "OK");
                    }
                }catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                
            });

            NavigateToMainPageAsGuestCommand = new Command(async () =>
            {               
                UserService userService = new UserService();
                userService.RegisterGuestAccount();                

                await navigation.PushModalAsync(new MainPage());
            });

            NavigateToRegisterCommand = new Command(async () =>
            {
                await navigation.PushModalAsync(new RegisterPage());
            });
            

        }
    }
}
