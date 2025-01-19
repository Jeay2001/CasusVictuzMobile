using CasusVictuzMobile.MVVM.View;
using CasusVictuzMobile.MVVM.Views;
using CasusVictuzMobile.Session;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusVictuzMobile.MVVM.ViewModel
{
    public partial class NavBarViewModel
    {

        public NavBarViewModel() { }





        [RelayCommand]
        public void NavigateToNotifications()
        {
            App.Current.MainPage.Navigation.PushModalAsync(new NotificationPage());
        }

        [RelayCommand]
        public void NavigateToEvents()
        {
            App.Current.MainPage.Navigation.PushModalAsync(new MainPage());
        }

        [RelayCommand]
        public void NavigateToAccount()
        {
            App.Current.MainPage.Navigation.PushModalAsync(new AccountPage());
        }


        [RelayCommand]
        public void TemporaryLogOut()
        {
            UserSession.Instance.Logout();
            App.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
        }

    }
}
