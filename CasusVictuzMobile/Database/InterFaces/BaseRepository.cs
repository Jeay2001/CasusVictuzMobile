using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasusVictuzMobile.Database.InterFaces
{
  public class BaseRepository<T> : IBaseRepository<T> where T : TableData, new()
    {
        SQLiteConnection connection;
        public string? StatusMessage { get; set; }

        public BaseRepository()
        {
            connection = new SQLiteConnection(Constants.DatabasePath, Constants.flags);
            connection.CreateTable<T>();
        }

        public void DeleteEntity(T? entity)
        {
            try
            {
                connection.Delete(entity);
            }
            catch (Exception ex)
            {
                StatusMessage = $"error: {ex.Message}";
            }
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public List<T>? GetAllEntities()
        {
            try
            {
                return connection.Table<T>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"error: {ex.Message}";
                return null;
            }
        }

        public T? GetEnity(int id)
        {
            try
            {
                return connection.Table<T>().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                StatusMessage = $"error: {ex.Message}";
                return null;
            }
        }

        public void SafeEntity(T? entity)
        {
            int result = 0;
            if (entity != null)
            {
                try
                {
                    if (entity.Id == 0)
                    {
                        result = connection.Insert(entity);
                        StatusMessage = $"{result} row(s) added";
                    }
                    else
                    {
                        result = connection.Update(entity);
                        StatusMessage = $"{result} row(s) updated";
                    }
                }
                catch (Exception ex)
                {
                    StatusMessage = $"error: {ex.Message}";
                }
            }
        }
    }
}
