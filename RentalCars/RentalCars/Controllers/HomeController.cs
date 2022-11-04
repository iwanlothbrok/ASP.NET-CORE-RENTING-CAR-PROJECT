namespace RentalCars.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Core.Services.Cars;

    public class HomeController : Controller
    {
        public ICarService carService;
        public HomeController(ICarService carService)
        {
            this.carService = carService;
        }

        public IActionResult Index()
        {
            var cars = carService.GetLastThreeCars();
            return View(cars);
        }

        public IActionResult Error()
       => View();
    }

}

