namespace RentalCars.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Booking
    {
        /// <summary>
        /// first name of the user
        /// </summary>
        [Required]
        public string CustomerFirstName { get; set; } = null!;

        /// <summary>
        /// last name of the user
        /// </summary>
        [Required]
        public string CustomerLastName { get; set; } = null!;

        [Required]
        [Phone]
        public string CustomerPhoneNumber { get; set; } = null!;

        /// <summary>
        /// id of the booking
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// date of the renting 
        /// </summary>
        [Required]
        public DateTime BookingDate { get; set; }

        /// <summary>
        /// date of returning the car 
        /// </summary>
        [Required]
        public DateTime ReturnDate { get; set; }

        /// <summary>
        /// price for 1 day
        /// </summary>
        [Range(0, int.MaxValue)]
        public decimal DailyPrice { get; set; }

        /// <summary>
        /// car id 
        /// </summary>
        public int? CarId { get; set; }

        /// <summary>
        /// dealer id 
        /// </summary>
        public int? DealerId { get; set; }

        /// <summary>
        /// customer id 
        /// </summary>
        [Required]
        public string CustomerId { get; set; } = null!;

        /// <summary>
        ///is it confirmed by admin
        /// </summary>
        public bool IsConfirmedByAdmin { get; set; } = false;

        /// <summary>
        ///is it confirmed by dealer
        /// </summary>
        public bool IsConfirmedByDealer { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool IsPaid { get; set; } = false;
    }
}
