using HotelData;
using HotelData.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Reservations_Manager.Services
{
    public static class CurrentReservation
    {

        public static int Id { get; set; }
        public static Room Room { get; set; }
        public static User Reserver { get; set; }
        public static List<Client> Clients { get; set; }
        public static DateTime StartDate { get; set; }
        public static DateTime EndDate { get; set; }
        public static bool IsBreakfastIncluded { get; set; }
        public static  bool IsAllInclusive { get; set; }
        public static decimal Price { get; set; }
        private static int adults;


        public static decimal CalkPrice(Reservation res)
        {
            int adults = 0;
            decimal price = 0;
            double days = (res.EndDate - res.StartDate).TotalDays;
            foreach (var item in res.Clients)
            {
                if (item.IsMinor == false)
                    adults++;
            }
            price += adults * res.Room.PriceAdult;
            price += (res.Clients.Count - adults) * res.Room.PriceKid;
            return price;
        }
        public static Room GetRoom(int rId, HotelContext _context)
        {
            var rooms = (from r in _context.Rooms where r.Id == rId select r).ToList();
            return rooms[0];
        }
        public static List<Client> GetClients(string cIds, HotelContext _context)
        {
            var Ids = cIds.Split(' ').ToList();
            List<Client> ListOfClients=new List<Client>();
            foreach (var id in Ids)
            {
                if (id != "" && id != " ")
                {
                    var clients = (from c in _context.Clients where c.Id == int.Parse(id) select c).ToList();
                    ListOfClients.Add(clients[0]);
                }
            }
            return ListOfClients;
        }
        public static User GetReserver(int rId, HotelContext _context)
        {
            var users = (from r in _context.Users where r.Id == rId select r).ToList();
            return users[0];
        }
    }
}
