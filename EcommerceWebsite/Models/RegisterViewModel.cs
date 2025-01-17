using System.ComponentModel.DataAnnotations;

namespace EcommerceWebsite.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]        
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}