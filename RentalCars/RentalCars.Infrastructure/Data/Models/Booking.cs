namespace RentalCars.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Booking
    {
        [Required]
        public string CustomerFirstName { get; set; } = null!;

        [Required]
        public string CustomerLastName { get; set; } = null!;
        /// <summary>
        /// id of the booking
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// date of the renting 
        /// </summary>
        [Required]
        public string BookingDate { get; set; }

        /// <summary>
        /// date of returning the car 
        /// </summary>
        [Required]
        public string ReturnDate { get; set; }

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
        public string CustomerId { get; set; } = null!;

        /// <summary>
        /// status of the book
        /// </summary>
        [Required]
        public bool Status { get; set; } = false;
    }
}
