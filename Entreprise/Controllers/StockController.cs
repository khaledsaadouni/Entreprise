using Microsoft.AspNetCore.Mvc;

namespace Entreprise.Controllers
{
    public class StockController : Controller
    {
        public IActionResult Index()
        {
            return View();
        } 
        public IActionResult Product()
        {
            return View();
        }
        public IActionResult Vendor()
        {
            return View();
        }
    }
}
