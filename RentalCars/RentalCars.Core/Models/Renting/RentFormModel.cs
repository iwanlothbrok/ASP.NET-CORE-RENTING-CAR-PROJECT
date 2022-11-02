namespace RentalCars.Core.Models.Renting
{
    using System.ComponentModel.DataAnnotations;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.User;

    public class RentFormModel
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(FullNameMaxLength, MinimumLength = FullNameMinLength)]

        public string CustomerFirstName { get; set; } = null!;

        [StringLength(FullNameMaxLength, MinimumLength = FullNameMinLength)]
        [Display(Name = "Last Name")]
        [Required]
        public string CustomerLastName { get; set; } = null!;

        [Display(Name = "Date of booking the car")]
        public string BookingDate { get; set; }

        [Display(Name = "Returning date")]
        public string ReturningDate { get; set; }

        [Display(Name = "Car")]
        public int CarId { get; init; }

        public IEnumerable<RentCarModel>? Cars { get; set; }


    }
}
