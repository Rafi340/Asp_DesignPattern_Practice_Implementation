using System.Diagnostics;
using Demo.Models;
using Demo.Models.Demo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IItem _item;
        private readonly IProduct _product;

        public HomeController(ILogger<HomeController> logger, IItem item, [FromKeyedServices("Config1")] IProduct product)
        {
            _logger = logger;
            _item = item;
            _product = product;
        }

        public IActionResult Index()
        {
            var item = _item.Price();
            var product = _product.GetPrice();
            return View();
        }
        [Authorize(Policy = "AgeRestriction")]
        public IActionResult AgeTest()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
