namespace RentalCars.Controllers
{
using Microsoft.AspNetCore.Mvc;
using RentalCars.Data;
using RentalCars.Infrastructure.Data.Models;
using RentalCars.Models.Cars;
    public class CarsController : BaseController
    {
        private readonly ApplicationDbContext data;


        public CarsController(ApplicationDbContext data)
            => this.data = data;

        [HttpGet]
        public IActionResult Add() => View(new CarFormModel
        {
            Categories = this.GetCarCategories()
        });

        [HttpPost]
        public async Task<IActionResult> Add(CarFormModel car)
        {
            if (!CategoryExists(car.CategoryId) || car.CategoryId == 0)
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

            return RedirectToAction("Index", "Home");
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
    }
}
