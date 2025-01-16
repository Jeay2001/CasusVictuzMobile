using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusVictuzMobile.Database.InterFaces
{
    public interface IBaseRepository<T> :IDisposable where T : TableData,new()
    {
        //Create/Updatw
        void SafeEntity(T? entity);
        //Read one
        T? GetEnity(int id);
        //get all
        List<T>? GetAllEntities();
        //Delete
        void DeleteEntity(T? entity);
    } 
}
