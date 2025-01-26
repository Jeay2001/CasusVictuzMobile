using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using CasusVictuzMobile.Database.InterFaces;

namespace CasusVictuzMobile.MVVM.Models
{
    [Table("User")]
    public class User:TableData
    {

        //Moet mischien nog aangepast worden voor veilig inlog systeem in verband met password
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public bool IsMember { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsGuest { get; set; }
        [Ignore]
        public virtual ICollection<Registration>? Registrations { get; set; }
        [Ignore]
        public virtual ICollection<Notification>? Notifications { get; set; }

        public void SaveOrUpdate()
        {
            App.UserRepository.SafeEntity(this);
        }
        public void Delete()
        {
            App.UserRepository.DeleteEntity(this);
        }
        public static User GetById(int id)
        {
            return App.UserRepository.GetEntity(id);
        }
        public static List<User> GetAll()
        {
            return App.UserRepository.GetAllEntities();
        }
    }
}
