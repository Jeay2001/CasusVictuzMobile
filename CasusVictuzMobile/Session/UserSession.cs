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
    // in een ViewModel kun je dan steeds:
    // // LoginPageViewModel:
    // UserSession.Instance.Login("email");    
    // // ProfileViewModel:
    // UserSession.Instance.LoggedInUser.Name;
    // Zonder dat je steeds 'UserSession userSession = new UserSession();' of nieuwe Service/Repository object hoeft te maken,
    // omdat UserSession een Singleton is en UserSession.Instance een static property.
    [Table("UserSession")]
    public class UserSession : TableData
    {
        private static UserSession _instance;
        [Ignore]
        public static UserSession Instance => _instance ??= new UserSession();
        [Ignore]
        public User? LoggedInUser { get; set; }
        public int UserId { get; set; }

        public bool IsLoggedIn => LoggedInUser != null;

        // LoadUserAsync wordt aangeroepen bij het opstarten van de app om te controleren of er een ingelogde gebruiker is
        // Dus in LoginPageViewModel 
        public void Initialize()
        {
            try
            {
                BaseRepository<UserSession> _userSessionRepository = new BaseRepository<UserSession>();
                var userSession = _userSessionRepository.GetAllEntities().FirstOrDefault();

                if (userSession == null)
                {
                    Logout(); // Make sure the session is correctly reset
                    return;
                }

                int loggedInUserId = userSession.UserId;

                if (loggedInUserId != 0)
                {
                    UserService userService = new UserService();
                    LoggedInUser = userService.GetUserById(loggedInUserId);
                }
                else
                {
                    Logout();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("INITILIAZE:");
                System.Diagnostics.Debug.WriteLine("INITILIAZE:");
                System.Diagnostics.Debug.WriteLine("INITILIAZE:");
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }




        // bij het invullen van Login form, wordt deze methode aangeroepen        
        public void Login(int userId)
        {

            try
            {
                UserId = userId;
                BaseRepository<UserSession> _userSessionRepository = new BaseRepository<UserSession>();
                _userSessionRepository.SafeEntity(this);
                Initialize();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LOGIN:");
                System.Diagnostics.Debug.WriteLine("LOGIN:");
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public void Logout()
        {
            try
            {
                UserId = 0;
                LoggedInUser = null;

                BaseRepository<UserSession> _userSessionRepository = new BaseRepository<UserSession>();
                var session = _userSessionRepository.GetAllEntities().FirstOrDefault();
                if (session != null)
                {
                    _userSessionRepository.connection.Delete(session);
                }
            }catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LOGOUT:");
                System.Diagnostics.Debug.WriteLine("LOGOUT:");
                System.Diagnostics.Debug.WriteLine("LOGOUT:");
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }



    }

}
