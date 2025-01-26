using System;
using SQLite;
using CasusVictuzMobile.Database.InterFaces;

namespace CasusVictuzMobile.MVVM.Models
{
    [Table("Comment")]
    public class Comment : TableData
    {
        [Ignore]
        public virtual EventRecap? EventRecap { get; set; }

        [NotNull]
        public int UserId { get; set; }

        [Ignore]
        public virtual User? User { get; set; }

        [NotNull]
        public string Content { get; set; } = string.Empty;

        // Nullable PhotoPath
        public string? PhotoPath { get; set; }

        [NotNull]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // CRUD Operations
        public void SaveOrUpdate()
        {
            App.CommentRepository.SafeEntity(this);
        }

        public void Delete()
        {
            App.CommentRepository.DeleteEntity(this);
        }

        public static Comment GetById(int id)
        {
            return App.CommentRepository.GetEntity(id);
        }

        public static List<Comment> GetAll()
        {
            return App.CommentRepository.GetAllEntities();
        }
    }
}
