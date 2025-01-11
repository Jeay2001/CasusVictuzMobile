using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusVictuzMobile.MVVM.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public bool IsOrginizer { get; set; }
        public string? RevieuwMessage { get; set; } //terugblik berricht
        //Moet mischien iets van opslag locatie komen voor foto's
        public Event Event { get; set; }
        public int EventId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
