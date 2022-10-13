namespace RentalCars.Controllers
{
    using CarRentingSystem.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;
    using RentalCars.Models.Cars;
    using RentalCars.Models.Dealers;
    using RentalCars.Services.Cars;
    using RentalCars.Services.Dealers;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants;
    public class CarsController : BaseController
    {
        private readonly ApplicationDbContext data;
        private readonly ICarService car;
        private readonly IDealerService dealer;


        public CarsController(IDealerService dealer,ICarService car, ApplicationDbContext data)
        {
            this.car = car;
            this.data = data;
            this.dealer = dealer;
        }

        public IActionResult All([FromQuery] AllCarsQueryModel query)
        {
            var queryResult = this.car.All(
                 query.Brand,   
                 query.SearchTerm,
                 query.Sorting,
                 query.CurrentPage,
                 AllCarsQueryModel.CarsPerPage);

            var carBrands = this.car.AllCarBrands();


            query.Brands = carBrands;
            query.TotalCars = queryResult.TotalCars;
            query.Cars = queryResult.Cars;

            return View(query);
        }


        [HttpGet]
        public IActionResult Add()
        {
            if (dealer.IsDealer(User.GetId()) == false)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            return View(new AddCarFormModel
            {
                Categories = this.GetCarCategories()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCarFormModel car)
        {
            if (dealer.IsDealer(User.GetId()) == false)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }


            if (CategoryExists(car.CategoryId) == false)
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
                DealerId = dealer.IdByUser(User.GetId())
            };

            await data.Cars.AddAsync(carData);
            await data.SaveChangesAsync();


            ViewData[MessageConstant.SuccsessMessage] = "Your car is added!";
            return RedirectToAction(nameof(All));
        }

        private IEnumerable<CarCategoryModel> GetCarCategories()
            => data
                .Categories
                .Select(c => new CarCategoryModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        public bool CategoryExists(int categoryId)
         => data
             .Categories
             .Any(c => c.Id == categoryId);

        
    }
}
