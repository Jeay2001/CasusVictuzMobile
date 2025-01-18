using CasusVictuzMobile.MVVM.Models;
using CasusVictuzMobile.Session;
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
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private User _currentUser;
        private ObservableCollection<Event> _futureAcceptedEvents;

        public ObservableCollection<Event> FutureAcceptedEvents
        {
            get => _futureAcceptedEvents;
            set
            {
                if (_futureAcceptedEvents != value)
                {
                    _futureAcceptedEvents = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainPageViewModel()
        {
            _currentUser = UserSession.Instance.LoggedInUser;
            LoadData();
        }

        private void LoadData()
        {
            User currentUser = UserSession.Instance.LoggedInUser;
            currentUser = User.GetById(currentUser.Id);

            var allEvents = Event.GetAll();

            /* If there are no events in the database, create some default events 
            if (allEvents.Count == 0)
            {
                Category cat1 = new Category { Id=0, Title = "ICT", Description = "ICT gerelateerde activiteiten" };
                App.CategoryRepository.SafeEntity(cat1);
                Category cat2 = new Category { Id=0, Title = "Sport", Description = "Evenementen waar sporten centraal staat" };
                App.CategoryRepository.SafeEntity(cat2);
                Models.Location loc1 = new MVVM.Models.Location { Id = 0, Name = "Hogeschool Zuyd B2.204" };
                App.LocationRepository.SafeEntity(loc1);
                Models.Location lo2 = new MVVM.Models.Location { Id = 0, Name = "Hogeschool Zuyd A1.123" };
                App.LocationRepository.SafeEntity(lo2);
                Models.Location loc3 = new MVVM.Models.Location { Id = 0, Name = "Hogeschool Zuyd Gymzaal" };
                App.LocationRepository.Equals(loc3);

                Event e1 = new Event { Name = "Voetbal", Description = "Dit is een voetbal toernooi", Date = DateTime.Now.AddDays(40), IsAccepted = true, CategoryId = cat2.Id, LocationId = loc3.Id, PictureLink = "https://images.unsplash.com/photo-1579952363873-27f3bade9f55?ixlib=rb-1.2.1&w=1000&q=80", Spots = 25 };
                App.EventRepository.SafeEntity(e1);
                Event e2 = new Event { Name = "Hackathon", Description = "Dit is een hackathon", Date = DateTime.Now.AddDays(50), IsAccepted = true, CategoryId = cat1.Id, LocationId = loc1.Id, PictureLink = "https://www.atulhost.com/wp-content/uploads/2021/02/hackathon.png", Spots = 40 };
                App.EventRepository.SafeEntity(e2);
                Event e3 = new Event { Name = "Basketbal", Description = "Dit is een basketbal toernooi", Date = DateTime.Now.AddDays(60), IsAccepted = true, CategoryId = cat2.Id, LocationId = loc3.Id, PictureLink = "https://i5.walmartimages.com/asr/8e33eda3-8982-4c51-9993-cffea55b31b7_3.cba592caf4424c5f68ead86773ca5612.png", Spots = 20 };
                App.EventRepository.SafeEntity(e3);
                allEvents.Add(e1);
                allEvents.Add(e2);
                allEvents.Add(e3);
            }
            */

            var futureEvents = allEvents.Where(e => e.Date > DateTime.Now).ToList();
            var futureAcceptedEvents = futureEvents.Where(e => e.IsAccepted).ToList();

            foreach (var e in futureAcceptedEvents)
            {
                var registration = Registration.GetAll();
                e.Category = Category.GetById(e.CategoryId);
                e.Location = MVVM.Models.Location.GetById(e.LocationId);
                e.Registrations = registration.Where(r => r.EventId == e.Id).ToList();
            }

            FutureAcceptedEvents = new ObservableCollection<Event>(futureAcceptedEvents);
        }


        public void ToggleRegistration(Event selectedEvent)
        {

            // Voeg registratie toe of verwijder
            if (selectedEvent.IsUserRegistered(_currentUser.Id))
            {
                var registration = selectedEvent.Registrations.FirstOrDefault(r => r.UserId == _currentUser.Id);
                App.RegistrationRepository.DeleteEntity(registration);
            }

            else if (selectedEvent.IsFull()) 
            {
                //eventement is vol
            }
                
            else
            {
                var newRegistration = new Registration
                {
                    UserId = _currentUser.Id,
                    EventId = selectedEvent.Id,
                };
                selectedEvent.Registrations.Add(newRegistration);
                App.RegistrationRepository.SafeEntity(newRegistration);
            }

            LoadData(); // Herladen van gegevens
        }

        public ICommand ToggleRegistrationCommand => new Command<Event>(ToggleRegistration);

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
