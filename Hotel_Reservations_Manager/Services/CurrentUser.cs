using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Reservations_Manager.Services
{
    public static class CurrentUser
    {
        public static int Id { get; set; }
        public static string UserName { get; set; }
        public static bool IsAdmin { get; set; }
        public static bool IsLogged { get; set; }
        public static void SetCurrentUser(int _id, string _username)
        {
            Id = _id;
            UserName = _username;
            if (Id == 1)
            {
                IsAdmin = true;
            }
            IsLogged = true;
        }
        public static void LogOut()
        {
            Id = 0;
            UserName = null;
            IsAdmin = false;
            IsLogged = false;
        }
    }
}
