using CasusVictuzMobile.Database;
using CasusVictuzMobile.Database.InterFaces;
using CasusVictuzMobile.MVVM.Models;
using CasusVictuzMobile.MVVM.View;
using CasusVictuzMobile.MVVM.Views;
using CasusVictuzMobile.Services;
using CasusVictuzMobile.Session;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace CasusVictuzMobile.MVVM.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        private User _currentUser;

        [ObservableProperty]
        private ObservableCollection<Event> displayedEvents;
        public ICommand NavigateToDetails { get; set; }
        public INavigation Navigation { get; set; }

        public MainPageViewModel(INavigation navigation)
        {
            _currentUser = UserSession.Instance.LoggedInUser;

            try
            {
                LoadData();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("error::");
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }

            NavigateToDetails = new Command<Event>(async (selectedEvent) =>
            {
                await navigation.PushModalAsync(new EventDetailPage(selectedEvent));
            });
        }

        private void LoadData()
        {
            //BaseRepository<Event> baseRepository = new BaseRepository<Event>();
            //baseRepository.connection.DeleteAll<Event>();
            //baseRepository.connection.DeleteAll<Notification>();
            //baseRepository.connection.DeleteAll<Registration>();
            var allEvents = Event.GetAll();
            //If there are no events in the database, create some default events
            if (allEvents.Count == 0)
            {
                Category cat1 = new Category { Id = 0, Title = "ICT", Description = "ICT gerelateerde activiteiten" };
                App.CategoryRepository.SafeEntity(cat1);
                Category cat2 = new Category { Id = 0, Title = "Sport", Description = "Evenementen waar sporten centraal staat" };
                App.CategoryRepository.SafeEntity(cat2);
                Models.Location loc1 = new MVVM.Models.Location { Id = 0, Name = "Hogeschool Zuyd B2.204" };
                App.LocationRepository.SafeEntity(loc1);
                Models.Location lo2 = new MVVM.Models.Location { Id = 0, Name = "Hogeschool Zuyd A1.123" };
                App.LocationRepository.SafeEntity(lo2);
                Models.Location loc3 = new MVVM.Models.Location { Id = 0, Name = "Hogeschool Zuyd Gymzaal" };
                App.LocationRepository.SafeEntity(loc3);

                //App.EventRepository.connection.DeleteAll<Event>();
                //App.NotificationRepository.connection.DeleteAll<Notification>();
                //return;

                Event e1 = new Event { Name = "Voetbal", Description = "Dit is een voetbal toernooi", Date = DateTime.Now.AddDays(40), IsAccepted = true, CategoryId = cat2.Id, LocationId = loc3.Id, PictureLink = "https://images.unsplash.com/photo-1579952363873-27f3bade9f55?ixlib=rb-1.2.1&w=1000&q=80", Spots = 25 };
                e1.CreateEvent();
                Event e2 = new Event { Name = "Hackathon", Description = "Dit is een hackathon", Date = DateTime.Now.AddDays(50), IsAccepted = true, CategoryId = cat1.Id, LocationId = loc1.Id, PictureLink = "https://www.atulhost.com/wp-content/uploads/2021/02/hackathon.png", Spots = 40 };
                e2.CreateEvent();
                Event e3 = new Event { Name = "Basketbal", Description = "Dit is een basketbal toernooi", Date = DateTime.Now.AddDays(60), IsAccepted = true, CategoryId = cat2.Id, LocationId = loc3.Id, PictureLink = "https://i5.walmartimages.com/asr/8e33eda3-8982-4c51-9993-cffea55b31b7_3.cba592caf4424c5f68ead86773ca5612.png", Spots = 20 };
                e3.CreateEvent();
                allEvents.Add(e1);
                allEvents.Add(e2);
                allEvents.Add(e3);
                // Event 4: Workshop on AI
                Event e4 = new Event
                {
                    Name = "AI Workshop",
                    Description = "Learn the basics of Artificial Intelligence and Machine Learning in this hands-on workshop.",
                    Date = DateTime.Now.AddDays(70),
                    IsAccepted = true,
                    IsOnlyForMembers = true,
                    CategoryId = cat1.Id, // ICT category
                    LocationId = loc1.Id, // Hogeschool Zuyd B2.204
                    PictureLink = "https://picsum.photos/seed/ai/400/300", // Random AI-related image
                    Spots = 30
                };
                e4.CreateEvent();

                // Event 5: Yoga Session
                Event e5 = new Event
                {
                    Name = "Yoga & Meditation",
                    Description = "Relax and rejuvenate with a guided yoga and meditation session.",
                    Date = DateTime.Now.AddDays(80),
                    IsAccepted = true,
                    CategoryId = cat2.Id, // Sport category
                    LocationId = loc3.Id, // Hogeschool Zuyd Gymzaal
                    PictureLink = "https://picsum.photos/seed/yoga/400/300", // Random yoga-related image
                    Spots = 20
                };
                e5.CreateEvent();

                // Event 6: Coding Bootcamp
                Event e6 = new Event
                {
                    Name = "Coding Bootcamp",
                    Description = "A 2-day intensive coding bootcamp to sharpen your programming skills.",
                    Date = DateTime.Now.AddDays(90),
                    IsAccepted = true,
                    CategoryId = cat1.Id, // ICT category
                    LocationId = loc1.Id, // Hogeschool Zuyd B2.204
                    PictureLink = "https://picsum.photos/seed/coding/400/300", // Random coding-related image
                    Spots = 25
                };
                e6.CreateEvent();

                // Event 7: Basketball Tournament
                Event e7 = new Event
                {
                    Name = "3v3 Basketball Tournament",
                    Description = "Compete in a fast-paced 3v3 basketball tournament. Prizes for the winners!",
                    Date = DateTime.Now.AddDays(100),
                    IsAccepted = true,
                    CategoryId = cat2.Id, // Sport category
                    LocationId = loc3.Id, // Hogeschool Zuyd Gymzaal
                    PictureLink = "https://picsum.photos/seed/basketball/400/300", // Random basketball-related image
                    Spots = 15
                };
                e7.CreateEvent();

                // Event 8: Cybersecurity Seminar
                Event e8 = new Event
                {
                    Name = "Cybersecurity Seminar",
                    Description = "Learn about the latest trends and best practices in cybersecurity.",
                    Date = DateTime.Now.AddDays(110),
                    IsAccepted = true,
                    IsOnlyForMembers = true,
                    CategoryId = cat1.Id, // ICT category
                    LocationId = loc1.Id, // Hogeschool Zuyd B2.204
                    PictureLink = "https://picsum.photos/seed/cybersecurity/400/300", // Random cybersecurity-related image
                    Spots = 35
                };
                e8.CreateEvent();
                
                Event e9 = new Event
                {
                    Name = "Past Event",
                    Description = "Past event for testing.",
                    Date = DateTime.Now.AddSeconds(30),
                    IsAccepted = true,
                    IsOnlyForMembers = true,
                    CategoryId = cat1.Id, // ICT category
                    LocationId = loc1.Id, // Hogeschool Zuyd B2.204
                    PictureLink = "https://picsum.photos//400/300",
                    Spots = 35
                };
                e9.CreateEvent();

                Registration registration = new Registration
                {
                    IsOrginizer = false,
                    UserId = UserSession.Instance.UserId,
                    EventId = e9.Id
                };

                // Add the new events to the list
                allEvents.Add(e4);
                allEvents.Add(e5);
                allEvents.Add(e6);
                allEvents.Add(e7);
                allEvents.Add(e8);
                allEvents.Add(e9);

            }

           
            var futureEvents = allEvents.Where(e => e.Date > DateTime.Now).ToList();
            var _futureAcceptedEvents = futureEvents.Where(e => e.IsAccepted).ToList();
            if (_currentUser.IsGuest)
            {
                //Gast krijgt alleen van de komende 2 weken evenementen te zien om voor in te schrijven
                _futureAcceptedEvents = _futureAcceptedEvents
                 .Where(e => e.Date >= DateTime.Now && e.Date <= DateTime.Now.AddDays(14))
                 .ToList();
            }

            foreach (var e in _futureAcceptedEvents)
            {
                var registration = Registration.GetAll();
                e.Category = Category.GetById(e.CategoryId);
                e.Location = MVVM.Models.Location.GetById(e.LocationId);
                e.Registrations = registration.Where(r => r.EventId == e.Id).ToList();
            }


            DisplayedEvents = new ObservableCollection<Event>(_futureAcceptedEvents);
        }


        public void ToggleRegistration(Event selectedEvent)
        {
            if (selectedEvent.IsOnlyForMembers && _currentUser.IsGuest)
            {
                App.Current.MainPage.DisplayAlert("Alleen leden", "Dit evenement is alleen voor leden", "OK");
                return;
            }


            if (selectedEvent.IsUserRegistered(_currentUser.Id))
            {
                var registration = selectedEvent.Registrations.FirstOrDefault(r => r.UserId == _currentUser.Id);
                App.RegistrationRepository.DeleteEntity(registration);
                LoadData(); // Reload data
                return;
            }


            if (selectedEvent.IsFull())
            {
                App.Current.MainPage.DisplayAlert("Evenement is vol", "Dit evenement is al vol.", "OK");
                return;
            }


            if (_currentUser.IsGuest)
            {
                RegistrationService registrationService = new RegistrationService();
                int registrationCount = registrationService.GetAllRegistrationsByUserId(_currentUser.Id).Count();

                if (registrationCount >= Constants.MAXIMUM_REGISTRATIONS_FOR_GUEST)
                {
                    App.Current.MainPage.DisplayAlert("Maximale inschrijvingen", "Je hebt je maximale inschrijvingen als gast bereikt", "OK");
                    return;
                }
            }


            var newRegistration = new Registration
            {
                UserId = _currentUser.Id,
                EventId = selectedEvent.Id,
            };
            selectedEvent.Registrations.Add(newRegistration);
            App.RegistrationRepository.SafeEntity(newRegistration);

            if(selectedEvent.Spots - selectedEvent.Registrations.Count == 5)
            {                
                NotificationService notificationService = new NotificationService();
                notificationService.CreateNewNotificationsForRemainingSpots(selectedEvent);
            }

            LoadData(); // Reload data
        }



        [ObservableProperty]
        private bool spotsAvailable;
        [ObservableProperty]
        private bool membersOnly;
        [ObservableProperty]
        private bool paidEvent;
        [ObservableProperty]
        private Category selectedCategory;
        [ObservableProperty]
        private Models.Location selectedLocation;

        public List<Category> AllCategories { get; } = new List<Category> { null }.Concat(App.CategoryRepository.GetAllEntities()).ToList();
        public List<Models.Location> AllLocations { get; } = new List<Models.Location> { null }.Concat(App.LocationRepository.GetAllEntities()).ToList();



        [RelayCommand]
        public async void OpenFilter()
        {
            var eventFilterPicker = new EventFilterPicker
            {
                BindingContext = this
            };

            await Application.Current.MainPage.Navigation.PushModalAsync(eventFilterPicker);
        }

        [RelayCommand]
        public async void OpenSuggestion()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new EventSuggestionPage());
        }

        [RelayCommand]
        public async Task ApplyFilter()
        {
            var allEvents = Event.GetAll().Select(e =>
            {
                e.Category = Category.GetById(e.CategoryId);
                e.Location = MVVM.Models.Location.GetById(e.LocationId);
                e.Registrations = Registration.GetAll().Where(r => r.EventId == e.Id).ToList();
                return e;
            }).ToList();

            if (_currentUser.IsGuest)
            {
                allEvents = allEvents
                 .Where(e => e.Date >= DateTime.Now && e.Date <= DateTime.Now.AddDays(14))
                 .ToList();
            }


            var filteredEvents = allEvents.AsEnumerable();


            if (SpotsAvailable)
            {
                filteredEvents = filteredEvents.Where(e => !e.IsFull());
            }

            if (MembersOnly)
            {
                filteredEvents = filteredEvents.Where(e => e.IsOnlyForMembers);
            }

            if (PaidEvent)
            {
                filteredEvents = filteredEvents.Where(e => e.IsPayed);
            }

            if (SelectedCategory != null)
            {
                filteredEvents = filteredEvents.Where(e => e.Category.Id == SelectedCategory.Id);
            }

            if (SelectedLocation != null)
            {
                filteredEvents = filteredEvents.Where(e => e.Location.Id == SelectedLocation.Id);
            }



            if (!filteredEvents.Any())
            {
                DisplayedEvents = new ObservableCollection<Event>();
            }
            else
            {
                DisplayedEvents = new ObservableCollection<Event>(filteredEvents.ToList());
            }

            await CloseFilter();
        }


        [RelayCommand]
        public async Task CloseFilter()
        {
            await App.Current.MainPage.Navigation.PopModalAsync();
        }

        [RelayCommand]
        public async void OpenSort()
        {
            var eventSortPicker = new EventSortPicker()
            {
                BindingContext = this
            };

            await Application.Current.MainPage.Navigation.PushModalAsync(eventSortPicker);
        }

        private bool _isUpdating;

        [ObservableProperty]
        private bool sortDateAscending;

        [ObservableProperty]
        private bool sortDateDescending;

        [ObservableProperty]
        private bool sortSignupsAscending;

        [ObservableProperty]
        private bool sortSignupsDescending;

        private void UncheckAllExcept(string selectedOption)
        {
            _isUpdating = true;

            SortDateAscending = selectedOption == nameof(SortDateAscending);
            SortDateDescending = selectedOption == nameof(SortDateDescending);
            SortSignupsAscending = selectedOption == nameof(SortSignupsAscending);
            SortSignupsDescending = selectedOption == nameof(SortSignupsDescending);

            _isUpdating = false;
        }

        partial void OnSortDateAscendingChanged(bool value)
        {
            if (!_isUpdating && value) UncheckAllExcept(nameof(SortDateAscending));
        }

        partial void OnSortDateDescendingChanged(bool value)
        {
            if (!_isUpdating && value) UncheckAllExcept(nameof(SortDateDescending));
        }

        partial void OnSortSignupsAscendingChanged(bool value)
        {
            if (!_isUpdating && value) UncheckAllExcept(nameof(SortSignupsAscending));
        }

        partial void OnSortSignupsDescendingChanged(bool value)
        {
            if (!_isUpdating && value) UncheckAllExcept(nameof(SortSignupsDescending));
        }

        [RelayCommand]
        private async void ApplySorting()
        {
            var allEvents = Event.GetAll().Select(e =>
            {
                e.Category = Category.GetById(e.CategoryId);
                e.Location = MVVM.Models.Location.GetById(e.LocationId);
                e.Registrations = Registration.GetAll().Where(r => r.EventId == e.Id).ToList();
                return e;
            }).ToList();

            if (_currentUser.IsGuest)
            {
                allEvents = allEvents
                 .Where(e => e.Date >= DateTime.Now && e.Date <= DateTime.Now.AddDays(14))
                 .ToList();
            }

            var filteredEvents = allEvents.AsEnumerable();  

            if (SortDateAscending)
            {
                filteredEvents = filteredEvents.OrderBy(e => e.Date);
            }
            else if (SortDateDescending)
            {
                filteredEvents = filteredEvents.OrderByDescending(e => e.Date);
            }
            else if (SortSignupsAscending)
            {
                filteredEvents = filteredEvents.OrderBy(e => e.Registrations.Count);
            }
            else if (SortSignupsDescending)
            {
                filteredEvents = filteredEvents.OrderByDescending(e => e.Registrations.Count);
            }

            if (!filteredEvents.Any())
            {
                DisplayedEvents = new ObservableCollection<Event>();
            }
            else
            {
                DisplayedEvents = new ObservableCollection<Event>(filteredEvents.ToList());
            }

            await CloseFilter();
        }

        [RelayCommand]
        public void Reset()
        {
            LoadData();
        }

        public ICommand ToggleRegistrationCommand => new Command<Event>(ToggleRegistration);

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
