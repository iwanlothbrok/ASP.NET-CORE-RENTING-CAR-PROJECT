﻿namespace RentalCars.Services.Cars
{

    using RentalCars.Data;
    using RentalCars.Models.Cars;
    using RentalCars.Services.Cars.Models;

    public class CarService : ICarService
    {
        private readonly ApplicationDbContext data;

        public CarService(ApplicationDbContext data)
            => this.data = data;

        public CarQueryServiceModel All(
            string brand,
            string searchTerm,
            CarSorting sorting,
            int currentPage,
            int carsPerPage)
        {
            var carsQuery = this.data.Cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                carsQuery = carsQuery.Where(c => c.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                carsQuery = carsQuery.Where(c =>
                    (c.Brand + " " + c.Model).ToLower().Contains(searchTerm.ToLower()) ||
                    c.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            carsQuery = sorting switch
            {
                CarSorting.Year => carsQuery.OrderByDescending(c => c.Year),
                CarSorting.BrandAndModel => carsQuery.OrderBy(c => c.Brand).ThenBy(c => c.Model),
                CarSorting.DateCreated or _ => carsQuery.OrderByDescending(c => c.Id)
            };

            var totalCars = carsQuery.Count();

            var cars = carsQuery
                .Skip((currentPage - 1) * carsPerPage)
                .Take(carsPerPage)
                .Select(c => new CarServiceModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    ImageUrl = c.ImageUrl,
                    Category = c.Category.Name
                })
                .ToList();

            return new CarQueryServiceModel
            {
                TotalCars = totalCars,
                CurrentPage = currentPage,
                CarsPerPage = carsPerPage,
                Cars = cars
            };
        }

        public IEnumerable<string> AllBrands()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> AllCarBrands()
            => this.data
                .Cars
                .Select(c => c.Brand)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

        public IEnumerable<CarCategoryServiceModel> AllCategories()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CarServiceModel> ByUser(string userId)
        {
            throw new NotImplementedException();
        }

        public bool CategoryExists(int categoryId)
        {
            throw new NotImplementedException();
        }

        public int Create(string brand, string model, string description, string imageUrl, int year, int categoryId, int dealerId)
        {
            throw new NotImplementedException();
        }

        public CarDetailsServiceModel Details(int carId)
        {
            throw new NotImplementedException();
        }

        public bool IsByDealer(int carId, int dealerId)
        {
            throw new NotImplementedException();
        }
    }
}
