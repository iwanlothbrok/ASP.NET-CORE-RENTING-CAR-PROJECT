namespace RentalCars.Core.Models.Bookings
{
    using System.ComponentModel.DataAnnotations;

    public class BookingFormModel 
    {
        public int CarId { get; set; }
        public string Brand { get; init; }

        public string Model { get; init; }

        public int DealerId { get; init; }

        public string ImageUrl { get; init; }

        public int Year { get; init; }
        public decimal  Price{ get; init; }


        [Required]
        [Display(Name = "Booking date")]
        public DateTime BookingDate { get; set; }

        [Required]
        [Display(Name = "Date of returning the car")]
        public DateTime ReturnDate { get; set; }
    }
}