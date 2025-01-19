using CasusVictuzMobile.MVVM.Models;
using CasusVictuzMobile.Services;
using CasusVictuzMobile.Session;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CasusVictuzMobile.MVVM.ViewModel
{
    public partial class AccountViewModel : ObservableObject
    {
        private readonly UserService _userService;
        private readonly INavigation _navigation;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string membershipStatus;

        [ObservableProperty]
        private string role;

        public ICommand LogoutCommand { get; }

        public AccountViewModel(INavigation navigation)
        {
            _userService = new UserService();
            _navigation = navigation;

            LogoutCommand = new RelayCommand(async () => await LogoutAsync());

            LoadUserData();
        }

        private void LoadUserData()
        {
            if (UserSession.Instance.IsLoggedIn && UserSession.Instance.LoggedInUser != null)
            {
                Username = UserSession.Instance.LoggedInUser.Username ?? "Onbekend";
                Email = UserSession.Instance.LoggedInUser.Email ?? "Onbekend";
                MembershipStatus = UserSession.Instance.LoggedInUser.IsMember ? "Lid" :
                                   UserSession.Instance.LoggedInUser.IsAdmin ? "Admin" : "Gast";
                Role = UserSession.Instance.LoggedInUser.IsAdmin ? "Administrator" :
                       UserSession.Instance.LoggedInUser.IsMember ? "Lid" : "Gast";
            }
            else
            {
                Username = "Niet ingelogd";
                Email = "Niet beschikbaar";
                MembershipStatus = "Geen";
                Role = "Gast";
            }
        }

        private async Task LogoutAsync()
        {
            UserSession.Instance.Logout();
            //await _navigation.PushModalAsync(new LoginPage());
        }
    }
}
