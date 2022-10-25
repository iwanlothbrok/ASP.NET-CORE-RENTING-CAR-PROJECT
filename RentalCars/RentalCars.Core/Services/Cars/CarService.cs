namespace RentalCars.Core.Services.Cars
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using RentalCars.Core.Models.Cars;
    using RentalCars.Core.Services.Cars.Models;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;

    public class CarService : ICarService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public CarService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public bool Delete(int id, int dealerId)
        {
            var carData = this.data.Cars.Find(id);
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
            var car = this.data.Cars.Find(carId);

            car.IsPublic = !car.IsPublic;

            this.data.SaveChanges();
        }

        public CarQueryServiceModel All(
           string brand = null,
           string searchTerm = null,
           CarSorting sorting = CarSorting.DateCreated,
           int currentPage = 1,
           int carsPerPage = int.MaxValue,
            bool publicOnly = true)
        {

            var carsQuery = this.data.Cars
                                .Where(p => p.IsPublic == publicOnly);

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

            var cars = GetCars(carsQuery
                .Skip((currentPage - 1) * carsPerPage)
                .Take(carsPerPage));


            return new CarQueryServiceModel
            {
                TotalCars = totalCars,
                CurrentPage = currentPage,
                CarsPerPage = carsPerPage,
                Cars = cars
            };
        }


        public int Create(string brand, string model, string description, string imageUrl, int year, int categoryId, int dealerId)
        {
            var carData = new Car
            {
                Brand = brand,
                Model = model,
                Description = description,
                ImageUrl = imageUrl,
                Year = year,
                CategoryId = categoryId,
                DealerId = dealerId,
                IsPublic = false
            };


            data.Cars.Add(carData);
            data.SaveChanges();

            return carData.Id;
        }

        public bool Edit(int id, string brand, string model, string description, string imageUrl, int year, int categoryId)
        {
            var carData = this.data.Cars.Find(id);

            if (carData == null)
            {
                return false;
            }

            carData.Brand = brand;
            carData.Model = model;
            carData.Description = description;
            carData.ImageUrl = imageUrl;
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
          => data
                .Categories
                .Select(c => new CarCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();


        public bool CategoryExists(int categoryId)
         => data
             .Categories
             .Any(c => c.Id == categoryId);

        public IEnumerable<CarServiceModel> GetCars(IQueryable<Car> carQuery)
           => carQuery
            .ProjectTo<CarServiceModel>(this.mapper.ConfigurationProvider)
            .ToList();
    }
}
