using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using SQLite;
using CasusVictuzMobile.Database.InterFaces;

namespace CasusVictuzMobile.MVVM.Models
{
    [Table("EventRecap")]
    public class EventRecap : TableData
    {

        [Ignore]
        public virtual Event? Event { get; set; }

        [Ignore]
        public virtual ICollection<Comment>? Comments { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // CRUD Operations
        public void SaveOrUpdate()
        {
            App.EventRecapRepository.SafeEntity(this);
        }

        public void Delete()
        {
            App.EventRecapRepository.DeleteEntity(this);
        }

        public static EventRecap GetById(int id)
        {
            return App.EventRecapRepository.GetEntity(id);
        }

        public static List<EventRecap> GetAll()
        {
            return App.EventRecapRepository.GetAllEntities();
        }
    }
}
