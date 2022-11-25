namespace RentalCars.Core.Services.Cars
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
    using RentalCars.Core.Models.Cars;
    using RentalCars.Core.Models.Renting;
    using RentalCars.Core.Services.Cars.Models;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class CarService : ICarService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public CarService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }
        public Car? FindCar(int id)
        => this.data.Cars.Find(id);

        public IEnumerable<CarServiceModel> GetLastThreeCars()
         => this.data
            .Cars
            .OrderByDescending(i => i.Id)
            .Where(p => p.IsPublic == true && p.IsBooked == false)
            .Select(m => new CarServiceModel
            {
                Id = m.Id,
                Brand = m.Brand,
                CarPhoto = m.CarPhoto,
                Model = m.Model,
                Year = m.Year
            }).Take(3)
              .ToList();

        public bool Delete(int id, int dealerId)
        {
            Car? carData = this.data.Cars.Find(id);

            bool isMyCar = true;

            if (carData == null)
            {
                isMyCar = false;
            }

            if (dealerId == 0)
            {
                isMyCar = false;
            }

            if (IsByDealer(carData.Id, dealerId) == false)
            {
                isMyCar = false;
            }

            if (isMyCar)
            {
                this.data.Cars.Remove(carData);
                this.data.SaveChanges();
            }

            return isMyCar;
        }

        public void ChangeVisility(int carId)
        {
            Car? car = this.data.Cars.Find(carId);

            if (car != null)
            {
                car.IsPublic = !car.IsPublic;
                this.data.SaveChanges();
            }
        }

        public CarQueryServiceModel All(
           string brand = null,
           string searchTerm = null,
           CarSorting sorting = CarSorting.DateCreated,
           int currentPage = 1,
           int carsPerPage = int.MaxValue,
            bool publicOnly = true,
            bool isBooked = false)
        {
            var carsQuery = this.data.Cars
                                .Where(p => p.IsPublic == publicOnly && p.IsBooked == isBooked);

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

            int totalCars = carsQuery.Count();

            IEnumerable<CarServiceModel> cars = GetCars(carsQuery
                .Skip((currentPage - 1) * carsPerPage)
                .Take(carsPerPage))
                .ToList();

            return new CarQueryServiceModel
            {
                TotalCars = totalCars,
                CurrentPage = currentPage,
                CarsPerPage = carsPerPage,
                Cars = cars
            };
        }

        public async Task<int> Create(string brand, string model, string description, decimal price, List<IFormFile> carPhoto, int year, int categoryId, int dealerId)
        {
            byte[] photo = new byte[8000];
            foreach (var item in carPhoto)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        photo = stream.ToArray();
                    }

                }
            }
            var carData = new Car
            {
                Brand = brand,
                Model = model,
                Description = description,
                Price = price,
                CarPhoto = photo,
                Year = year,
                CategoryId = categoryId,
                DealerId = dealerId,
                IsPublic = false
            };

            await this.data.Cars.AddAsync(carData);
            await this.data.SaveChangesAsync();

            return carData.Id;
        }

        public async Task<bool> Edit(int id, string brand, string model, decimal price, string description, List<IFormFile> carPhoto, int year, int categoryId)
        {
            Car? carData = this.data.Cars.Find(id);

            if (carData == null)
            {
                return false;
            }

            byte[] photo = new byte[8000];
            foreach (var item in carPhoto)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        photo = stream.ToArray();
                    }

                }
            }

            carData.Brand = brand;
            carData.Model = model;
            carData.Description = description;
            carData.Price = price;
            carData.CarPhoto = photo;
            carData.Year = year;
            carData.CategoryId = categoryId;
            carData.IsPublic = false;

            this.data.SaveChanges();

            return true;
        }

        public CarDetailsServiceModel? Details(int id)
            => this.data
            .Cars
            .Where(c => c.Id == id)
            .ProjectTo<CarDetailsServiceModel>(this.mapper.ConfigurationProvider)
            .FirstOrDefault();

        public IEnumerable<string> AllBrands()
        => this.data
                .Cars
                .Select(c => c.Brand)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

        public IEnumerable<CarServiceModel> ByUser(string userId)
             => GetCars(this.data
                 .Cars
                 .Where(c => c.Dealer.UserId == userId));

        public bool IsByDealer(int carId, int dealerId)
            => this.data
                .Cars
                .Any(c => c.Id == carId && c.DealerId == dealerId);

        public IEnumerable<CarCategoryServiceModel> AllCategories()
          => this.data
                .Categories
                .Select(c => new CarCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        public IEnumerable<RentCarModel> AllCars()
         => this.data
               .Cars
               .Select(c => new RentCarModel
               {
                   Id = c.Id,
                   Brand = c.Brand,
                   Model = c.Model,
                   Year = c.Year,
                   Price = c.Price,
                   IsBooked = c.IsBooked,
                   IsPublic = c.IsPublic,
               })
               .ToList();

        public bool CarsExists(int carId)
         => this.data
             .Cars
             .Any(c => c.Id == carId);

        public bool CategoryExists(int categoryId)
         => this.data
             .Categories
             .Any(c => c.Id == categoryId);

        public IEnumerable<CarServiceModel> GetCars(IQueryable<Car> carQuery)
              => carQuery
                  .ProjectTo<CarServiceModel>(this.mapper.ConfigurationProvider)
                  .ToList();


    }
}
