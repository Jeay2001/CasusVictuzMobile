// EventDetailViewModel.cs
using CasusVictuzMobile.Database.InterFaces;
using CasusVictuzMobile.MVVM.Models;
using CasusVictuzMobile.Services;
using CasusVictuzMobile.Session;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CasusVictuzMobile.MVVM.ViewModels
{
    public class EventDetailViewModel : INotifyPropertyChanged
    {
        private readonly RegistrationService _registrationService;

        private bool _isDescriptionExpanded;
        private string _displayedDescription;
        private string _registrationButtonText;
        private string _registrationButtonColorHex;
        private int _registeredUsers;
        private string _spotsInfo;
        private bool _isSeeAllVisible;

        public Event CurrentEvent { get; private set; }

        public string DisplayedDescription
        {
            get => _displayedDescription;
            set
            {
                _displayedDescription = value;
                OnPropertyChanged();
            }
        }

        public bool IsDescriptionExpanded
        {
            get => _isDescriptionExpanded;
            set
            {
                _isDescriptionExpanded = value;
                OnPropertyChanged();
            }
        }

        public string SpotsInfo
        {
            get => _spotsInfo;
            set
            {
                _spotsInfo = value;
                OnPropertyChanged();
            }
        }

        public string RegistrationButtonText
        {
            get => _registrationButtonText;
            set
            {
                _registrationButtonText = value;
                OnPropertyChanged();
            }
        }

        public string RegistrationButtonColorHex
        {
            get => _registrationButtonColorHex;
            set
            {
                _registrationButtonColorHex = value;
                OnPropertyChanged();
            }
        }

        public bool IsSeeAllVisible
        {
            get => _isSeeAllVisible;
            set
            {
                _isSeeAllVisible = value;
                OnPropertyChanged();
            }
        }

        // New property for "See All" button text
        public string SeeAllButtonText
        {
            get => IsDescriptionExpanded ? "See Less" : "See All";
        }

        public ICommand ToggleDescriptionCommand { get; }
        public ICommand RegisterCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public EventDetailViewModel(Event selectedEvent)
        {
            CurrentEvent = selectedEvent;
            _registrationService = new RegistrationService();

            InitializeDescription();
            UpdateSpotsInfo();
            UpdateRegistrationButton();

            ToggleDescriptionCommand = new Command(ToggleDescription);
            RegisterCommand = new Command(async () => await RegisterOrUnregisterAsync());
        }

        private void InitializeDescription()
        {
            if (!string.IsNullOrEmpty(CurrentEvent.Description) && CurrentEvent.Description.Length > 200)
            {
                DisplayedDescription = CurrentEvent.Description.Substring(0, 200) + "...";
                IsDescriptionExpanded = false;
                IsSeeAllVisible = true;
            }
            else
            {
                DisplayedDescription = CurrentEvent.Description;
                IsDescriptionExpanded = true;
                IsSeeAllVisible = false;
            }
        }

        private void ToggleDescription()
        {
            if (IsDescriptionExpanded)
            {
                // Collapse
                DisplayedDescription = CurrentEvent.Description.Length > 200
                    ? CurrentEvent.Description.Substring(0, 200) + "..."
                    : CurrentEvent.Description;
                IsDescriptionExpanded = false;
            }
            else
            {
                // Expand
                DisplayedDescription = CurrentEvent.Description;
                IsDescriptionExpanded = true;
            }
            OnPropertyChanged(nameof(SeeAllButtonText));
        }

        private void UpdateSpotsInfo()
        {
            _registeredUsers = CurrentEvent.Registrations?.Count ?? 0;
            SpotsInfo = $"{_registeredUsers} van {CurrentEvent.Spots} bezet";
        }

        private void UpdateRegistrationButton()
        {
            if (CurrentEvent.IsFull())
            {
                RegistrationButtonText = "Dit evenement zit vol";
                RegistrationButtonColorHex = "#808080"; // Gray
            }
            else if (UserSession.Instance.IsLoggedIn && CurrentEvent.IsUserRegistered(UserSession.Instance.LoggedInUser.Id))
            {
                RegistrationButtonText = "Uitschrijven";
                RegistrationButtonColorHex = "#FF0000"; // Red
            }
            else
            {
                RegistrationButtonText = "Inschrijven";
                RegistrationButtonColorHex = "#00FF00"; // Green
            }
        }

        private async Task RegisterOrUnregisterAsync()
        {
            if (!UserSession.Instance.IsLoggedIn)
            {
                await Application.Current.MainPage.DisplayAlert("Niet ingelogd", "Log in om je in te schrijven voor evenementen.", "OK");
                return;
            }

            if (CurrentEvent.IsFull())
            {
                await Application.Current.MainPage.DisplayAlert("Vol", "Dit evenement zit vol.", "OK");
                return;
            }

            if (CurrentEvent.IsUserRegistered(UserSession.Instance.LoggedInUser.Id))
            {
                // Unregister
                bool confirm = await Application.Current.MainPage.DisplayAlert("Bevestigen", "Weet je zeker dat je je wilt uitschrijven?", "Ja", "Nee");
                if (confirm)
                {
                    bool success = await _registrationService.UnregisterAsync(CurrentEvent.Id, UserSession.Instance.LoggedInUser.Id);
                    if (success)
                    {
                        // Remove the registration from the local collection
                        var registration = CurrentEvent.Registrations.FirstOrDefault(r => r.UserId == UserSession.Instance.LoggedInUser.Id);
                        if (registration != null)
                        {
                            CurrentEvent.Registrations.Remove(registration);
                        }

                        UpdateSpotsInfo();
                        UpdateRegistrationButton();
                        await Application.Current.MainPage.DisplayAlert("Succes", "Je bent uitgeschreven van het evenement.", "OK");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Fout", "Er is een fout opgetreden bij het uitschrijven.", "OK");
                    }
                }
            }
            else
            {
                // Register
                bool success = await _registrationService.RegisterAsync(CurrentEvent.Id, UserSession.Instance.LoggedInUser.Id);
                if (success)
                {
                    // Add the registration to the local collection
                    var newRegistration = new Registration { EventId = CurrentEvent.Id, UserId = UserSession.Instance.LoggedInUser.Id };
                    if (CurrentEvent.Registrations == null)
                        CurrentEvent.Registrations = new ObservableCollection<Registration>();
                    CurrentEvent.Registrations.Add(newRegistration);

                    UpdateSpotsInfo();
                    UpdateRegistrationButton();
                    await Application.Current.MainPage.DisplayAlert("Succes", "Je bent ingeschreven voor het evenement.", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Fout", "Er is een fout opgetreden bij het inschrijven.", "OK");
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
