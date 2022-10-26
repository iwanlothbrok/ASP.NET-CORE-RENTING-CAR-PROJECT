namespace CarRentingSystem.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Controllers;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Models.Dealers;
    using RentalCars.Core.Services.Dealers;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.Web;

    public class DealersController : BaseController
    {
        private readonly ApplicationDbContext data;
        private readonly IDealerService dealerService;
        private readonly IMapper mapper;

        public DealersController(ApplicationDbContext data, IDealerService dealer, IMapper mapper)
        {

            this.data = data;
            this.dealerService = dealer;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Become() => View();

        [HttpPost]
        public async Task<IActionResult> Become(BecomeDealerFormModel dealer)
        {
            if (dealerService.IsDealer(User.GetId()))
            {
                return RedirectToAction("Error", "Cars");
            }

            if (!ModelState.IsValid)
            {
                return View(dealer);
            }

            var dealerForm = this.mapper.Map<Dealer>(dealer);
            dealerForm.UserId = User.GetId();

            await data.Dealers.AddAsync(dealerForm);
            await data.SaveChangesAsync();

            TempData[GlobalMessageKey] = "You become dealer successfully!";

            return RedirectToAction("All", "Cars");
        }


    }
}