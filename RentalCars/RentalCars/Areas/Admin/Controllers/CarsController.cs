namespace RentalCars.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Core.Services.Cars;

    [Area(Constants.AreaName)]
    [Authorize]
    public class CarsController : Controller
    {
        private readonly ICarService cars;

        public CarsController(ICarService cars)
        {
            this.cars = cars;
        }

        public IActionResult All()
        {
            var cars = this.cars
                .All(publicOnly: false)
                .Cars;

            return View(cars);
        }

        //public IActionResult ChangeVisibility(int id)
        //{
        //    this.cars.ChangeVisility(id);

        //    return RedirectToAction(nameof(All));
        //}
    }
}
