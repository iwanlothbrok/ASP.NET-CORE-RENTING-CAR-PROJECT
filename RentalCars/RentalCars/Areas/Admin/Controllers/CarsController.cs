namespace RentalCars.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Core.Services.Cars;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.Web;


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
            var cars = this.cars
                .All(publicOnly: false)
                .Cars;

            return View(cars);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.cars.ChangeVisility(id);


            TempData[GlobalMessageKey] = "Thank you for adding your car!";


            return RedirectToAction(nameof(All));
        }
    }
}
