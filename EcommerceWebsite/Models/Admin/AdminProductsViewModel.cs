using EcommerceWebsite.Entities;

namespace EcommerceWebsite.Models
{
    public class AdminProductsViewModel
    {
        public List<Entities.Product> Products { get; set; }

        public string GetImagePath(Entities.Product product)
        {
            if (product.ImageData != null && product.ImageData.Length > 0)
            {
                var base64 = Convert.ToBase64String(product.ImageData);
                return $"data:image/png;base64,{base64}";
            }
            return string.Empty;
        }
    }
}