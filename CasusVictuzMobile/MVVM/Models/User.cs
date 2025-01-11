using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusVictuzMobile.MVVM.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public bool IsMember { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsGuest { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
