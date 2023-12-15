using LibraryApp.Models;
using LibraryApp.Models.Abstract;
using LibraryApp.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibraryApp.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly IRentOrderRepository _rentorderepo;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,IRentOrderRepository rentorderrepository)
        {
            _logger = logger;
            _rentorderepo = rentorderrepository;
        }

        public IActionResult Index()
        {

            int count = 0;
            List<RentOrder> orders = _rentorderepo.GetAll().Where(r => r.OrderState == false).ToList();
            foreach (RentOrder order in orders)
            {
                count++;
            }
            ViewBag.count = count;
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