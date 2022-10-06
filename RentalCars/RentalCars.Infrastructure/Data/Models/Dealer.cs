
//namespace RentalCars.Infrastructure.Data.Models
//{
//using System.ComponentModel.DataAnnotations;
//    using static RentalCars.Infrastructure.Data.Constants.DealerConstants;
//    public class Dealer
//    {
//        /// <summary>
//        /// dealer id 
//        /// </summary>
//        public Guid Id { get; set; }

//        /// <summary>
//        /// first name of the dealer
//        /// </summary>
//        [Required]
//        [MaxLength(NameMaxLenght)]
//        public string FirstName { get; set; }

//        /// <summary>
//        /// phone number of the dealer 
//        /// </summary>
//        [Required]
//        [MaxLength(PhoneNumberMaxLength)]
//        public string PhoneNumber { get; set; }

//        /// <summary>
//        /// 
//        /// </summary>
//        [Required]
//        public Guid UserId { get; set; }

//        public IEnumerable<Car> Cars { get; init; } = new List<Car>();
//    }
//}
