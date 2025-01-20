using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CasusVictuzMobile.Database.InterFaces;

namespace CasusVictuzMobile.MVVM.Models
{
    [Table("Location")]
    public class Location: TableData
    {
        public string? Name { get; set; }
        //mogelijk iets met een aps api

        public void SaveOrUpdate()
        {
            App.LocationRepository.SafeEntity(this);
        }
        public void Delete()
        {
            App.LocationRepository.DeleteEntity(this);
        }
        public static Location GetById(int id)
        {
            return App.LocationRepository.GetEntity(id);
        }
        public static List<Location> GetAll()
        {
            return App.LocationRepository.GetAllEntities();
        }
    }
}
