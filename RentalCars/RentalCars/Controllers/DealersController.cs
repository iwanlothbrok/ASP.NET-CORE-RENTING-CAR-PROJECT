namespace CarRentingSystem.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Controllers;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;
    using RentalCars.Models.Dealers;
    using System.Linq;

    public class DealersController : BaseController
    {
        private readonly ApplicationDbContext data;

        public DealersController(ApplicationDbContext data)
            => this.data = data;

        [Authorize]
        [HttpGet]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Become(BecomeDealerFormModel dealer)
        {
            var userId = this.User.GetId();

            var userIdAlreadyDealer = this.data
                .Dealers
                .Any(d => d.UserId == userId);

            if (userIdAlreadyDealer)
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
                UserId = userId
            };

            await data.Dealers.AddAsync(dealerData);
            await data.SaveChangesAsync();

            return RedirectToAction("All", "Cars");
        }
    }
}