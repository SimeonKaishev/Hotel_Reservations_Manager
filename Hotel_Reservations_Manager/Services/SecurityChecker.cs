using Hotel_Reservations_Manager.Exeptions;
using HotelData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hotel_Reservations_Manager.Services
{
    public static class SecurityChecker
    {
        private static string passPattern = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$";
        private static Regex rgPass= new Regex(passPattern);
        private static bool CheckPass(string pass)
        {
            return rgPass.IsMatch(pass);
        }
        private static string phoneattern = @"^((\+359)|0)(8)([789][0-9]{3})([0-9]{4})$";
        private static Regex rgPhone = new Regex(phoneattern);
        private static bool CheckPhone(string phone)
        {
            return rgPhone.IsMatch(phone);
        }
        private static string emailPattern = @"[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+";
        private static Regex rgEmail = new Regex(emailPattern);
        private static bool CheckEmail(string email)
        {
            return rgEmail.IsMatch(email);
        }
        private static bool CheckIfNull<T>(T obj)
        {
            if (obj == null)
                return true;
            return false;
        }
        private static bool CheckEgn(string egn)
        {
            int b = (egn[0] - '0') * 2 + (egn[1] - '0') * 4 + (egn[2] - '0') * 8 + (egn[3] - '0') * 5 + (egn[4] - '0') * 10 + (egn[5] - '0') * 9 + (egn[6] - '0') * 7 + (egn[7] - '0') * 3 + (egn[8] - '0') * 6;
            int q = b / 11;
            q = b - q * 11;
            if (q == egn[9] - '0')
                return true;
            return false;
        }
        public static void CheckUser(User usr)
        {
            if (CheckIfNull(usr.Egn) || CheckIfNull(usr.Email) || CheckIfNull(usr.FirstName) ||
           CheckIfNull(usr.HireDate) || CheckIfNull(usr.LastName) || CheckIfNull(usr.Password) ||
           CheckIfNull(usr.PhoneNumber) || CheckIfNull(usr.SecondName) || CheckIfNull(usr.Username))
            {
               throw new ArgumentNullException();
            }
            if (!CheckEmail(usr.Email))
            {
                throw new InvalidEmailExeption();
            }
            if (!CheckPhone(usr.PhoneNumber))
            {
                throw new InvalidPhoneException();
            }
            if (!CheckPass(usr.Password))
            {
                throw new InvalidPassException();
            }
            if (!CheckEgn(usr.Egn))
            {
                throw new InvalidEgnException();
            }
        }
        public static void CheckClient(Client cl)
        {
            if (CheckIfNull(cl.Email) || CheckIfNull(cl.FirstName) || CheckIfNull(cl.LastName) ||
               CheckIfNull(cl.PhoneNumber))
            {
                throw new ArgumentNullException();
            }
            if (!CheckEmail(cl.Email))
            {
                throw new InvalidEmailExeption();
            }
            if (!CheckPhone(cl.PhoneNumber))
            {
                throw new InvalidPhoneException();
            }

        }
        public static void CheckRoom(Room room)
        {
            if (CheckIfNull(room.roomTypes))
            {
                throw new ArgumentNullException();
            }
            if (room.PriceAdult == 0 || room.PriceKid == 0)
            { 
                throw new InvalidPriceException();
            }
            if (room.Capacity == 0)
            {
                throw new InvalidCapacityException();
            }

        }
        public static void CheckDate(DateTime start, DateTime end)
        {
            if (start < DateTime.Now)
            {
                throw new InvalidStartDateException();
            }
            if (end < start)
            { 
                throw new InvalidEndDateException();
            }
        }
        public static void CheckReservation(Reservation res)
        {
            if (CheckIfNull(res.Clients) || CheckIfNull(res.Room))
            {
                throw new ArgumentNullException();
            }
        }
    }
}
 