namespace RentalCars.Controllers
{
    using CarRentingSystem.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;
    using RentalCars.Models.Cars;
    using RentalCars.Models.Dealers;
    using RentalCars.Services.Cars;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants;
    public class CarsController : BaseController
    {
        private readonly ApplicationDbContext data;
        private readonly ICarService carService;


        public CarsController(ICarService _carService, ApplicationDbContext _data)
        {
            carService = _carService;
            data = _data;
        }

        public IActionResult All([FromQuery] AllCarsQueryModel query)
        {
            var queryResult = this.carService.All(
                 query.Brand,   
                 query.SearchTerm,
                 query.Sorting,
                 query.CurrentPage,
                 AllCarsQueryModel.CarsPerPage);

            var carBrands = this.carService.AllCarBrands();


            query.Brands = carBrands;
            query.TotalCars = queryResult.TotalCars;
            query.Cars = queryResult.Cars;

            return View(query);
        }


        [HttpGet]
        public IActionResult Add()
        {
            if (!this.UserIsDealer())
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
            var dealerId = data
               .Dealers
               .Where(d => d.UserId == this.User.GetId())
               .Select(d => d.Id)
               .FirstOrDefault();

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }


            if (!CategoryExists(car.CategoryId))
            {
                ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist.");

            }

            if (!ModelState.IsValid)
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
                DealerId = dealerId
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

        private bool UserIsDealer()
           => data
               .Dealers
               .Any(d => d.UserId == this.User.GetId());
    }
}
