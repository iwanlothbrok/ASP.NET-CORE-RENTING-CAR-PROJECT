namespace RentalCars.Core.Models.Renting
{
    using System.ComponentModel.DataAnnotations;

    public class RentFormModel
    {
        [Required]
        public string CustomerFirstName { get; set; } = null!;

        [Required]
        public string CustomerLastName { get; set; } = null!;

        public DateTime BookingDate { get; set; }

        public DateTime ReturningDate { get; set; }

        [Display(Name = "Car")]
        public int CarId { get; init; }

        public IEnumerable<RentCarModel>? Cars { get; set; }


    }
}
