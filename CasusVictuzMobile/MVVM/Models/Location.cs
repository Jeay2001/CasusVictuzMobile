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

        public static Location GetById(int id)
        {
            return App.LocationRepository.GetEntity(id);
        }
    }
}
