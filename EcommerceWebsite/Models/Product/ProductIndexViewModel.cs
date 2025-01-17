using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceWebsite.Models.Product;

public class ProductIndexViewModel
{
    public List<Entities.Product> Products { get; set; }
    public ProductFilterModel ProductFilters { get; set; }
}