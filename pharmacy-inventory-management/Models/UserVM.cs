using System.ComponentModel.DataAnnotations;

namespace pharmacy_inventory_management.Models
{
    public class UserVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\\W).{8,}$", 
            ErrorMessage = "Password require at least: (1 lowercase letter, 1 upper case letter, 1 digit, 1 symbol)")]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string ReasonToRemove { get; set; }
    }
}
