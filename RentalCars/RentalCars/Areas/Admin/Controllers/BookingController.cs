namespace RentalCars.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Core.Models.Renting;
    using RentalCars.Core.Services.Bookings;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.Web;

    [Area(Constants.AreaName)]
    [Authorize(Roles = Constants.AreaName)]
    public class BookingController : Controller
    {
        private readonly IBookingService booking;

        public BookingController(IBookingService booking)
        {
            this.booking = booking;
        }

        public IActionResult Rent()
        {
            IEnumerable<AdminBookingModel> bookings = this.booking
                .All(confirmByAdmin: false, confirmByDealer: false)
                .Bookings
                .ToList();

            return View(bookings);
        }

        public IActionResult GetOfferts()
        {
            IEnumerable<AdminBookingModel> bookings = this.booking
                .All(confirmByAdmin: true, confirmByDealer: true)
                .Bookings
                .ToList();

            return View(bookings);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.booking.ChangeVisilityByAdmin(id);

            int carId = this.booking.FindCarBookingId(id);

            this.booking.IsRented(id, carId);

            return RedirectToActionPermanent("", "/");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            bool findBook = this.booking.Delete(id);

            if (findBook == true)
            {
                TempData[GlobalMessageKey] = "You delete booking successfully!";
            }

            return View();
        }
    }
}
