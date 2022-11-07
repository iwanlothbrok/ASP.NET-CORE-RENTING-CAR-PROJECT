namespace RentalCars.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Models.Renting;
    using RentalCars.Core.Services.Bookings;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Services.Dealers;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.Web;


    public class BookingController : BaseController
    {

        private readonly ICarService carService;
        private readonly IDealerService dealerService;
        private readonly IBookingService bookingService;
        private readonly IMapper mapper;
        public BookingController(IDealerService dealer, ICarService car, IBookingService bookingService, IMapper mapper)
        {
            this.carService = car;
            this.dealerService = dealer;
            this.bookingService = bookingService;
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
            if (bookingService.UserHasBookedCar(userId))
            {
                return RedirectToAction("Error", "Home");
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
            var carId = model.CarId;
            var car = carService.FindCar(carId);
            if (car.IsBooked == true || car.IsPublic == false)
            {
                return RedirectToAction("Error", "Home");
            }
            var price = car.Price;

            var isDateValid = bookingService.DateChecker(model.BookingDate, model.ReturningDate);

            if (isDateValid == false)
            {
                return RedirectToAction("Error", "Home");
            }

           
            var isValid = this.bookingService.CreateBooking(model.CustomerFirstName, model.CustomerLastName, userId, dealer, model.BookingDate, price, model.ReturningDate, false, model.CarId);

            if (isValid == 0)
            {
                TempData[GlobalMessageKey] = "Your request is on the waitlist!";

                return RedirectToAction("Index", "Home");

            }
            TempData[GlobalMessageKey] = "Thank you for renting our car, your request is on the waitlist!";

            return RedirectToAction("Index", "Home");
        }
       
    }
}
