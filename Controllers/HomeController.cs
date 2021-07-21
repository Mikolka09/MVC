using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MVC.Models;
using MVC.Options;
using System;
using System.Diagnostics;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmailOptions emailOptions;

        public HomeController(ILogger<HomeController> logger, IOptionsSnapshot<EmailOptions> emailOptions)
        {
            _logger = logger;
            this.emailOptions = emailOptions.Value;
        }

        public IActionResult Index(Guid? id)
        {
            ViewBag.EmailOptions = emailOptions;
            return View(id);
        }

        [ActionName("Index With Parametr")]
        public IActionResult Index(int? id)
        {
            ViewBag.EmailOptions = emailOptions;
            return View("Index", id);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[Route("Error", Name = "Error"), HttpGet, HttpPost]
        //[Route("[area]/[controller]/[action]", Name = "Error"), HttpGet, HttpPost]
        [Route("[action]", Name = "Error"), HttpGet, HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("~/[action]")]
        [ActionName("NotFound")]
        public IActionResult NotFoundView()
        {
            ViewData["Title"] = "Page not Found";
            return View();
        }
    }
}
