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



    }
}
