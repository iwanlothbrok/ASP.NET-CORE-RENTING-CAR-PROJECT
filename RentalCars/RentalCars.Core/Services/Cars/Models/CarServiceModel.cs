namespace RentalCars.Core.Services.Cars.Models
{
    public class CarServiceModel
    {
        public int Id { get; init; }

        public string Brand { get; init; }

        public string Model { get; init; }

        public string ImageUrl { get; init; }

        public decimal Price { get; set; }

        public int Year { get; init; }

        public bool IsPublic { get; init; }

        public string CategoryName { get; init; }
    }
}
