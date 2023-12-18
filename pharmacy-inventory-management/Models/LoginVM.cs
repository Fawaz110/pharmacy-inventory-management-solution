using System.ComponentModel.DataAnnotations;

namespace pharmacy_inventory_management.Models
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\\W).{8,}$",
            ErrorMessage = "Incorrect email or password")]
        public string Password { get; set; }
    }
}
