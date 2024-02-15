using Microsoft.AspNetCore.Mvc;
using Mirea_Avia.Database;
using Mirea_Avia.Models;
using Mirea_Avia.Models.Users;
using System.Diagnostics;

namespace Mirea_Avia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                User u1 = new User { Email = "Test@mail.ru", Id = 1, Login = "TestLogin", Username = "Test username", Password="TestPassword" };

                db.Users.Add(u1);
                db.SaveChanges();
            }

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