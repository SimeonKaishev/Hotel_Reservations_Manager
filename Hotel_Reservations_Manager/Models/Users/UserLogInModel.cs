using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Reservations_Manager.Models.Users
{
    public class UserLogInModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$",
                            ErrorMessage = "Password must contain at least one digit, one upper and lower case letter and one special character")]
        public string Password { get; set; }
    }
}
