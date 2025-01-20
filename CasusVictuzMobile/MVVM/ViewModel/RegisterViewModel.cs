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
    public partial class RegisterViewModel : ObservableObject
    {
        public ICommand RegisterAccountCommand { get; set; }
        //public INavigation Navigation { get; set; }
        public ICommand NavigateToLoginCommand { get; set; }

        [ObservableProperty]
        private string userName;
        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private string emailConfirm;
        [ObservableProperty]
        private string password;
        [ObservableProperty]
        private string passwordConfirm;


        public RegisterViewModel(INavigation navigation)
        {
            RegisterAccountCommand = new Command(async () =>
            {
                if (ValidateInputs())
                {
                    UserService userService = new UserService();
                    userService.Register(UserName, Email, Password);                    

                    await App.Current.MainPage.DisplayAlert("Success!", "Account succesfully registered.", "OK");
                    await navigation.PushModalAsync(new LoginPage());
                }
            });

            NavigateToLoginCommand = new Command(async () =>
            {
                await navigation.PushModalAsync(new LoginPage());
            });
        }

        public bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(emailConfirm) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(passwordConfirm))
            {
                App.Current.MainPage.DisplayAlert("Invalid", "Please fill in all entries", "OK");
                return false;
            }
            else
            {
                if(email != emailConfirm || password != passwordConfirm)
                {
                    App.Current.MainPage.DisplayAlert("Invalid", "Password or Email do not match.", "OK");
                    return false;
                }
                else
                {
                    return true;
                }                
            }
        }
    }
}
