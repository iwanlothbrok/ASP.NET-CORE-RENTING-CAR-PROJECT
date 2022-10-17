namespace RentalCars.Core.Services.Cars
{
    using RentalCars.Core.Services.Cars.Models;
    using RentalCars.Infrastructure.Data.Models;
    using RentalCars.Core.Models.Cars;
    public interface ICarService
    {
        CarQueryServiceModel All(
           string brand,
           string searchTerm,
           CarSorting sorting,
           int currentPage,
           int carsPerPage);

        public bool Edit(int id,
            string brand,
            string model,
            string description,
            string imageUrl,
            int year,
            int categoryId);

        int Create(
          string brand,
          string model,
          string description,
          string imageUrl,
          int year,
          int categoryId,
          int dealerId);
        CarDetailsServiceModel Details(int carId);

        public IEnumerable<CarServiceModel> GetCars(IQueryable<Car> carQuery);

        IEnumerable<CarServiceModel> ByUser(string userId);

        bool IsByDealer(int carId, int dealerId);

        IEnumerable<string> AllBrands();

        IEnumerable<CarCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);
    }
}
