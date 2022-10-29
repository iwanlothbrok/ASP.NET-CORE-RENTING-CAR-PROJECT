namespace RentalCars.Core.Models.Bookings
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class BookingFormModel
    {

        [Required]
        [Display(Name = "Booking date")]
        public DateTime BookingDate { get; set; }

        [Required]
        [Display(Name = "Date of returning the car")]
        public DateTime ReturnDate { get; set; }
    }
}