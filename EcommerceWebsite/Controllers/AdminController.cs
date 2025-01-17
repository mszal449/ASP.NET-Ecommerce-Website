using EcommerceWebsite.Data;
using EcommerceWebsite.Entities;
using EcommerceWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace EcommerceWebsite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Index
        [HttpGet]
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            var viewModel = new AdminViewModel
            {
                Products = products
            };
            return View(viewModel);    
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        // GET: Admin/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
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
                var existingProduct = _context.Products.Find(product.ProductId);
                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.StockQuantity = product.StockQuantity;

                    if (imageFile != null && imageFile.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        imageFile.CopyTo(ms);
                        existingProduct.ImageData = ms.ToArray();
                    }

                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Product not found.");
            }
            return View(product);
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

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}