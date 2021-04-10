using HotelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Reservations_Manager.Services
{
    public static class AvailabilityHandler
    {
        private static void Fillrooms( HotelContext _context)
        {
            var reservations = (from r in _context.Reservations  select r).ToList();
            if (reservations.Count != 0)
            {
                foreach (var res in reservations)
                {
                    if (res.StartDate.Date <= DateTime.Today && res.Room.IsAvailable==true )
                    {
                        res.Room.IsAvailable = false;
                        _context.Update(res.Room);
                        _context.SaveChangesAsync();
                    }
                }
            }
           // return true;
        }
        private static void Emptyrooms( HotelContext _context)
        {
            var reservations = (from r in _context.Reservations select r).ToList();
            if (reservations.Count != 0)
            {
                foreach (var res in reservations)
                {
                    if (res.EndDate.Date <= DateTime.Today && res.Room.IsAvailable == false)
                    {
                        res.Room.IsAvailable = true;
                        _context.Update(res.Room);
                        _context.SaveChangesAsync();
                    }
                }
            }
            //return true;
        }
        public static void GetHandled(HotelContext context)
        {
            Fillrooms(context);
            Emptyrooms(context);
        }
    }
}
