using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EcommerceWebsite.Models;

namespace EcommerceWebsite.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("Index", "Product");
    }
}