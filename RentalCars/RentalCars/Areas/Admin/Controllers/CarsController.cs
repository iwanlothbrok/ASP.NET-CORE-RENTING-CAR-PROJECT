namespace RentalCars.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Services.Cars.Models;

    [Area(Constants.AreaName)]
    [Authorize(Roles = Constants.AreaName)]
    public class CarsController : Controller
    {
        private readonly ICarService cars;

        public CarsController(ICarService cars)
        {
            this.cars = cars;
        }

        public IActionResult All()
        {
            IEnumerable<CarServiceModel> cars = this.cars
                .All(publicOnly: false)
                .Cars
                .ToList();

            return View(cars);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.cars.ChangeVisility(id);

            return Redirect("https://localhost:7163/");
        }
    }
}
