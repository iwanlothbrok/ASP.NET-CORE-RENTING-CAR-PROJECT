namespace RentalCars.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Models.Bookings;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Services.Cars.Models;
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
        public IActionResult Booking(int id)
        {

            var car = carService.Details(id);

            if (ModelState.IsValid == false)
            {
                return RedirectToAction("Error", "Cars");
            }


            var carForm = this.mapper.Map<BookingFormModel>(car);


            return View(carForm);

        }

        [HttpPost]
        public IActionResult Booking(BookingFormModel car)
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

            if (car.CarId != 0
                && car.Model != null
                && car.Brand != null
                && car.DealerId != 0)
            {
                var booking = new Booking
                {
                    CarId = car.CarId,
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
