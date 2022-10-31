namespace RentalCars.Core.Models.Bookings
{
    using System.ComponentModel.DataAnnotations;

    public class BookingFormModel 
    {
        public string ImageUrl { get; set; }
        public decimal Price{ get; set; }

        [Display(Name = "Driver License")]
        [Required]
        public int DriverLicense { get; set; }

        [Display(Name = "Booking date")]
        [Required]
        public DateTime BookingDate { get; set; }


        [Display(Name = "Date of returning the car")]
        [Required]
        public DateTime ReturnDate { get; set; }
    }
}