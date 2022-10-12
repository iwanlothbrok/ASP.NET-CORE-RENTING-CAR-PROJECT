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

        IEnumerable<string> AllCarBrands();
    }
}
