using System.ComponentModel.DataAnnotations.Schema;
using EcommerceWebsite.Entities;

namespace EcommerceWebsite.Entities
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [NotMapped]
        public Entities.Product? Product { get; set; }
    }
}