namespace RentalCars.Services.Cars
{

    using RentalCars.Models.Cars;
    using RentalCars.Services.Cars.Models;

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

        IEnumerable<CarServiceModel> ByUser(string userId);

        bool IsByDealer(int carId, int dealerId);

        IEnumerable<string> AllBrands();

        IEnumerable<CarCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);
    }
}
