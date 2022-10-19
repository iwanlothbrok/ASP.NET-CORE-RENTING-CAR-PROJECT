namespace RentalCars.Controllers
{
    using CarRentingSystem.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Core.Models.Cars;
    using RentalCars.Core.Models.Dealers;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Services.Cars.Models;
    using RentalCars.Core.Services.Dealers;
    using RentalCars.Data;
    public class CarsController : BaseController
    {
        private readonly ICarService carService;
        private readonly IDealerService dealerService;


        public CarsController(IDealerService dealer, ICarService car, ApplicationDbContext data)
        {
            this.carService = car;
            this.dealerService = dealer;
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

            this.carService.Create(car.Brand, car.Model, car.Description, car.ImageUrl, car.Year, car.CategoryId, dealerId);


            return RedirectToAction(nameof(All));
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
                return Unauthorized();
            }
            return View(new CarFormModel
            {
                Brand = car.Brand,
                Model = car.Model,
                Description = car.Description,
                ImageUrl = car.ImageUrl,
                Year = car.Year,
                CategoryId = car.CategoryId,
                Categories = carService.AllCategories()
            });
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
                this.ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                car.Categories = this.carService.AllCategories();

                return View(car);
            }

            if (!this.carService.IsByDealer(id, dealerId))
            {
                return BadRequest();
            }

            this.carService.Edit(
                id,
                car.Brand,
                car.Model,
                car.Description,
                car.ImageUrl,
                car.Year,
                car.CategoryId);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public IActionResult Mine()
        {
            var myCars = this.carService.ByUser(this.User.GetId());

            return View(myCars);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var car = carService.Details(id);


            return View(new CarDetailsServiceModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Description = car.Description,
                ImageUrl = car.ImageUrl,
                Year = car.Year,
                CategoryId = car.CategoryId,
                CategoryName = car.CategoryName,
            });
        }
        [HttpPost]
        public IActionResult Details(int id, CarDetailsServiceModel car)
        {
            
            if (!this.carService.CategoryExists(car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {  

                return View(car);
            }
           
            return View(car);
        }
    }
}