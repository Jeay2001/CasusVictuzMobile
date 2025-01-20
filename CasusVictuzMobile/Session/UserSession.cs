using CasusVictuzMobile.Database.InterFaces;
using CasusVictuzMobile.MVVM.Models;
using CasusVictuzMobile.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CasusVictuzMobile.Session
{
    [Table("UserSession")]
    public class UserSession : TableData
    {
        private static readonly Lazy<UserSession> _instance = new Lazy<UserSession>(() => new UserSession());
        public static UserSession Instance => _instance.Value;

        private static readonly BaseRepository<UserSession> _userSessionRepository = new BaseRepository<UserSession>();

        [Ignore]
        public User? LoggedInUser { get; set; }
        public int UserId { get; set; }

        public bool IsLoggedIn => LoggedInUser != null;

        public void Initialize()
        {
            try
            {
                var userSession = _userSessionRepository.GetAllEntities().FirstOrDefault();

                if (userSession == null)
                {
                    Logout();
                    return;
                }

                UserId = userSession.UserId;

                if (UserId != 0)
                {
                    UserService userService = new UserService();
                    LoggedInUser = userService.GetUserById(UserId);
                }
                else
                {
                    Logout();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR in Initialize: {ex}");
            }
        }

        public void Login(int userId)
        {
            try
            {
                UserId = userId;
                _userSessionRepository.SafeEntity(this);
                Initialize();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR in Login: {ex}");
            }
        }

        public void Logout()
        {
            try
            {
                UserId = 0;
                LoggedInUser = null;

                var session = _userSessionRepository.GetAllEntities().FirstOrDefault();
                if (session != null)
                {
                    _userSessionRepository.connection.Delete(session);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR in Logout: {ex}");
            }
        }
    }
}
