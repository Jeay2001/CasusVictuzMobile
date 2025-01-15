using CasusVictuzMobile.MVVM.Models;
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

        // LoadUserAsync wordt aangeroepen bij het opstarten van de app,
        // Dus in App.xaml.cs 
        public async Task LoadUserAsync()
        {
            string? loggedInUserEmail = await SecureStorage.GetAsync("loggedInUserEmail");

            if (!string.IsNullOrEmpty(loggedInUserEmail))
            {
                // Repos/Services bestaan nog niet, 
                // hier haalt hij dan het ingelogde User object op:
                //
                // UserService userService = new UserService();
                // LoggedInUser = userService.GetUserByEmail(loggedInUserEmail);            
            }
        }


        // bij het invullen van Login form, wordt deze methode aangeroepen        
        public void Login(string userEmail)
        {
            Logout();
            SecureStorage.SetAsync("loggedInUserEmail", userEmail);

        }

        public void Logout()
        {
            LoggedInUser = null;
            SecureStorage.Remove("loggedInUserEmail");
        }


    }

}
