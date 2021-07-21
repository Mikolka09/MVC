using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Areas.Shopping_Cart.Controllers
{
    [Area("ShoppingCart")]
    public class CheckOutController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "CheckOut";
            return View();
        }
    }
}
