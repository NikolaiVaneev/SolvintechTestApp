using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolvintechTestApp.Models;
using System.Diagnostics;

namespace SolvintechTestApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        private readonly ILogger<HomeController> _logger;


        [HttpGet]
        public IActionResult Index()
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
