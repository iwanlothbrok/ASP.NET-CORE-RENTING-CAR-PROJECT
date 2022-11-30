namespace RentalCars.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Services.Cars.Models;

    public class HomeController : Controller
    {
        public ICarService carService;
        public HomeController(ICarService carService)
        {
            this.carService = carService;
        }

        public IActionResult Index()
        {
            IEnumerable<CarServiceModel> cars = this.carService
                                    .GetLastThreeCars()
                                    .ToList();
            return View(cars);
        }

        public IActionResult Error()
       => View();
    }

}

