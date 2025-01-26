using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CasusVictuzMobile.MVVM.Models;
using CasusVictuzMobile.MVVM.Views;
using CasusVictuzMobile.Services;
using CasusVictuzMobile.Session;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace CasusVictuzMobile.MVVM.ViewModels
{
    public partial class NotificationViewModel : ObservableObject
    {
        private ObservableCollection<Notification> _notifications;

        [ObservableProperty]
        private bool noNewNotifications; // dit wordt niet meer gebruikt

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
            NotificationService notificationService = new NotificationService();
            Notifications = new ObservableCollection<Notification>(notificationService.GetAllNotificationsByUserId(UserSession.Instance.UserId));            
            NoNewNotifications = Notifications.Count() == 0 ? true : false;           
        }

        [RelayCommand]
        public async Task NotificationTapped(Notification notification)
        {            
            if (notification != null)
            {
                notification.Seen = true;
                // kan opzich gewoon verwijderd worden uit db?
                notification.SaveOrUpdate();

                await App.Current.MainPage.Navigation.PushModalAsync(new EventDetailPage(notification.Event));


            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
