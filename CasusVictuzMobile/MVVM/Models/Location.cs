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
        public double Latitude { get; set; } = 50.88150; // hardcoded coordinates van Zuyd Hogeschool.        
        public double Longitude { get; set; } = 5.95885; // zou dan bij object aanmaken andere waarde krijgen.

        public static Location GetById(int id)
        {
            return App.LocationRepository.GetEntity(id);
        }
    }
}
