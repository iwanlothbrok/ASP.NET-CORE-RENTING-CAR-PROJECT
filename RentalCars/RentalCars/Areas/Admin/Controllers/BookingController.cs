namespace RentalCars.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Core.Models.Renting;
    using RentalCars.Core.Services.Bookings;
    using RentalCars.Core.Services.Cars;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.Web;

    [Area(Constants.AreaName)]
    [Authorize(Roles = Constants.AreaName)]
    public class BookingController : Controller
    {
        private readonly ICarService cars;
        private readonly IBookingService booking;

        public BookingController(ICarService cars, IBookingService booking)
        {
            this.cars = cars;
            this.booking = booking;
        }

        public IActionResult Rent()
        {
            IEnumerable<AdminBookingModel> bookings = this.booking
                .All()
                .Bookings
                .ToList();

            return View(bookings);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.booking.ChangeVisilityByAdmin(id);

            int carId = this.booking.FindCar(id);

            this.booking.IsRented(id, carId);

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

            return View();
        }
    }
}
