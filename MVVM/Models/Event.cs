using CasusVictuzMobile.Database.InterFaces;
using CasusVictuzMobile.Session;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusVictuzMobile.MVVM.Models
{
    [Table("Event")]
    public class Event : TableData
    {
        [SQLite.NotNull]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Spots { get; set; }
        public DateTime Date { get; set; }
        public bool IsAccepted { get; set; }
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
        [Ignore]
        public Location? Location { get; set; }

        [Ignore]
        public virtual ICollection<Registration>? Registrations { get; set; }
        //Moet mischien iets van opslag locatie komen voor foto's van revieuws, of bij inschrijving
        [Ignore]
        public string SpotsInfo
        {
            get
            {
                int occupiedSpots = Registrations.Count;
                return $"{occupiedSpots} van {Spots} bezet";
            }
        }
        [Ignore]
        public string RegistrationButtonColor
        {
            get
            {
                if (IsFull())
                    return "Gray"; // Grijs: vol
                if (IsUserRegistered(UserSession.Instance.LoggedInUser.Id))
                    return "Red"; // Rood: al ingeschreven
                return "Green"; // Groen: niet ingeschreven
            }
        }
        [Ignore]
        public string RegistrationButtonText
        {
            get
            {
                if (IsFull())
                    return "Dit evenement zit vol";
                if (IsUserRegistered(UserSession.Instance.LoggedInUser.Id))
                    return "Uitschrijven"; 
                return "Inschrijven"; 
            }
        }

        public bool IsUserRegistered(int userId)
        {
            return Registrations.Any(r => r.UserId == userId);
        }

        public bool IsFull()
        {
            return Registrations.Count >= Spots;
        }
        
        



        

        public void SaveOrUpdate()
        {
            App.EventRepository.DeleteEntity(this);
        }
        public void Delete()
        {
            App.EventRepository.DeleteEntity(this);
        }
        public static Event GetById(int id)
        {
            return App.EventRepository.GetEntity(id);
        }
        public static List<Event> GetAll()
        {
            return App.EventRepository.GetAllEntities();
        }
    }
}
