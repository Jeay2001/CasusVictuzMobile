using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CasusVictuzMobile.MVVM.Models;
using CasusVictuzMobile.Session;

namespace CasusVictuzMobile.MVVM.ViewModel
{
    public class CreateSuggestionViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _description;
        private int _spots;
        private DateOnly _date = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
        private TimeSpan _time = DateTime.Now.TimeOfDay;
        private string _pictureLink;


        public string GivenName
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(GivenName));
            }
        }

        public string GivenDescription
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(GivenDescription));
            }
        }

        public int GivenSpots
        {
            get => _spots;
            set
            {
                _spots = value;
                OnPropertyChanged(nameof(GivenSpots));
            }
        }

        public DateOnly GivenDate
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(GivenDate));
            }
        }

        public TimeSpan GivenTime
        {
            get => _time;
            set
            {
                _time = value;
                OnPropertyChanged(nameof(GivenTime));
            }
        }
        public string GivenPictureLink
        {
            get => _pictureLink;
            set
            {
                _pictureLink = value;
                OnPropertyChanged(nameof(GivenPictureLink));
            }
        }

        public ICommand SubmitCommand { get; }

        public CreateSuggestionViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }

        private void OnSubmit()
        {
            Event @event = new Event
            {
                Id = 0,
                Name = _name,
                Description = _description,
                Spots = _spots,
                IsAccepted = false,
                PictureLink = _pictureLink,
                Date = _date.ToDateTime(new TimeOnly(_time.Hours, _time.Minutes))
            };
            App.EventRepository.SafeEntity(@event);
            Registration registration = new Registration
            {
                Id = 0,
                UserId = UserSession.Instance.LoggedInUser.Id,
                EventId = @event.Id,
                IsOrginizer = false
            };
            App.RegistrationRepository.SafeEntity(registration);
            NavigateBack();
        }

        private async void NavigateBack()
        {
            await App.Current.MainPage.DisplayAlert("Suggestie is verzonden", "Je suggestie is succesvol verzonden.", "OK");
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
