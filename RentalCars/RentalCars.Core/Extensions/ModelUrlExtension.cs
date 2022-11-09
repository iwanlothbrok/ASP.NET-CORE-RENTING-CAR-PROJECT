namespace RentalCars.Core.Extensions
{
    using RentalCars.Core.Services.Cars.Models;
    public static class ModelUrlExtension
    {
        public static string GetInformationUrl(this CarServiceModel car)
        => car.Brand + " " + car.Model + " " + car.Year;
    }
}

