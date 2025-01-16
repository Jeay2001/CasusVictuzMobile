using CasusVictuzMobile.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CasusVictuzMobile.MVVM.ViewModel
{
    public class LoginViewModel
    {
        public ICommand NavigateToMainPageCommand { get; set; }
        //public INavigation Navigation { get; set; }
        public ICommand NavigateToRegisterCommand { get; set; }
        public LoginViewModel(INavigation navigation)
        {
            NavigateToMainPageCommand = new Command(async () =>
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
