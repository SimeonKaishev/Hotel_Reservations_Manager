using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelData.Entity
{
    public class Reservation
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Room Room { get; set; }
        [Required]
        public User Reserver { get; set; }
        [Required]
        public List<Client> Clients { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public bool IsBreakfastIncluded { get; set; }
        [Required]
        public bool IsAllInclusive { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
