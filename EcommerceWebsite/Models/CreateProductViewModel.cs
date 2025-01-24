using System.ComponentModel.DataAnnotations;

namespace EcommerceWebsite.Models
{
    public class CreateProductViewModel
    {
        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        
        
        [Display(Name = "Description")]
        public string Description { get; set; }
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock Quantity must be non-negative.")]
        [Display(Name = "Stock Quantity")]
        public int StockQuantity { get; set; }
        
        
        [Display(Name = "Image")]
        [Required(ErrorMessage = "Please upload an image.")]
        public IFormFile? ImageFile { get; set; }
    }
}