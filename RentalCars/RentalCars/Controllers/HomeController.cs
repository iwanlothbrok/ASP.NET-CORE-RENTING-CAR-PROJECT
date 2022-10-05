using Microsoft.AspNetCore.Mvc;

namespace RentalCars.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
