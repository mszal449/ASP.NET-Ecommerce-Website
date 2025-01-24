using Microsoft.AspNetCore.Mvc;
using EcommerceWebsite.Data;
using EcommerceWebsite.Entities;
using EcommerceWebsite.Models;
using EcommerceWebsite.Models.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebsite.Controllers
{
    [Authorize]
    public class CartController(AppDbContext context, UserManager<User> userManager) : Controller
    {
        // GET: /Cart/Index
        public IActionResult Index()
        {
            var userId = userManager.GetUserId(User);
            var openOrder = context.Orders
                                   .Include(o => o.OrderItems)
                                    .ThenInclude(oi => oi.Product)
                                   .FirstOrDefault(o => o.UserId == userId && o.State == OrderState.Open);

            var vm = new CartViewModel
            {
                CartItems = openOrder?.OrderItems.Select(oi => new CartItem
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    Product = oi.Product
                }).ToList() ?? new List<CartItem>()
            };

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

            var userId = userManager.GetUserId(User);
            var openOrder = context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(orderItem => orderItem.Product)
                .FirstOrDefault(o => o.UserId == userId && o.State == OrderState.Open);

            if (openOrder == null)
            {
                openOrder = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    State = OrderState.Open
                };
                context.Orders.Add(openOrder);
            }

            var existingItem = openOrder.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);
            if (existingItem == null)
            {
                openOrder.OrderItems.Add(new OrderItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Product = product
                });
            }
            else
            {
                existingItem.Quantity += quantity;
            }

            openOrder.TotalAmount = openOrder.OrderItems.Sum(oi => oi.Quantity * oi.Product.Price);

            context.SaveChanges();

            TempData["SuccessMessage"] = "Product successfully added to your cart.";
            return RedirectToAction("Index", "Product");
        }

        // POST: /Cart/UpdateCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCart(CartViewModel model)
        {
            var userId = userManager.GetUserId(User);
            var openOrder = context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(orderItem => orderItem.Product)
                .FirstOrDefault(o => o.UserId == userId && o.State == OrderState.Open);

            if (openOrder == null)
            {
                return RedirectToAction("Index");
            }

            foreach (var item in model.CartItems.ToList())
            {
                var orderItem = openOrder.OrderItems.FirstOrDefault(oi => oi.ProductId == item.ProductId);
                if (orderItem == null) continue;
                if (item.Quantity > 0)
                {
                    orderItem.Quantity = item.Quantity;
                }
                else
                {
                    // Remove item if quantity is 0
                    openOrder.OrderItems.Remove(orderItem);
                }
            }

            openOrder.TotalAmount = openOrder.OrderItems.Sum(oi => oi.Quantity * oi.Product.Price);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: /Cart/Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout()
        {
            var userId = userManager.GetUserId(User);
            var openOrder = context.Orders.Include(order => order.OrderItems)
                .FirstOrDefault(o => o.UserId == userId && o.State == OrderState.Open);

            if (openOrder == null || openOrder.OrderItems.Count == 0)
            {
                TempData["ErrorMessage"] = "Your cart is empty.";
                return RedirectToAction("Index");
            }

            openOrder.State = OrderState.Placed;
            openOrder.OrderDate = DateTime.Now;

            context.SaveChanges();

            TempData["SuccessMessage"] = "Your order has been placed successfully.";
            return RedirectToAction("Index", "Product");
        }
        
        [HttpPost]
        public async Task<IActionResult> ChangeOrderStatus(int orderId)
        {
            var order = await context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            order.State = OrderState.Placed;
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Product");
        }
    }
}