namespace RentalCars.Core.Services.Cars.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CarServiceModel
    {
        public int Id { get; init; }

        public string Brand { get; init; }

        public string Model { get; init; }

        public byte[] CarPhoto { get; set; }

        public decimal Price { get; set; }

        public int Year { get; init; }

        public bool IsPublic { get; init; }

        public bool IsBooked { get; init; }

        public string CategoryName { get; init; }

        [Display(Name = "Country")]
        public string Country { get; init; } = null!;

        [Display(Name = "City")]
        public string City { get; init; } = null!;
    }
}
