namespace CarRentingSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Controllers;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Models.Dealers;
    using RentalCars.Core.Models.Renting;
    using RentalCars.Core.Services.Bookings;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Services.Dealers;
    using RentalCars.Data;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.Web;

    public class DealersController : BaseController
    {
        private readonly IDealerService dealerService;
        private readonly IBookingService booking;
        private readonly ICarService car;
        private readonly ApplicationDbContext data;

        public DealersController(IDealerService dealerService, IBookingService booking, ICarService car, ApplicationDbContext data)
        {
            this.dealerService = dealerService;
            this.booking = booking;
            this.car = car;
            this.data = data;
        }

        [HttpGet]
        public IActionResult Become() => View();

        [HttpPost]
        public async Task<IActionResult> Become(BecomeDealerFormModel dealer)
        {
            if (dealerService.IsDealer(User.GetId()))
            {
                return RedirectToAction("Error", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(dealer);
            }

            dealerService.Become(dealer, userId: User.GetId());

            TempData[GlobalMessageKey] = "You become dealer successfully!";

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Rent()
        {
            var userId = User.GetId();

            int dealerId = dealerService.IdByUser(userId);




            if (this.booking.CheckIfIsDealer(dealerId) == false)
            {
                return RedirectToAction("Error", "Home");
            }
           



            var bookings = this.booking
                .All()
                .Bookings;

            return View(bookings);
        }
        public IActionResult ChangeVisibility(int id)
        {
            this.booking.ChangeVisilityByDealer(id);

            var carId = this.booking.FindCar(id);

            this.booking.IsRented(id, carId);

            return Redirect("https://localhost:7163/");
        }

    }
}