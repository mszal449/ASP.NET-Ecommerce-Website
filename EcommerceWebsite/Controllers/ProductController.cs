using EcommerceWebsite.Data;
using EcommerceWebsite.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using EcommerceWebsite.Models;
using EcommerceWebsite.Models.Product;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceWebsite.Controllers
{
    public class ProductController(AppDbContext context) : Controller
    {
        
        public IActionResult Index(ProductFilterModel filters)
        {
            // Retrieve all products from the database
            var products = context.Products.AsQueryable();

            // Apply name filter
            if (!string.IsNullOrEmpty(filters.Name))
            {
                products = products.Where(p => p.Name.Contains(filters.Name));
            }

            if (!string.IsNullOrEmpty(filters.Description))
            {
                products = products.Where(p => p.Description.Contains(filters.Description));
            }
            
            if (filters.MinPrice != null)
            {
                products = products.Where(p => p.Price >= filters.MinPrice);
            }
            
            if (filters.MaxPrice != null)
            {
                products = products.Where(p => p.Price <= filters.MaxPrice);
            }

            switch (filters.Stock)
            {
                case StockOption.InStock:
                    products = products.Where(p => p.StockQuantity > 0);
                    break;
                case StockOption.OutOfStock:
                    products = products.Where(p => p.StockQuantity == 0);
                    break;
            }
            
            // Convert to list before applying ordering
            var productList = products.ToList();

            // Apply sorting
            switch (filters.SortBy)
            {
                case SortByOption.NameAsc:
                    productList = productList.OrderBy(p => p.Name).ToList();
                    break;
                case SortByOption.NameDesc:
                    productList = productList.OrderByDescending(p => p.Name).ToList();
                    break;
                case SortByOption.PriceAsc:
                    productList = productList.OrderBy(p => p.Price).ToList();
                    break;
                case SortByOption.PriceDesc:
                    productList = productList.OrderByDescending(p => p.Price).ToList();
                    break;
            }
            
            var viewModel = new ProductIndexViewModel()
            {
                Products = productList,
                ProductFilters = filters
            };
            return View(viewModel);
        }
    }
}