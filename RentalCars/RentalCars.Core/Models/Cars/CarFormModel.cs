﻿namespace RentalCars.Core.Models.Cars
{
    using RentalCars.Core.Services.Cars.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
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
        public byte[]? CarPhoto { get; set; } = null!;

        [Required]
        [Display(Name = "Daily price")]
        [Range(PriceMinValue, PriceMaxValue)]
        public decimal Price { get; init; }

        [Display(Name = "Year of the car")]
        [Range(YearMinValue, YearMaxValue)]
        public int Year { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        [Required]

        [Display(Name = "Country")]
        public string Country { get; init; } = null!;

        [Required]
        [Display(Name = "City")]
        public string City { get; init; } = null!;

        public IEnumerable<CarCategoryServiceModel>? Categories { get; set; }
    }
}