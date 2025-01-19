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
        [NotNull]
        public int EventId { get; set; }

        [Ignore]
        public virtual Event? Event { get; set; }

        [Ignore]
        public virtual ICollection<Comment>? Comments { get; set; }

        [NotNull]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // CRUD Operations
        public void SaveOrUpdate()
        {
            App.EventRecapRepository?.SaveOrUpdate(this);
        }

        public void Delete()
        {
            App.EventRecapRepository?.DeleteEntity(this);
        }

        public static EventRecap GetById(int id)
        {
            return App.EventRecapRepository?.GetEntity(id) ?? new EventRecap();
        }

        public static List<EventRecap> GetAll()
        {
            return App.EventRecapRepository?.GetAllEntities() ?? new List<EventRecap>();
        }
    }
}
