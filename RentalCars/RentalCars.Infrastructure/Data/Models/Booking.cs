namespace RentalCars.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.Car;


    public class Booking
    {
        /// <summary>
        /// id of the booking
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// car brand 
        /// </summary>
        [Required]
        [Range(BrandMinLength, BrandMaxLength)]
        public string CarBrand { get; set; } = null!;

        /// <summary>
        /// car model 
        /// </summary>

        [Required]
        [Range(ModelMinLength, ModelMaxLength)]
        public string CarModel { get; set; } = null!;

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
        /// car id 
        /// </summary>
        public int CarId { get; set; }
        /// <summary>
        /// car id 
        /// </summary>
        //[ForeignKey(nameof(CarId))]
        //public Car Car { get; set; } = null!;

        /// <summary>
        /// dealer id 
        /// </summary>
        public int DealerId { get; set; }

        /// <summary>
        /// dealer id 
        /// </summary>
        //[ForeignKey(nameof(DealerId))]
        //public Dealer Dealer { get; set; } = null!;

        /// <summary>
        /// customer id 
        /// </summary>
        [Required]
        public string UserId { get; set; } = null!;

        /// <summary>
        /// status of the book
        /// </summary>
        [Required]
        public bool Status { get; set; } = false;
    }
}
