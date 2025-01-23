using CasusVictuzMobile.MVVM.Models;
using CasusVictuzMobile.Session;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusVictuzMobile.MVVM.ViewModel
{
    public class RegistrationsPageViewModel : ObservableObject
    {
        private ObservableCollection<Registration> _registrations;
        public ObservableCollection<Registration> Registrations
        {
            get => _registrations;
            set => SetProperty(ref _registrations, value);
        }

        public RegistrationsPageViewModel()
        {
            Registrations = new ObservableCollection<Registration>();
            LoadRegistrations();
        }


        private void LoadRegistrations()
        {
            var allRegistrations = App.RegistrationRepository.GetAllEntities()
                .Where(x => x.UserId == UserSession.Instance.UserId)
                .ToList();

            foreach (var registration in allRegistrations)
            {
                registration.Event = App.EventRepository.GetEntity(registration.EventId);
                registration.GenerateQRCode();
            }
            List<Registration> AllFutureRegistrations = allRegistrations.Where(x => x.Event.Date >= DateTime.Now).ToList();

            Registrations = new ObservableCollection<Registration>(
                AllFutureRegistrations.OrderBy(x => x.Event.Date)
            );
        }
    }
}