// EventDetailViewModel.cs
using CasusVictuzMobile.Database.InterFaces;
using CasusVictuzMobile.MVVM.Models;
using CasusVictuzMobile.Services;
using CasusVictuzMobile.Session;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CasusVictuzMobile.MVVM.ViewModels
{
    public partial class EventDetailViewModel : ObservableObject
    {
        private readonly RegistrationService _registrationService;
        private int _registeredUsers;

        public Event CurrentEvent { get; private set; }

        [ObservableProperty]
        private string _displayedDescription;

        [ObservableProperty]
        private bool _isDescriptionExpanded;

        [ObservableProperty]
        private string _spotsInfo;

        [ObservableProperty]
        private string _registrationButtonText;

        [ObservableProperty]
        private string _registrationButtonColorHex;

        [ObservableProperty]
        private bool _isSeeAllVisible;

        // Property for "See All" button text
        public string SeeAllButtonText => IsDescriptionExpanded ? "Zie Minder" : "Zie Alles";

        public ICommand ToggleDescriptionCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand ShareInstagramCommand { get; }
        public ICommand ShareXCommand { get; }
        public ICommand ShareWhatsAppCommand { get; }

        public EventDetailViewModel(Event selectedEvent)
        {
            CurrentEvent = selectedEvent;
            _registrationService = new RegistrationService();
            InitializeDescription();
            UpdateSpotsInfo();
            UpdateRegistrationButton();

            ToggleDescriptionCommand = new RelayCommand(ToggleDescription);
            RegisterCommand = new AsyncRelayCommand(RegisterOrUnregisterAsync);
            ShareInstagramCommand = new AsyncRelayCommand(() => ShareEventAsync("Instagram"));
            ShareXCommand = new AsyncRelayCommand(() => ShareEventAsync("X (Twitter)"));
            ShareWhatsAppCommand = new AsyncRelayCommand(() => ShareEventAsync("WhatsApp"));
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
                RegistrationButtonColorHex = "#808080"; // Grijs
            }
            else if (CurrentEvent.IsUserRegistered(UserSession.Instance.LoggedInUser.Id))
            {
                RegistrationButtonText = "Uitschrijven";
                RegistrationButtonColorHex = "#FF0000"; // Rood
            }
            else
            {
                RegistrationButtonText = "Inschrijven";
                RegistrationButtonColorHex = "#00FF00"; // Groen
            }
        }

        private async Task RegisterOrUnregisterAsync()
        {
            if (CurrentEvent.IsUserRegistered(UserSession.Instance.LoggedInUser.Id))
            {
                // Gebruiker uitschrijven
                var registration = CurrentEvent.Registrations
                    .FirstOrDefault(r => r.UserId == UserSession.Instance.LoggedInUser.Id);
                if (registration != null)
                {
                    CurrentEvent.Registrations.Remove(registration);
                    App.RegistrationRepository.DeleteEntity(registration);
                }
            }
            else if (!CurrentEvent.IsFull())
            {
                // Gebruiker inschrijven
                var newRegistration = new Registration
                {
                    UserId = UserSession.Instance.LoggedInUser.Id,
                    EventId = CurrentEvent.Id,
                };
                CurrentEvent.Registrations.Add(newRegistration);
                App.RegistrationRepository.SafeEntity(newRegistration);
            }

            // Werk knoppen en informatie bij
            UpdateSpotsInfo();
            UpdateRegistrationButton();
        }

        private async Task ShareEventAsync(string platform)
        {
            string eventName = CurrentEvent.Name;
            string message = $"Hey! Check out this event: {eventName}. It's going to be awesome!";

            try
            {
                await Share.Default.RequestAsync(new ShareTextRequest
                {
                    Text = message,
                    Title = $"Deel via {platform}"
                });
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Fout", $"Er is een fout opgetreden bij het delen via {platform}: {ex.Message}", "OK");
            }
        }
    }
}
