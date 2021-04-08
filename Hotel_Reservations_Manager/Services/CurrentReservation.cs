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
        public static void SetResFirst(Reservation reservation)
        {
            Reserver = reservation.Reserver;
            
            StartDate = reservation.StartDate;
            EndDate = reservation.EndDate;
            IsAllInclusive = reservation.IsAllInclusive;
            IsBreakfastIncluded = reservation.IsBreakfastIncluded;
        }
        public static Reservation GetReservation()
        {
            Reservation res = new Reservation();
            res.Clients = Clients;
            res.EndDate = EndDate;
            res.IsAllInclusive = IsAllInclusive;
            res.IsBreakfastIncluded = IsBreakfastIncluded;
            CalkPrice();
            res.Price = Price;
            res.Reserver = Reserver;
            res.Room = Room;
            res.StartDate = StartDate;
            return res;
        }
        private static void CalkPrice()
        {
            adults = 0;
            Price = 0;
            double days = (EndDate - StartDate).TotalDays;
            foreach (var item in Clients)
            {
                if (item.IsMinor == false)
                    adults++;
            }
            Price += adults * Room.PriceAdult;
            Price += (Clients.Count - adults) * Room.PriceKid;
        }
    }
}
