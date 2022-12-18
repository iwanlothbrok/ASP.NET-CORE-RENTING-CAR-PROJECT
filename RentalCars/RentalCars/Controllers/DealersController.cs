namespace CarRentingSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Controllers;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Models.Dealers;
    using RentalCars.Core.Models.Renting;
    using RentalCars.Core.Services.Bookings;
    using RentalCars.Core.Services.Dealers;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.Web;

    public class DealersController : BaseController
    {
        private readonly IDealerService dealerService;
        private readonly IBookingService booking;

        public DealersController(IDealerService dealerService, IBookingService booking)
        {
            this.dealerService = dealerService;
            this.booking = booking;
        }

        [HttpGet]
        public IActionResult Become() => View();

        [HttpPost]
        public IActionResult Become(BecomeDealerFormModel dealer)
        {
            if (this.dealerService.IsDealer(User.GetId()))
            {
                return RedirectToAction("Error", "Home");
            }

            if (this.ModelState.IsValid == false)
            {
                return View(dealer);
            }

            this.dealerService.Become(dealer, userId: User.GetId());

            TempData[GlobalMessageKey] = "You become dealer successfully!";

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Rent()
        {
            string userId = User.GetId();
            int dealerId = dealerService.IdByUser(userId);

            IEnumerable<AdminBookingModel> bookings = this.booking
                .All(confirmByAdmin: false, confirmByDealer: false)
                .Bookings
                .Where(d => d.DealerId == dealerId)
                .ToList();

            return View(bookings);
        }

        public IActionResult GetRentedCars()
        {
            string userId = User.GetId();
            int dealerId = dealerService.IdByUser(userId);

            IEnumerable<AdminBookingModel> bookings = this.booking
                .All(confirmByAdmin: true, confirmByDealer: true)
                .Bookings
                .Where(c => c.DealerId == dealerId)
                .ToList();

            return View(bookings);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.booking.ChangeVisilityByDealer(id);

            int carId = this.booking.FindCarBookingId(id);

            this.booking.IsRented(id, carId);

            TempData[GlobalMessageKey] = "You change visibility successfully!";

            return Redirect("https://localhost:7163/");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            bool findBook = this.booking.Delete(id);

            if (findBook == true)
            {
                TempData[GlobalMessageKey] = "You delete booking successfully!";
            }

            return RedirectToAction("Index", "Home");
        }
    }
}