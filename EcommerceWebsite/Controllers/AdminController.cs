using EcommerceWebsite.Data;
using EcommerceWebsite.Entities;
using EcommerceWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using EcommerceWebsite.Models.Admin;

namespace EcommerceWebsite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController(AppDbContext context) : Controller
    {
        // GET: Admin/Index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        // GET: Admin/Products
        [HttpGet]
        public IActionResult Products()
        {
            var products = context.Products.ToList();
            var viewModel = new AdminProductsViewModel
            {
                Products = products
            };
            return View(viewModel);    
        }
        
        // GET: Admin/Users
        [HttpGet]
        public IActionResult Users()
        {
            var users = context.Users.ToList();
            return View(users);
        }
        

        // GET: Admin/DeleteProduct/5
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("Products");
        }
        
        // GET: Admin/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    
        // POST: Admin/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = context.Products.Find(product.ProductId);
                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.StockQuantity = product.StockQuantity;

                    if (imageFile is { Length: > 0 })
                    {
                        using var ms = new MemoryStream();
                        imageFile.CopyTo(ms);
                        existingProduct.ImageData = ms.ToArray();
                    }

                    context.SaveChanges();
                    return RedirectToAction("Products");
                }
                ModelState.AddModelError("", "Product not found.");
            }
            return RedirectToAction("Products");
        }

        // GET: Admin/AddProduct
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        // POST: Admin/AddProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                StockQuantity = model.StockQuantity,
            };

            if (model.ImageFile is { Length: > 0 })
            {
                using var ms = new MemoryStream();
                model.ImageFile.CopyTo(ms);
                product.ImageData = ms.ToArray();
            }

            context.Products.Add(product);
            context.SaveChanges();

            return RedirectToAction("Products");
        }
        
        // GET: Admin/Browse
        [HttpGet]
        public async Task<IActionResult> Orders(OrderState? state)
        {
            var ordersQuery = context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .AsQueryable();

            if (state.HasValue)
            {
                ordersQuery = ordersQuery.Where(o => o.State == state.Value);
            }

            var orders = await ordersQuery.ToListAsync();

            var viewModel = new AdminOrdersViewModel
            {
                Orders = orders,
                State = state,
                OrderStates = AdminOrdersViewModel.GetOrderStates(state)
            };

            return View(viewModel);
        }
    }
}