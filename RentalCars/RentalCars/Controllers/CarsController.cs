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
        public IActionResult All([FromQuery] AllCarsQueryModel query)
        {
            var queryResult = this.carService.All(
                 query.Brand,
                 query.SearchTerm,
                 query.Sorting,
                 query.CurrentPage,
                 AllCarsQueryModel.CarsPerPage);

            var carBrands = this.carService.AllBrands();


            query.Brands = carBrands;
            query.TotalCars = queryResult.TotalCars;
            query.Cars = queryResult.Cars;

            return View(query);
        }


        [HttpGet]
        public IActionResult Add()
        {
            if (dealerService.IsDealer(User.GetId()) == false)
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
            var dealerId = dealerService.IdByUser(User.GetId());

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (carService.CategoryExists(car.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist.");

            }

            if (ModelState.IsValid == false)
            {
                car.Categories = this.carService.AllCategories();

                return View(car);
            }

            this.carService.Create(car.Brand, car.Model, car.Description,car.Price, car.ImageUrl, car.Year, car.CategoryId, dealerId);

            TempData[GlobalMessageKey] = "Thank you for adding your car!";

            return RedirectToAction(nameof(Mine));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userId = User.GetId();

            if (dealerService.IsDealer(userId) == false)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");

            }

            var car = carService.Details(id);

            if (car.UserId != userId)
            {
                return RedirectToAction("Error", "Home");
            }

            var carForm = this.mapper.Map<CarFormModel>(car);
            carForm.Categories = this.carService.AllCategories();

            return View(carForm);
        }

        [HttpPost]
        public IActionResult Edit(int id, CarFormModel car)
        {
            var dealerId = this.dealerService.IdByUser(this.User.GetId());

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (!this.carService.CategoryExists(car.CategoryId))
            {
                return RedirectToAction("Error", "Home");
            }

            if (!ModelState.IsValid)
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
            var myCars = this.carService.ByUser(this.User.GetId());

            return View(myCars);
        }

        [HttpGet]
        public IActionResult Details(int id, string information)
        {
            var car = carService.Details(id);

            if (ModelState.IsValid == false)
            {
                return RedirectToAction("Error", "Home");
            }
            if (information != car.GetInformationUrl())
            {
                return RedirectToAction("Error", "Home");
            }

            var carForm = this.mapper.Map<CarDetailsServiceModel>(car);


            return View(carForm);

        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Error", "Home");
            }
            var user = User.GetId();

            if (user == null)
            {
                return RedirectToAction("Error", "Home");
            }
            var dealerId = dealerService.IdByUser(user);

            if (dealerId == 0)
            {
                return RedirectToAction("Error", "Home");
            }

            if (carService.Delete(id, dealerId) == false)
            {
                return RedirectToAction("Error","Home");
            }

            TempData[GlobalMessageKey] = "You delete your car successfully!";


            return RedirectToAction(nameof(All));
        }

      
      
    }
}