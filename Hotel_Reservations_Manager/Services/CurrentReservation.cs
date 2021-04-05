using HotelData.Entity;
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
    }
}
