using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CasusVictuzMobile.MVVM.Models;

namespace CasusVictuzMobile.MVVM.ViewModels
{
    public class NotificationViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Notification> _notifications;

        public ObservableCollection<Notification> Notifications
        {
            get => _notifications;
            set
            {
                _notifications = value;
                OnPropertyChanged();
            }
        }

        public NotificationViewModel()
        {
            // Hardcoded notifications
            Notifications = new ObservableCollection<Notification>
            {
                new Notification
                {
                    Title = "Nieuw Evenement!",
                    Message = "31 december - eindejaars gala!",
                    Date = DateTime.Now,
                    Seen = false
                },
                new Notification
                {
                    Title = "Bijna vol!",
                    Message = "Voetbal is bijna vol, reserveer je plek!",
                    Date = DateTime.Now.AddDays(-1),
                    Seen = true
                }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
