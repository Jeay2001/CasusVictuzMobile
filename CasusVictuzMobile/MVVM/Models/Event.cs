using CasusVictuzMobile.Database.InterFaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusVictuzMobile.MVVM.Models
{
    public class Event : TableData
    {
        [SQLite.NotNull]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsAccepted { get; set; }
        public bool UrlPicture { get; set; }
        public string? PictureLink { get; set; }
        public bool IsOnlyForMembers { get; set; } //Alleen voor leden, is mischien niet nodig als we alleen Lid functionaliteiten maken
        public bool IsPayed { get; set; }
        public double Price { get; set; }
        [Column("CategoryId")]
        public int CategoryId { get; set; }
        [Ignore]
        public Category? Category { get; set; }
        [Column("LocationId")]
        public int LocationId { get; set; }
        public Location? Location { get; set; }

        [Ignore]
        public virtual ICollection<Registration>? Registrations { get; set; }
        //Moet mischien iets van opslag locatie komen voor foto's van revieuws, of bij inschrijving

    }
}
