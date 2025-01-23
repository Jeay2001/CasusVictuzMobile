using CasusVictuzMobile.Database.InterFaces;
using CasusVictuzMobile.Enums;
using CasusVictuzMobile.MVVM.Models;
using CasusVictuzMobile.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CasusVictuzMobile.Services
{
    public class UserService
    {
        private readonly BaseRepository<User> _userRepository;
        public UserService()
        {
            _userRepository = new BaseRepository<User>();
        }

        public LoginResult Login(string email, string password)
        {                        
            User user = _userRepository.GetAllEntities().FirstOrDefault(x => x.Email == email);

            if (user != null)
            {
                if (user.Password == password)
                {
                    UserSession.Instance.Login(user.Id);                    
                    return LoginResult.Success;
                }
                else
                {
                    return LoginResult.WrongPassword;
                }
            }
            else
            {
                return LoginResult.UserNotFound;
            }
        }

        public void Register(string userName, string email, string password)
        {
            User user = new User
            {
                Username = userName,
                Email = email,
                Password = password,
                IsMember = true,
            };
            _userRepository.SafeEntity(user);           
        }

        public void RegisterGuestAccount()
        {
            User user = new User
            {
                Username = "Guest",
                IsGuest = true,
            };
            _userRepository.SafeEntity(user);
            UserSession.Instance.Login(user.Id);
            UserSession.Instance.Initialize();
        }

        public void UpdateUser(User user)
        {
            _userRepository.SafeEntity(user);
        }   

        public User GetUserById(int userId)
        {
            return _userRepository.GetAllEntities().FirstOrDefault(x => x.Id == userId);
        }
    }
}
