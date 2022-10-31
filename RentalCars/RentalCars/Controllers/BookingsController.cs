namespace RentalCars.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Models.Bookings;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Services.Dealers;

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

            var car = carService.FindCar(id);

            var userId = User.GetId();

            if (ModelState.IsValid == false)
            {
                return RedirectToAction("Error", "Home");
            }
            if (dealerService.IsDealer(userId))
            {
                var dealerId = dealerService.IdByUser(userId);
                if (carService.IsByDealer(id, dealerId))
                {
                   return RedirectToAction("Error", "Home");
                }
            }

           return View(new BookingFormModel
           {
               ImageUrl = car.ImageUrl,
               Price = car.Price
           });

        }

        [HttpPost]
        public IActionResult Booking(BookingFormModel model)
        {
            var user = User.GetId();
            if (user == null)
            {
                return RedirectToAction("Error", "Cars");
            }

            

            
            return Ok();
        }
    }
}
