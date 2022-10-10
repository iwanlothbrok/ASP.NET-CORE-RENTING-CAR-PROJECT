namespace RentalCars.Controllers
{
    using CarRentingSystem.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;
    using RentalCars.Models.Cars;
    using RentalCars.Models.Dealers;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants;
    public class CarsController : BaseController
    {
        private readonly ApplicationDbContext data;


        public CarsController(ApplicationDbContext data)
            => this.data = data;


        public IActionResult All([FromQuery] AllCarsQueryModel query)
        {
            var carsQuery = this.data.Cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Brand))
            {
                carsQuery = carsQuery.Where(c => c.Brand == query.Brand);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                carsQuery = carsQuery.Where(c =>
                    (c.Brand + " " + c.Model).ToLower().Contains(query.SearchTerm.ToLower()) ||
                    c.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            carsQuery = query.Sorting switch
            {
                CarSorting.Year => carsQuery.OrderByDescending(c => c.Year),
                CarSorting.BrandAndModel => carsQuery.OrderBy(c => c.Brand).ThenBy(c => c.Model),
                CarSorting.DateCreated or _ => carsQuery.OrderByDescending(c => c.Id)
            };

            var totalCars = carsQuery.Count();

            var cars = carsQuery
                .Skip((query.CurrentPage - 1) * AllCarsQueryModel.CarsPerPage)
                .Take(AllCarsQueryModel.CarsPerPage)
                .Select(c => new CarListingViewModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    ImageUrl = c.ImageUrl,
                    Category = c.Category.Name
                })
                .ToList();

            var carBrands = this.data
                .Cars
                .Select(c => c.Brand)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

            query.TotalCars = totalCars;
            query.Brands = carBrands;
            query.Cars = cars;

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
            if (!CategoryExists(car.CategoryId))
            {
                this.ModelState.AddModelError(nameof(car.CategoryId), "Category does not exist.");

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
                CategoryId = car.CategoryId
            };

            await this.data.Cars.AddAsync(carData);
            await this.data.SaveChangesAsync();


            ViewData[MessageConstant.SuccsessMessage] = "Welcome to the Warehouse!";
            return RedirectToAction(nameof(All));
        }

        private IEnumerable<CarCategoryModel> GetCarCategories()
            => this.data
                .Categories
                .Select(c => new CarCategoryModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        public bool CategoryExists(int categoryId)
         => this.data
             .Categories
             .Any(c => c.Id == categoryId);

        private bool UserIsDealer()
           => this.data
               .Dealers
               .Any(d => d.UserId == this.User.GetId());
    }
}
