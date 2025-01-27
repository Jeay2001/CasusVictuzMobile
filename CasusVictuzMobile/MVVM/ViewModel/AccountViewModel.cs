using CasusVictuzMobile.MVVM.Models;
using CasusVictuzMobile.MVVM.View;
using CasusVictuzMobile.MVVM.Views; // Zorg ervoor dat dit de juiste namespace is
using CasusVictuzMobile.Services;
using CasusVictuzMobile.Session;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

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

        // RelayCommand attribuut genereert automatisch de command property
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

        // RelayCommand met parameter voor navigatie naar recap detail
        [RelayCommand]
        private async Task NavigateToRecapDetailAsync(EventRecap recap)
        {
            if (recap != null)
            {
                await _navigation.PushAsync(new EventRecapDetailPage(recap));
            }
        }

        // Constructor
        public AccountViewModel(INavigation navigation)
        {
            _userService = new UserService();
            _registrationService = new RegistrationService();
            _navigation = navigation;

            // Commands worden automatisch gegenereerd door [RelayCommand] attribuut
            // LogoutCommand wordt automatisch gegenereerd als LogoutAsync method

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
                // Deze conditie wordt nooit gebruikt. UserSession.Instance is ook ingevuld als guest
                Username = "Niet ingelogd";
                Email = "Niet beschikbaar";
                MembershipStatus = "Geen";
                Role = "Gast";
            }
        }

        private void LoadEventRecaps()
        {
            var registrations = _registrationService.GetAllRegistrationsByUserId(UserSession.Instance.LoggedInUser.Id);
            if (registrations.Count < 1)
            {
                // Seed data voor een vorig EventRecap
                var pastEvent = new Event
                {
                    Id = 999, // Zorg voor een unieke ID of haal deze op uit de database
                    Name = "Terugblik Evenement 2024",
                    Description = "Een terugblik op ons succesvolle evenement in 2024.",
                    Date = DateTime.UtcNow.AddMonths(-6),
                    Spots = 100,
                    IsAccepted = true,
                    IsOnlyForMembers = false,
                    IsPayed = false,
                    Price = 0,
                    CategoryId = 1,
                    LocationId = 1,
                    // Initialiseer andere benodigde eigenschappen indien nodig
                };

                var eventRecap = new EventRecap
                {
                    Id = 1001, // Zorg voor een unieke ID of haal deze op uit de database
                    Event = pastEvent,
                    Comments = new List<Comment>
                    {
                        new Comment { Content = "Geweldig evenement!", UserId = UserSession.Instance.LoggedInUser.Id },
                        new Comment { Content = "Ik heb genoten van de sessies.", UserId = UserSession.Instance.LoggedInUser.Id }
                    },
                    CreatedAt = pastEvent.Date.AddDays(1)
                };

                // Voeg de seed data toe aan de collectie
                EventRecaps.Add(eventRecap);
            }
            else
            {
                // Optioneel: laad daadwerkelijke EventRecaps uit de database
                // Voor eenvoud voegen we alleen seed data toe als er geen registraties zijn
            }
        }

        private async Task Logout()
        {
            UserSession.Instance.Logout();
            await _navigation.PushModalAsync(new LoginPage());
        }
    }
}
