namespace RentalCars.Controllers
{
    using AutoMapper;
    using CarRentingSystem.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Models.Renting;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Services.Dealers;

    public class RentingController : Controller
    {

        private readonly ICarService carService;
        private readonly IDealerService dealerService;
        private readonly IMapper mapper;
        public RentingController(IDealerService dealer, ICarService car, IMapper mapper)
        {
            this.carService = car;
            this.dealerService = dealer;
            this.mapper = mapper;
        }

        public IActionResult Rent()
        {


            return View(new RentFormModel
            {
                Cars = this.carService.AllCars().ToList()
            }); 
        }

        [HttpPost]
        public IActionResult Rent(RentFormModel car)
        {
            var dealerId = dealerService.IdByUser(User.GetId());

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            //if (carService.CategoryExists(car.CategoryId) == false)
            //{
            //    ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist.");

            //}

            //if (ModelState.IsValid == false)
            //{
            //    car.Categories = this.carService.AllCategories();

            //    return View(car);
            //}

            //if (car.Price == 0)
            //{
            //    return RedirectToAction(nameof(Add));

            //}

            //this.carService.Create(car.Brand, car.Model, car.Description, car.Price, car.ImageUrl, car.Year, car.CategoryId, dealerId);

            //TempData[GlobalMessageKey] = "Thank you for adding your car!";

            return Ok();
        }
    }
}
