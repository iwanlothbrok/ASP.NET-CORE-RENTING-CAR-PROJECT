namespace RentalCars.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Services.Dealers;
    using RentalCars.Infrastructure.Data.Models;

    public class BookingsController : BaseController
    {
        private readonly ICarService carService;
        private readonly IDealerService dealerService;
        private readonly IMapper mapper;


        public BookingsController(IDealerService dealer, ICarService car, IMapper mapper)
        {
            this.carService = car;
            this.dealerService = dealer;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Booking(int Id)
       => View();

        [HttpPost]
        public IActionResult Booking(Car car)
        {
            var user = User.GetId();
            if (user == null)
            {
                return RedirectToAction("Error", "Cars");
            }

            if (car == null)
            {
                return RedirectToAction("Error", "Cars");
            }

            if (car.Id != 0
                && car.IsPublic == true
                && car.Model != null
                && car.Brand != null
                && car.DealerId != 0)
            {
                var booking = new Booking
                {
                    CarId = car.Id,
                    CarBrand = car.Brand,
                    CarModel = car.Model,
                    DealerId = car.DealerId,
                    UserId = user,

                };
            }
            return Ok();
        }
    }
}
