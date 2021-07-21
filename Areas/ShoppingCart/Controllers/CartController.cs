using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Areas.Shopping_Cart.Controllers
{
    [Area("ShoppingCart")]
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Cart";
            return View();
        }
    }
}
