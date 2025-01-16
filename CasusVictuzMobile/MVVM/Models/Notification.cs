using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using CasusVictuzMobile.Database.InterFaces;

namespace CasusVictuzMobile.MVVM.Models
{
    [Table("Notification")]
    public class Notification: TableData
    {
        [SQLite.NotNull]
        public string? Title { get; set; }
        [SQLite.NotNull]
        public string? Message { get; set; }
        [SQLite.NotNull]
        public DateTime Date { get; set; }
        public bool Seen { get; set; }
        [Ignore]
        public Event? Event { get; set; }
        [Column("EventId")]
        public int? EventId { get; set; }
        [Ignore]
        public User? User { get; set; }
        [Column("UserId")]
        public int UserId { get; set; }

        public void SaveOrUpdate()
        {
            App.NotificationRepository.DeleteEntity(this);
        }
        public void Delete()
        {
            App.NotificationRepository.DeleteEntity(this);
        }
        public Notification GetById(int id)
        {
            return App.NotificationRepository.GetEnity(id);
        }
        public List<Notification> GetAll()
        {
            return App.NotificationRepository.GetAllEntities();
        }
    }
}
