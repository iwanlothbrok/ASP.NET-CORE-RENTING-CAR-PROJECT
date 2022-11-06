namespace RentalCars.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Core.Services.Bookings;
    using RentalCars.Core.Services.Cars;


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
            var bookings = this.booking
                .All()
                .Bookings;

            return View(bookings);
        }


        public IActionResult ChangeVisibility(int id)
        {
            this.booking.ChangeVisility(id);


            return Ok();
        }
    }
}
