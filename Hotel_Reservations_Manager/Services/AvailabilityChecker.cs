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
        private static bool CheckIfClientEmailAvailable(string email, HotelContext _context)
        {
            var emails = (from c in _context.Clients where c.Email == email select c.Email).ToList();
            if (emails.Count != 0)
            {
                return false;
            }
            return true;
        }
        private static bool CheckIfClientPhoneAvailable(string phonenum, HotelContext _context)
        {
            var phones = (from c in _context.Clients where c.PhoneNumber == phonenum select c.PhoneNumber).ToList();
            if (phones.Count != 0)
            {
                return false;
            }
            return true;
        }
        public static void CheckClientAvailabikity(Client client, HotelContext context)
        {
            if (!CheckIfClientPhoneAvailable(client.PhoneNumber, context))
            {
                throw new PhoneAlreadyExistsException();
            }
            if (!CheckIfClientEmailAvailable(client.Email, context))
            {
                throw new EmailAlreadyExistsException();
            }
        }
        private static bool CheckIfRoomNmAvailable(int roomNum, HotelContext _context)
        {
            var rooms = (from r in _context.Rooms where r.roomNumber == roomNum select r.roomNumber).ToList();
            if (rooms.Count != 0)
            {
                return false;
            }
            return true;
        }
        public static void CheckRoomAvailabikity(Room room, HotelContext context)
        {
            if (!CheckIfRoomNmAvailable(room.roomNumber, context))
            {
                throw new RoomNumberExistsException();
            }
        }
    }
}
