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
            App.UserRepository.DeleteEntity(this);
        }
        public void Delete()
        {
            App.UserRepository.DeleteEntity(this);
        }
        public User GetById(int id)
        {
            return App.UserRepository.GetEnity(id);
        }
        public List<User> GetAll()
        {
            return App.UserRepository.GetAllEntities();
        }
    }
}
