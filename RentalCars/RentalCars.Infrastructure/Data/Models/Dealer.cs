namespace RentalCars.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.DealerConstants;
    public class Dealer
    {
        /// <summary>
        /// dealer id 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// first name of the dealer
        /// </summary>
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        /// <summary>
        /// phone number of the dealer 
        /// </summary>
        [Required]
        [StringLength(PhoneNumberMaxLength,MinimumLength = PhoneNumberMinLength)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// id of the user that is the dealer 
        /// </summary>
        [Required]
        public string UserId { get; set; }

        public IEnumerable<Car> Cars { get; init; } = new List<Car>();
    }
}
