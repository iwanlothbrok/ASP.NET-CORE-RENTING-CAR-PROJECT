namespace CarRentingSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Controllers;
    using RentalCars.Core.Models.Dealers;
    using RentalCars.Core.Services.Dealers;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;

    public class DealersController : BaseController
    {
        private readonly ApplicationDbContext data;
        private readonly IDealerService dealerService;

        public DealersController(ApplicationDbContext data, IDealerService dealer)
        {

            this.data = data;
            this.dealerService = dealer;
        }

        [HttpGet]
        public IActionResult Become() => View();

        [HttpPost]
        public async Task<IActionResult> Become(BecomeDealerFormModel dealer)
        {

            if (dealerService.IsDealer(User.GetId()))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(dealer);
            }

            var dealerData = new Dealer
            {
                Name = dealer.Name,
                PhoneNumber = dealer.PhoneNumber,
                UserId = User.GetId(),
            };

            await data.Dealers.AddAsync(dealerData);
            await data.SaveChangesAsync();

            return RedirectToAction("All", "Cars");
        }
    }
}