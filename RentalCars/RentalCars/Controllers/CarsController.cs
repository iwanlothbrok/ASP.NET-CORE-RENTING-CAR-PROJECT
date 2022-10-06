using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCars.Data;
using RentalCars.Infrastructure.Data.Models;
using RentalCars.Models.Cars;

namespace RentalCars.Controllers
{
    public class CarsController : BaseController
    {
        private readonly ApplicationDbContext data;

        public CarsController(ApplicationDbContext data)
            => this.data = data;

        public IActionResult Add() => View(new CarFormModel
        {
            Categories = this.GetCarCategories()
        });

        [HttpPost]
        public async Task<IActionResult> Add(CarFormModel car)
        {
            if (!this.data.Categories.Any(c => c.Id == car.CategoryId))
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
    }


}
