using Microsoft.AspNetCore.Identity;

namespace EcommerceWebsite.Entities
{
    public class User : IdentityUser
    {
        public bool IsAdmin { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}