using System.ComponentModel.DataAnnotations;

namespace EcommerceWebsite.Models
{
    public class CreateProductViewModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public decimal Price { get; set; }
        
        public string Description { get; set; }
        
        [Required]
        public int StockQuantity { get; set; }
        
        [Required(ErrorMessage = "Please upload an image.")]
        public IFormFile? ImageFile { get; set; }
    }
}