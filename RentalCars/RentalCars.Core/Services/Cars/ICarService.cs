namespace RentalCars.Core.Services.Cars
{
    using Microsoft.AspNetCore.Http;
    using RentalCars.Core.Models.Cars;
    using RentalCars.Core.Models.Renting;
    using RentalCars.Core.Services.Cars.Models;
    using RentalCars.Infrastructure.Data.Models;

    public interface ICarService
    {
        bool Delete(int id, int dealerId);

        CarQueryServiceModel All(
           string brand = null,
           string searchTerm = null,
           CarSorting sorting = CarSorting.DateCreated,
           int currentPage = 1,
           int carsPerPage = int.MaxValue,
            bool publicOnly = true);

        IEnumerable<CarServiceModel> GetLastThreeCars();

        public Car FindCar(int id);

        public void ChangeVisility(int carId);

        Task<bool> Edit(int id,
            string brand,
            string model,
            decimal price,
            string description,
            List<IFormFile> carPhoto,
            int year,
            int categoryId);

        Task<int> Create(string brand,
          string model,
          string description,
          decimal price,
          List<IFormFile> carPhoto,
          int year,
          int categoryId,
          int dealerId);
        CarDetailsServiceModel Details(int carId);

        IEnumerable<CarServiceModel> GetCars(IQueryable<Car> carQuery);
        IEnumerable<CarServiceModel> ByUser(string userId);

        bool IsByDealer(int carId, int dealerId);

        IEnumerable<string> AllBrands();

        IEnumerable<CarCategoryServiceModel> AllCategories();

        bool CarsExists(int carId);

        bool CategoryExists(int categoryId);

        IEnumerable<RentCarModel> AllCars();
    }
}
