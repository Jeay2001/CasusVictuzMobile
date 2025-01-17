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

        private async Task InitializeAsync()
        {
            await UserSession.Instance.LoadUserAsync();
        }

        public LoginViewModel(INavigation navigation)
        {
            // dit is een workaround die ik gevonden heb, omdat die SecureStore async is
            _ = InitializeAsync();

            if(UserSession.Instance.IsLoggedIn)
            {
                navigation.PushModalAsync(new MainPage());
            }

            LoginCommand = new Command(async () =>
            {
                UserService userService = new UserService();
                LoginResult loginResult = userService.Login(Email, Password);

                if (loginResult == LoginResult.Success)
                {
                    await navigation.PushModalAsync(new MainPage());
                }
                else if(loginResult == LoginResult.UserNotFound)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "User not found", "OK");
                }
                else if (loginResult == LoginResult.WrongPassword)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Invalid email or password", "OK");
                }
                
            });

            NavigateToMainPageAsGuestCommand = new Command(async () =>
            {               
                await navigation.PushModalAsync(new MainPage());
            });

            NavigateToRegisterCommand = new Command(async () =>
            {
                await navigation.PushModalAsync(new RegisterPage());
            });
            

        }
    }
}
