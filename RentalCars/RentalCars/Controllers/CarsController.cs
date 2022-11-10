namespace RentalCars.Controllers
{
    using AutoMapper;
    using CarRentingSystem.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Models.Cars;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Services.Cars.Models;
    using RentalCars.Core.Services.Dealers;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.Web;

    public class CarsController : BaseController
    {
        private readonly ICarService carService;
        private readonly IDealerService dealerService;
        private readonly IMapper mapper;

        public CarsController(IDealerService dealer, ICarService car, IMapper mapper)
        {
            this.carService = car;
            this.dealerService = dealer;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult Add()
        {
            if (this.dealerService.IsDealer(User.GetId()) == false)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            return View(new CarFormModel
            {
                Categories = this.carService.AllCategories()
            });
        }

        [HttpPost]
        public IActionResult Add(CarFormModel car)
        {
            int dealerId = this.dealerService.IdByUser(User.GetId());

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (this.carService.CategoryExists(car.CategoryId) == false)
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist.");

            }

            if (this.ModelState.IsValid == false)
            {
                car.Categories = this.carService.AllCategories();

                return View(car);
            }

            if (car.Price == 0)
            {
                return RedirectToAction(nameof(Add));
            }

            this.carService.Create(car.Brand, car.Model, car.Description, car.Price, car.ImageUrl, car.Year, car.CategoryId, dealerId);

            TempData[GlobalMessageKey] = "Thank you for adding your car!";

            return RedirectToAction(nameof(Mine));
        }


        [HttpGet]
        public IActionResult All([FromQuery] AllCarsQueryModel query)
        {
            CarQueryServiceModel queryResult = this.carService.All(
                 query.Brand,
                 query.SearchTerm,
                 query.Sorting,
                 query.CurrentPage,
                 AllCarsQueryModel.CarsPerPage);

            IEnumerable<string> carBrands = this.carService
                .AllBrands()
                .ToList();

            query.Brands = carBrands;
            query.TotalCars = queryResult.TotalCars;
            query.Cars = queryResult.Cars;

            return View(query);
        }


        [HttpGet]
        public IActionResult Details(int id, string information)
        {
            CarDetailsServiceModel car = this.carService.Details(id);

            if (this.ModelState.IsValid == false)
            {
                return RedirectToAction("Error", "Home");
            }
            if (information != car.GetInformationUrl())
            {
                return RedirectToAction("Error", "Home");
            }

            CarDetailsServiceModel carForm = this.mapper.Map<CarDetailsServiceModel>(car);

            return View(carForm);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Error", "Home");
            }

            var user = this.User.GetId();

            if (user == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var dealerId = this.dealerService.IdByUser(user);

            if (dealerId == 0)
            {
                return RedirectToAction("Error", "Home");
            }

            if (this.carService.Delete(id, dealerId) == false)
            {
                return RedirectToAction("Error", "Home");
            }

            TempData[GlobalMessageKey] = "You delete your car successfully!";

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            string userId = this.User.GetId();

            if (dealerService.IsDealer(userId) == false)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            CarDetailsServiceModel car = this.carService.Details(id);

            if (car.UserId != userId)
            {
                return RedirectToAction("Error", "Home");
            }

            CarFormModel carForm = this.mapper.Map<CarFormModel>(car);
            carForm.Categories = this.carService.AllCategories();

            return View(carForm);
        }   

        [HttpPost]
        public IActionResult Edit(int id, CarFormModel car)
        {
            int dealerId = this.dealerService.IdByUser(this.User.GetId());

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (this.carService.CategoryExists(car.CategoryId) == false)
            {
                return RedirectToAction("Error", "Home");
            }

            if (this.ModelState.IsValid == false)
            {
                car.Categories = this.carService.AllCategories();

                return View(car);
            }

            if (!this.carService.IsByDealer(id, dealerId))
            {
                return RedirectToAction("Error", "Home");
            }

            this.carService.Edit(
                id,
                car.Brand,
                car.Model,
                car.Price,
                car.Description,
                car.ImageUrl,
                car.Year,
                car.CategoryId);

            TempData[GlobalMessageKey] = "You edit your car successfully!";

            return RedirectToAction(nameof(Mine));
        }

        [HttpGet]
        public IActionResult Mine()
        {
            IEnumerable<CarServiceModel> myCars = this.carService
                .ByUser(this.User.GetId())
                .ToList();

            return View(myCars);
        }
    }
}