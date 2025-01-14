using CasusVictuzMobile.MVVM.Models.InterFaces;
using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusVictuzMobile.MVVM.Models
{
    public class Location: TableData
    {
        public string? Name { get; set; }
        //mogelijk iets met een aps api
    }
}
