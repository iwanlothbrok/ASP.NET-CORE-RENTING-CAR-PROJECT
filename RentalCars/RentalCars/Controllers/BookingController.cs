namespace RentalCars.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Models.Renting;
    using RentalCars.Core.Services.Bookings;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Services.Dealers;
    using RentalCars.Infrastructure.Data.Models;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.Web;

    public class BookingController : BaseController
    {
        private readonly ICarService carService;
        private readonly IDealerService dealerService;
        private readonly IBookingService bookingService;

        public BookingController(IDealerService dealer, ICarService car, IBookingService bookingService)
        {
            this.carService = car;
            this.dealerService = dealer;
            this.bookingService = bookingService;

            bookingService.ReturningDateChecker(bookingService.GetBookingsAll());
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
            string userId = this.User.GetId();
            int dealer = this.dealerService.IdByUser(userId);

            if (this.bookingService.CheckUser(userId))
            {
                return RedirectToAction("Error", "Home");
            }

            if (dealer != 0)
            {
                if (this.carService.IsByDealer(model.CarId, dealer))
                {
                    return RedirectToAction("Error", "Home");
                }
            }

            if (this.bookingService.UserHasBookedCar(userId))
            {
                return RedirectToAction("Error", "Home");
            }

            if (this.carService.CarsExists(model.CarId) == false)
            {
                return RedirectToAction("Error", "Home");
            }

            if (this.ModelState.IsValid == false)
            {
                model.Cars = this.carService.AllCars().ToList();

                return View(model);
            }

            int carId = model.CarId;
            Car car = this.carService.FindCar(carId);

            if (car.IsBooked == true || car.IsPublic == false)
            {
                return RedirectToAction("Error", "Home");
            }

            decimal price = car.Price;

            bool isDateValid = this.bookingService.DateChecker(model.BookingDate, model.ReturningDate);

            if (isDateValid == false)
            {
                model.Cars = this.carService.AllCars().ToList();

                return View(model);
            }

            if (price < 0)
            {
                return RedirectToAction("Error", "Home");
            }
            this.bookingService.CreateBooking(model.CustomerFirstName, model.CustomerLastName, userId, car.DealerId, model.BookingDate, price, model.ReturningDate, model.CarId);

            TempData[GlobalMessageKey] = "Thank you for renting our car, your request is on the waitlist!";

            return RedirectToAction("Index", "Home");
        }
    }
}
