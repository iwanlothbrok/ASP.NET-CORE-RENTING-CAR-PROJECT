namespace RentalCars.Core.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using RentalCars.Core.Services.Cars.Models;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.Car;

    public class CarFormModel
    {
        [Required]
        [StringLength(
            BrandMaxLength,
            MinimumLength = BrandMinLength,
            ErrorMessage = "The field Brand must be minumum {0} length and maximum {1} length! ")]
        public string Brand { get; set; } = null!;

        [Required]
        [StringLength(ModelMaxLength, MinimumLength = ModelMinLength)]
        public string Model { get; init; } = null!;

        [Required]
        [StringLength(
            int.MaxValue,
            MinimumLength = DescriptionMinLength,
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; init; } = null!;

        [Display(Name = "Photo of the car")]
        public byte[] CarPhoto { get; set; } = null!;

        [Required]
        [Display(Name = "Daily price")]
        [Range(PriceMinValue, PriceMaxValue)]
        public decimal Price { get; init; }

        [Range(YearMinValue, YearMaxValue)]
        public int Year { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<CarCategoryServiceModel>? Categories { get; set; }
    }
}