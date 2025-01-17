using System.Collections.Generic;

namespace EcommerceWebsite.Models
{
    public class CartViewModel
    {
        public List<Entities.CartItem> CartItems { get; set; } = new ();
    }
}