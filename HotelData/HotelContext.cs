using HotelData.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace HotelData
{
    public class HotelContext:DbContext
    {
        public HotelContext(DbContextOptions options) : base(options) 
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
