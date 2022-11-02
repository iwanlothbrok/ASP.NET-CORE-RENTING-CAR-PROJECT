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

        //public IActionResult Rent()
        //{
        //    var cars = this.cars
        //        .All(publicOnly: false)
        //        .Cars;

        //    return View(cars);
        //}

        //public IActionResult ChangeVisibility(int id)
        //{
        //    this.cars.ChangeVisility(id);


        //    return RedirectToAction(nameof(All));
        //}
    }
}
