using System.ComponentModel.DataAnnotations;

namespace EcommerceWebsite.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name can't exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock Quantity cannot be negative.")]
        public int StockQuantity { get; set; }
        
        public byte[]? ImageData { get; set; }
    }
}