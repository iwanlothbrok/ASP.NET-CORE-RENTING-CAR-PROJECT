namespace RentalCars.Controllers
{
    using AutoMapper;
    using CarRentingSystem.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Models.Renting;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Services.Dealers;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.Web;

    public class RentingController : BaseController
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
        public IActionResult Rent(RentFormModel model)
        {
            var userId = User.GetId();
            var dealer = dealerService.IdByUser(userId);

            if (dealer != 0)
            {
                if (carService.IsByDealer(model.CarId, dealer))
                {
                    return RedirectToAction("Error", "Home");
                }
            }

            if (carService.CarsExists(model.CarId) == false)
            {
                return RedirectToAction("Error", "Home");
            }

            if (ModelState.IsValid == false)
            {
                model.Cars = this.carService.AllCars().ToList();

                return View(model);
            }


           var id = this.carService.CreateBooking(model.CustomerFirstName, model.CustomerLastName, userId,dealer, model.BookingDate, model.ReturningDate, model.CarId);

            TempData[GlobalMessageKey] = "Thank you for adding your car!";

            return RedirectToAction("Index","Home");
        }
    }
}
