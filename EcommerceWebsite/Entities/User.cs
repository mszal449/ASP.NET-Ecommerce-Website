using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EcommerceWebsite.Entities
{
    public class User : IdentityUser
    {
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    
        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}