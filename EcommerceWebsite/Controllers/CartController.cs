using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EcommerceWebsite.Data;
using EcommerceWebsite.Entities;
using EcommerceWebsite.Helpers;
using EcommerceWebsite.Models;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceWebsite.Controllers
{
    [Authorize]
    public class CartController(AppDbContext context) : Controller
    {
        // GET: /Cart/Index
        public IActionResult Index()
        {
            var sessionCart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") 
                             ?? new List<CartItem>();

            // Enrich each CartItem with Product details
            var enrichedCart = sessionCart.Select(ci =>
            {
                var product = context.Products.FirstOrDefault(p => p.ProductId == ci.ProductId);
                return new CartItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    Product = product
                };
            }).ToList();

            var vm = new CartViewModel { CartItems = enrichedCart };
            return View(vm);
        }

        // POST: /Cart/AddToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            var product = context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
            {
                return RedirectToAction("Index");
            }

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") 
                       ?? new List<CartItem>();

            var existingItem = cart.FirstOrDefault(ci => ci.ProductId == productId);
            if (existingItem == null)
            {
                cart.Add(new CartItem { ProductId = productId, Quantity = quantity });
            }
            else
            {
                existingItem.Quantity += quantity;
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart);
            
            TempData["SuccessMessage"] = "Product successfully added to your cart.";
            return RedirectToAction("Index", "Product");
        }

        // POST: /Cart/UpdateCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCart(CartViewModel model)
        {
            var sessionCart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") 
                             ?? new List<CartItem>();

            foreach (var item in model.CartItems.ToList())
            {
                var cartItem = sessionCart.FirstOrDefault(ci => ci.ProductId == item.ProductId);
                if (cartItem != null)
                {
                    if (item.Quantity > 0)
                    {
                        cartItem.Quantity = item.Quantity;
                    }
                    else
                    {
                        // Remove item if quantity is 0
                        sessionCart.Remove(cartItem);
                    }
                }
            }

            HttpContext.Session.SetObjectAsJson("Cart", sessionCart);
            return RedirectToAction("Index");
        }
    }
}