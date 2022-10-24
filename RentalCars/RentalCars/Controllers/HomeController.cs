namespace RentalCars.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("All", "Cars");
            }
            return View();
        }

        public IActionResult Error() => View();
    }

}

