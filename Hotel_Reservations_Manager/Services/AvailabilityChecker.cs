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
        //private static readonly HotelContext _context = new HotelContext();
        private static bool CheckIfUsernameAvailable(string username, HotelContext _context)
        {
            var usrname = (from u in _context.Users where u.Username == username select u.Username).ToList();
            if (usrname.Count != 0)
            {
                return false;
            }
            return true;
        }
        private static bool CheckIfPhoneNumberAvailable(string phoneNumber, HotelContext _context)
        {
            var pnNumbers = (from u in _context.Users where u.PhoneNumber == phoneNumber select u.PhoneNumber).ToList();
            if (pnNumbers.Count != 0)
            {
                return false;
            }
            return true;
        }
        private static bool CheckIfEgnAvailable(string egn, HotelContext _context)
        {
            var egns = (from u in _context.Users where u.Egn == egn select u.Egn).ToList();
            if (egns.Count != 0)
            {
                return false;
            }
            return true;
        }
        private static bool CheckIfEmailAvailable(string email, HotelContext _context)
        {
            var emails = (from u in _context.Users where u.Email == email select u.Email).ToList();
            if (emails.Count != 0)
            {
                return false;
            }
            return true;
        }
        public static void CheckUserAvailabikity(User user, HotelContext context)
        {
            if (!CheckIfUsernameAvailable(user.Username,context))
            {
                throw new UsernameAlreadyExistsException();
            }
            if (!CheckIfPhoneNumberAvailable(user.PhoneNumber, context))
            {
                throw new PhoneAlreadyExistsException();
            }
            if (!CheckIfEmailAvailable(user.Email, context))
            {
                throw new EmailAlreadyExistsException();
            }
            if (!CheckIfEgnAvailable(user.Egn, context))
            {
                throw new EgnAlreadyExistsException();
            }
        }
    }
}
