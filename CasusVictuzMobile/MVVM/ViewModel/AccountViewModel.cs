using CasusVictuzMobile.MVVM.Models;
using CasusVictuzMobile.MVVM.View;
using CasusVictuzMobile.MVVM.ViewModel;
using CasusVictuzMobile.MVVM.Views;
using CasusVictuzMobile.Services;
using CasusVictuzMobile.Session;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CasusVictuzMobile.MVVM.ViewModel
{
    public partial class AccountViewModel : ObservableObject
    {
        private readonly UserService _userService;
        private readonly RegistrationService _registrationService;
        private readonly INavigation _navigation;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string membershipStatus;

        [ObservableProperty]
        private string role;

        [ObservableProperty]
        private bool isGuest;

        [ObservableProperty]
        private ObservableCollection<EventRecap> eventRecaps = new ObservableCollection<EventRecap>();

        [RelayCommand]
        private async Task LogoutAsync()
        {
            await Logout();
        }

        [RelayCommand]
        private async Task OpenSignUpAsGuestModalAsync()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new SignUpMemberAccountModal());
        }

        [RelayCommand]
        private async Task NavigateToRecapDetailAsync(EventRecap recap)
        {
            if (recap != null)
            {
                await App.Current.MainPage.Navigation.PushModalAsync(new EventRecapDetailPage(recap));
            }
        }

        public AccountViewModel(INavigation navigation)
        {
            _userService = new UserService();
            _registrationService = new RegistrationService();
            _navigation = navigation;

            LoadUserData();
            LoadEventRecaps();
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

                IsGuest = UserSession.Instance.LoggedInUser.IsGuest;
            }
            else
            {
                Username = "Niet ingelogd";
                Email = "Niet beschikbaar";
                MembershipStatus = "Geen";
                Role = "Gast";
            }
        }

        private void LoadEventRecaps()
        {
            var registrations = _registrationService.GetAllRegistrationsByUserId(UserSession.Instance.LoggedInUser.Id);

            if (registrations.Count > 0)
            {
                var eventIds = registrations.Select(r => r.EventId).ToList();
                var pastEvents = Event.GetAll()
                                      .Where(e => eventIds.Contains(e.Id) && e.Date < DateTime.Now)
                                      .ToList();
                var eventDict = pastEvents.ToDictionary(e => e.Id);

                var allComments = Comment.GetAll()
                                         .Where(c => eventIds.Contains(c.EventRecapId))
                                         .ToList();
                var commentsDict = allComments.GroupBy(c => c.EventRecapId)
                                              .ToDictionary(g => g.Key, g => g.ToList());

                foreach (var registration in registrations)
                {
                    if (eventDict.TryGetValue(registration.EventId, out var eventEntity))
                    {
                        registration.Event = eventEntity;

                        var eventRecap = new EventRecap
                        {
                            Id = eventEntity.Id,
                            Event = eventEntity,
                            Comments = commentsDict.TryGetValue(registration.EventId, out var eventComments) ? eventComments : new List<Comment>(),
                            CreatedAt = eventEntity.Date
                        };

                        EventRecaps.Add(eventRecap);
                    }
                }
            }
        }


        [RelayCommand]
        public void EventRecapSelected(EventRecap recap)
        {
            if (recap != null)
            {
                // Navigeer naar de detailpagina van de geselecteerde EventRecap
                _navigation.PushAsync(new EventRecapDetailPage(recap));
            }
        }

        private async Task Logout()
        {
            UserSession.Instance.Logout();
            await _navigation.PushModalAsync(new LoginPage());
        }
    }
}
