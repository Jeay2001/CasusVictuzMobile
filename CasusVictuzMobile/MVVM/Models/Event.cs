using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusVictuzMobile.MVVM.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsAccepted { get; set; }
        public string Image { get; set; }
        public bool UrlPicture { get; set; }
        public string PictureLink { get; set; }
        public bool IsOnlyForMembers { get; set; } //Alleen voor leden, is mischien niet nodig als we alleen Lid functionaliteiten maken
        public bool IsPayed { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; }

}
}
