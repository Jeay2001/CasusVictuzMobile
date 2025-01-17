﻿using CasusVictuzMobile.MVVM.Models;
using CasusVictuzMobile.Services;
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
    public class UserSession
    {        
        private static UserSession _instance;
        public static UserSession Instance => _instance ??= new UserSession();

        public User? LoggedInUser { get; set; }

        public bool IsLoggedIn => LoggedInUser != null;

        // LoadUserAsync wordt aangeroepen bij het opstarten van de app om te controleren of er een ingelogde gebruiker is
        // Dus in LoginPageViewModel 
        public async Task LoadUserAsync()
        {
            int? loggedInUserId = Convert.ToInt32(await SecureStorage.GetAsync("loggedInUserId"));           

            if (loggedInUserId != null)
            {
                UserService userService = new UserService();
                LoggedInUser = userService.GetUserById(loggedInUserId.Value);
                
            }
        }


        // bij het invullen van Login form, wordt deze methode aangeroepen        
        public void Login(int userId)
        {
            Logout();
            SecureStorage.SetAsync("loggedInUserId", userId.ToString());            
        }

        public void Logout()
        {
            LoggedInUser = null;
            SecureStorage.Remove("loggedInUserId");
        }


    }

}
