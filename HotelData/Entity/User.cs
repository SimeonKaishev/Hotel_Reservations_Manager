using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelData.Entity
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$",
                            ErrorMessage = "Password must contain at least one digit, one upper and lower case letter and one special character")]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Please enter a valid egn")]
        [MaxLength(10)]
        public string Egn { get; set; }
        [Required(ErrorMessage = "Phone number id is required")]
        [RegularExpression(@"^((\+359)|0)(8)([789][0-9]{3})([0-9]{4})$",
                            ErrorMessage = "Please enter a valid phone number")]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression(@"[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+",
                            ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime HireDate { get; set; }
        public DateTime LeaveDate { get; set; }

    }
}
