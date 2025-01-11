using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusVictuzMobile.MVVM.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public bool Seen { get; set; }
        public Event? Event { get; set; }
        public int? EventId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
