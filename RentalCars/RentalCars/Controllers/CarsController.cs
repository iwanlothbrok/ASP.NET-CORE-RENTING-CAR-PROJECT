namespace RentalCars.Controllers
{
    using CarRentingSystem.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;
    using RentalCars.Models.Cars;
    using RentalCars.Models.Dealers;
    using RentalCars.Services.Cars;
    using RentalCars.Services.Cars.Models;
    using RentalCars.Services.Dealers;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants;
    public class CarsController : BaseController
    {
        private readonly ApplicationDbContext data;
        private readonly ICarService carService;
        private readonly IDealerService dealerService;


        public CarsController(IDealerService dealer,ICarService car, ApplicationDbContext data)
        {
            this.carService = car;
            this.data = data;
            this.dealerService = dealer;
        }

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
                Categories = this.GetCarCategories()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(CarFormModel car)
        {
            if (dealerService.IsDealer(User.GetId()) == false)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }


            if (cars.Cas == false)
            {
                ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist.");

            }

            if (ModelState.IsValid == false)
            {
                car.Categories = this.GetCarCategories();

                return View(car);
            }

            var carData = new Car
            {
                Brand = car.Brand,
                Model = car.Model,
                Description = car.Description,
                ImageUrl = car.ImageUrl,
                Year = car.Year,
                CategoryId = car.CategoryId,
                DealerId = dealerService.IdByUser(User.GetId())
            };

            await data.Cars.AddAsync(carData);
            await data.SaveChangesAsync();


            ViewData[MessageConstant.SuccsessMessage] = "Your car is added!";
            return RedirectToAction(nameof(All));
        }

        private IEnumerable<CarCategoryServiceModel> GetCarCategories()
            => data
                .Categories
                .Select(c => new CarCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

      
        
    }
}
