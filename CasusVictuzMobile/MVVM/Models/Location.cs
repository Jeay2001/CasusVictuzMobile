using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CasusVictuzMobile.Database.InterFaces;

namespace CasusVictuzMobile.MVVM.Models
{
    public class Location: TableData
    {
        public string? Name { get; set; }
        //mogelijk iets met een aps api

        public void SaveOrUpdate()
        {
            App.LocationRepository.DeleteEntity(this);
        }
        public void Delete()
        {
            App.LocationRepository.DeleteEntity(this);
        }
        public Location GetById(int id)
        {
            return App.LocationRepository.GetEnity(id);
        }
        public List<Location> GetAll()
        {
            return App.LocationRepository.GetAllEntities();
        }
    }
}
