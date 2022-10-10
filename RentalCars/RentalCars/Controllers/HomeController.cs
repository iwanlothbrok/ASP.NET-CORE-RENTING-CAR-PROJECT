namespace RentalCars.Controllers
{
using Microsoft.AspNetCore.Mvc;
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
