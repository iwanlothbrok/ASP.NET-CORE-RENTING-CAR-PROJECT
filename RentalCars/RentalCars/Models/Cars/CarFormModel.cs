namespace RentalCars.Models.Cars
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using RentalCars.Services.Cars.Models;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.CarConstants;

    public class CarFormModel
    {
        [Required]
        [StringLength(
            BrandMaxLength, 
            MinimumLength = BrandMinLength,
            ErrorMessage = "The field Brand must be minumum {0} length and maximum {1} length! ")]
        public string Brand { get; set; }

        [Required]
        [StringLength(ModelMaxLength, MinimumLength = ModelMinLength)]
        public string Model { get; init; }

        [Required]
        [StringLength(
            int.MaxValue,
            MinimumLength = DescriptionMinLength,
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; init; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        [Range(YearMinValue, YearMaxValue)]
        public int Year { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<CarCategoryServiceModel>? Categories { get; set; }
    }
}