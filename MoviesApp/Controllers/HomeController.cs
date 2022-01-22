using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MoviesApp.Controllers
{
    public class HomeController: Controller
    {
        //dependencies - зависимости
        private readonly ILogger<HomeController> _logger;
        //dependency injection
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        // GET: /
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        // GET: Home/Privacy
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}