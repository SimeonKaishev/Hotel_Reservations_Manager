using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelData.Entity
{
    public class Room
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Capacity { get; set; }
        public enum roomType { dualBeds, doubleBed, appartament, pentHouse, mezonet }
        [Required]
        public string roomTypes { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        [Required]
        public decimal PriceAdult { get; set; }
        [Required]
        public decimal PriceKid { get; set; }
        [Required]
        public int roomNumber { get; set; }

    }
}
