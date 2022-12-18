namespace RentalCars.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Models.Home;
    using RentalCars.Core.Services.Bookings;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Services.Dealers;
    using RentalCars.Core.Services.DebitCards;
    using RentalCars.Core.Services.Payments;
    using RentalCars.Infrastructure.Data.Models;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.Web;

    public class PaymentController : BaseController
    {
        public ICarService cars;
        private readonly IBookingService bookingService;
        private readonly ICarService carService;
        private readonly IDealerService dealerService;
        private readonly IDebitCardService debitCardService;
        private readonly IPaymentService paymentService;

        public PaymentController(ICarService cars,
            IBookingService bookingService,
            ICarService carService,
            IDealerService dealerService,
            IDebitCardService debitCardService,
            IPaymentService paymentService)
        {
            this.cars = cars;
            this.bookingService = bookingService;
            this.carService = carService;
            this.dealerService = dealerService;
            this.debitCardService = debitCardService;
            this.paymentService = paymentService;
        }

        [HttpGet]
        public IActionResult Pay()
        {
            PaymentsModel model = new PaymentsModel();

            if (GetBookingAndCarInfo(model) == -1)
            {
                return RedirectToAction("Error", "Home");
            }

            Booking? booking = this.bookingService.GetBookByUserId(User.GetId());

            if (booking.IsPaid == true)
            {
                TempData[GlobalMessageKey] = "Thank you for renting your car, the dealer will call you soon! Best Regards!";

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Pay(PaymentsModel model)
        {
            string userId = User.GetId();
            if (model.CreditCardNumber.ToString().Length != 16 || model.CVV.ToString().Length != 3)
            {
                ModelState.AddModelError(model.CreditCardNumber.ToString(), "Problem with payment, try again!");
            }
            try
            {
                if (Enum.IsDefined(typeof(Months), model.ExpMonth.ToLower()) == false)
                {
                    ModelState.AddModelError(model.ExpMonth.ToString(), "Problem with payment, try again!");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(model.CreditCardNumber.ToString(), "Problem with card month, try again!");
            }

            if (this.ModelState.IsValid == false)
            {
                return RedirectToAction(nameof(Pay));
            }

            int debitCardId = debitCardService.CreateDebitCard(model.CreditCardNumber, model.CVV, model.NameOnCard, model.ExpMonth, model.ExpYear);

            if (debitCardId == -1)
            {
                return RedirectToAction(nameof(Pay));
            }

            if (GetBookingAndCarInfo(model) == -1)
            {
                return RedirectToAction("Error", "Home");
            }

            int payment = paymentService.CreatePaymentInfo((int)model.BookingId, (int)model.CarId, debitCardId, true, userId);

            if (payment == -1)
            {
                return View(model);
            }

            TempData[GlobalMessageKey] = "Thank you for renting your car, the dealer will call you soon! Best Regards!";

            return RedirectToAction(nameof(Index));
        }
        public int GetBookingAndCarInfo(PaymentsModel model)
        {
            Booking? bookInfo = this.bookingService.GetBookByUserId(User.GetId());

            if (bookInfo is null)
            {
                return -1;
            }

            int carId = this.bookingService.FindCarBookingId(bookInfo.Id);

            if (carId is 0)
            {
                return -1;
            }

            Car? car = carService.FindCar(carId);

            if (car is null)
            {
                return -1;
            }

            Dealer? dealer = dealerService.GetDealer(car.DealerId);

            if (dealer is null)
            {
                return -1;
            }

            model.BookingId = bookInfo.Id;
            model.Book = bookInfo;
            model.CarId = carId;
            model.Car = car;
            model.Dealer = dealer;

            return 1;
        }

        public IActionResult GetYouPaymentInfo()
        {
            PaymentsModel model = new PaymentsModel();

            if (GetBookingAndCarInfo(model) == -1)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(model);
        }
    }
}
