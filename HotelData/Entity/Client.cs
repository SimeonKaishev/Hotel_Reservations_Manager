using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelData.Entity
{
    public class Client
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^((\+359)|0)(8)([789][0-9]{3})([0-9]{4})$",
                            ErrorMessage = "Please enter a valid phone number")]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression(@"[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+",
                            ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        [Required]
        public bool IsMinor { get; set; }

    }
}
