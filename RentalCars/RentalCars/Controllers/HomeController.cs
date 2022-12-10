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
    using RentalCars.Core.Services.DebitCards;
    using RentalCars.Core.Services.Payments;
    using RentalCars.Infrastructure.Data.Models;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.Web;

    public class HomeController : Controller
    {
        public ICarService cars;
        private readonly IMemoryCache cache;
        private readonly IBookingService bookingService;
        private readonly ICarService carService;
        private readonly IDealerService dealerService;
        private readonly IDebitCardService debitCardService;
        private readonly IPaymentService paymentService;

        public HomeController(ICarService cars,
            IMemoryCache cache,
            IBookingService bookingService,
            ICarService carService,
            IDealerService dealerService,
            IDebitCardService debitCardService,
            IPaymentService paymentService)
        {
            this.cars = cars;
            this.cache = cache;
            this.bookingService = bookingService;
            this.carService = carService;
            this.dealerService = dealerService;
            this.debitCardService = debitCardService;
            this.paymentService = paymentService;
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
            PaymentsModel model = new PaymentsModel();

            Booking? bookInfo = this.bookingService.GetBookByUserId(User.GetId());

            if (bookInfo is null)
            {
                return RedirectToAction("Error", "Home");
            }

            int carId = this.bookingService.FindCarBookingId(bookInfo.Id);

            if (carId is 0)
            {
                return RedirectToAction("Error", "Home");
            }

            Car? car = carService.FindCar(carId);

            if (car is null)
            {
                return RedirectToAction("Error", "Home");
            }

            Dealer? dealer = dealerService.GetDealer(car.DealerId);

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
            int debitCardId = debitCardService.CreateDebitCard(int.Parse(model.CreditCardNumber), model.CVV, model.NameOnCard, model.ExpMonth, model.ExpYear);

            if (debitCardId == -1)
            {
                ModelState.AddModelError(model.CreditCardNumber.ToString(), "Problem with payment, try again!");
            }

            if (this.ModelState.IsValid == false)
            {
                model.BookingId = model.BookingId;
                model.Book = model.Book;
                model.CarId = model.CarId;
                model.Car = model.Car;
                model.Dealer = model.Dealer;

                return View(model);
            }

            int payment = paymentService.CreatePaymentInfo((int)model.BookingId, (int)model.CarId, true);

            if (payment == -1)
            {
                return View(model);
            }

            TempData[GlobalMessageKey] = "Thank you for renting your car, the dealer will call you soon! Best Regards!";

            return RedirectToAction(nameof(Index));
        }

    }
}

