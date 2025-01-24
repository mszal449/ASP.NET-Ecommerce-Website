
namespace EcommerceWebsite.Models.Cart
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }

    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Entities.Product? Product { get; set; }
    }
}