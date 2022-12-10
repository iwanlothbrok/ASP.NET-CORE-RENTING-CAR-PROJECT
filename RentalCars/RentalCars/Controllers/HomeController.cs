namespace RentalCars.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Models.Home;
    using RentalCars.Core.Services.Bookings;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Services.Cars.Models;
    using RentalCars.Core.Services.Dealers;

    public class HomeController : Controller
    {
        public ICarService cars;
        private readonly IMemoryCache cache;
        private readonly IBookingService bookingService;
        private readonly ICarService carService;
        private readonly IDealerService dealerService;

        public HomeController(ICarService cars, IMemoryCache cache, IBookingService booking, ICarService carService, IDealerService dealerService)
        {
            this.cars = cars;
            this.cache = cache;
            this.bookingService = booking;
            this.carService = carService;   
            this.dealerService = dealerService;   
        }

        public IActionResult Index()
        {
            const string latestCarsCacheKey = "LatestCarsCacheKey";

            var latestCars = this.cache.Get<List<CarServiceModel>>(latestCarsCacheKey);

            if (latestCars == null)
            {
                latestCars = this.cars
                   .GetLastThreeCars()
                   .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(latestCarsCacheKey, latestCars, cacheOptions);
            }

            return View(latestCars);
        }

        public IActionResult Error()
       => View();

        [HttpGet]
        public IActionResult Pay()
        {
            var model = new PaymentsModel();

            var bookInfo = this.bookingService.GetBookByUserId(User.GetId());

            if (bookInfo is null)
            {
                return RedirectToAction("Error", "Home");
            }

            var carId = this.bookingService.FindCarBookingId(bookInfo.Id);

            if (carId is 0)
            {
                return RedirectToAction("Error", "Home");
            }

            var car = carService.FindCar(carId);

            if (car is null)
            {
                return RedirectToAction("Error", "Home");
            }

            var dealer = dealerService.GetDealer(car.DealerId);

            if (dealer is null)
            {
                return RedirectToAction("Error", "Home");
            }

            model.BookingId = bookInfo.Id;
            model.Book = bookInfo;
            model.CarId = carId;
            model.Car = car;
            model.Dealer = dealer;

            return View(model);
        }

        [HttpPost]
        public IActionResult Pay(PaymentsModel model)
        {
            


            if (this.ModelState.IsValid == false)
            {
                car.Categories = this.carService.AllCategories();

                return View(car);
            }

            TempData[GlobalMessageKey] = "Thank you for adding your car!";

            return RedirectToAction(nameof(Mine));
        }

    }
}

