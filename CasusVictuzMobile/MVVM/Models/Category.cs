using CasusVictuzMobile.Database.InterFaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusVictuzMobile.MVVM.Models
{
    [Table("Category")]
    public class Category : TableData
    {
        [NotNull]
        public string? Title { get; set; }
        public string? Description { get; set; }

        public static Category GetById(int id)
        {
            return App.CategoryRepository.GetEntity(id);
        }

    }
}
