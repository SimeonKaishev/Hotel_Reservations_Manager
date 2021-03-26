using Hotel_Reservations_Manager.Exeptions;
using HotelData;
using HotelData.Entity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Reservations_Manager.Services
{
    public static class AvailabilityChecker
    {
        private static readonly HotelContext _context = new HotelContext();
        private static bool CheckIfUsernameAvailable(string username)
        {
            var usrname = (from u in _context.Users where u.Username == username select u.Username).ToList();
            if (usrname.Count != 0)
            {
                return false;
            }
            return true;
        }
        private static bool CheckIfPhoneNumberAvailable(string phoneNumber)
        {
            var pnNumbers = (from u in _context.Users where u.PhoneNumber == phoneNumber select u.PhoneNumber).ToList();
            if (pnNumbers.Count != 0)
            {
                return false;
            }
            return true;
        }
        private static bool CheckIfEgnAvailable(string egn)
        {
            var egns = (from u in _context.Users where u.Egn == egn select u.Egn).ToList();
            if (egns.Count != 0)
            {
                return false;
            }
            return true;
        }
        private static bool CheckIfEmailAvailable(string email)
        {
            var emails = (from u in _context.Users where u.Email == email select u.Email).ToList();
            if (emails.Count != 0)
            {
                return false;
            }
            return true;
        }
        public static void CheckUserAvailabikity(User user)
        {
            if (!CheckIfUsernameAvailable(user.Username))
            {
                throw new UsernameAlreadyExistsException();
            }
            if (!CheckIfPhoneNumberAvailable(user.PhoneNumber))
            {
                throw new PhoneAlreadyExistsException();
            }
            if (!CheckIfEmailAvailable(user.Email))
            {
                throw new EmailAlreadyExistsException();
            }
            if (!CheckIfEgnAvailable(user.Egn))
            {
                throw new EgnAlreadyExistsException();
            }
        }
    }
}
