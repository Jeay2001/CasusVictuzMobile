using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CasusVictuzMobile.Database.InterFaces;
using SQLite;

namespace CasusVictuzMobile.MVVM.Models
{
    public class Registration:TableData
    {
        public bool IsOrginizer { get; set; }
        public string? RevieuwMessage { get; set; } //terugblik berricht
        //Moet mischien iets van opslag locatie komen voor foto's, of bij event
        [Ignore]
        public Event? Event { get; set; }
        [NotNull]
        public int EventId { get; set; }
        [Ignore]
        public User? User { get; set; }
        [NotNull]
        public int UserId { get; set; }
    }
}
