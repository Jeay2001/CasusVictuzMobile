using CasusVictuzMobile.Database.InterFaces;
using CasusVictuzMobile.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusVictuzMobile.Services
{
    public class NotificationService
    {
        private readonly BaseRepository<Notification> _notificationRepository;

        public NotificationService()
        {
            _notificationRepository = new BaseRepository<Notification>();
        }

        //public void SaveNotification(Notification notification)
        //{
        //    _notificationRepository.SaveEntity(notification);
        //}

        public void CreateNewNotifications(Event e)
        {
            var users = App.UserRepository.GetAllEntities();
            foreach (var user in users)
            {
                var notification = new Notification
                {
                    Type = "New Event!",
                    Title = e.Name,
                    Message = e.Description,
                    Date = e.Date,
                    Seen = false,
                    Event = e,
                    EventId = e.Id,
                    User = user,
                    UserId = user.Id
                };
                _notificationRepository.SafeEntity(notification);
            }

        }

        public void CreateNewNotificationsForRemainingSpots(Event e)
        {
            var users = App.UserRepository.GetAllEntities();
            foreach (var user in users)
            {
                var notification = new Notification
                {
                    Type = "Registration almost full! 5 Spots left",
                    Title = e.Name,
                    Message = e.Description,
                    Date = e.Date,
                    Seen = false,
                    Event = e,
                    EventId = e.Id,
                    User = user,
                    UserId = user.Id
                };
                _notificationRepository.SafeEntity(notification);
            }

        }

        public List<Notification> GetAllNotificationsByUserId(int userId)
        {
            var allNotifications = App.NotificationRepository.GetAllEntities()
                .Where(n => n.UserId == userId)
                .ToList();
            
            foreach (var notification in allNotifications)
            {
                notification.Event = App.EventRepository.GetAllEntities()
                    .FirstOrDefault(e => e.Id == notification.EventId);
                notification.Event.Registrations = App.RegistrationRepository.GetAllEntities()
                    .Where(r => r.EventId == notification.EventId)
                    .ToList();
                notification.Event.Category = App.CategoryRepository.GetEntity(notification.Event.CategoryId);
                notification.Event.Location = App.LocationRepository.GetEntity(notification.Event.LocationId);
            }

            return allNotifications;
        }

    }
}
