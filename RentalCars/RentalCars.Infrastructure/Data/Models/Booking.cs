namespace RentalCars.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Booking
    {
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
        /// car id 
        /// </summary>
        public int CarId { get; set; }

        
        /// <summary>
        /// dealer id 
        /// </summary>
        
        public int DealerId { get; set; }
       
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
