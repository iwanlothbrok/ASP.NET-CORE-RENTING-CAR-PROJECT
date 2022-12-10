namespace RentalCars.Core.Models.Home
{
    using RentalCars.Infrastructure.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.DebitCard;

    public class PaymentsModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Credit card number")]
        [StringLength(CardNumberLength, MinimumLength = CardNumberLength)]
        public int CreditCardNumber { get; set; }

        [Required]
        [StringLength(CVVLength, MinimumLength = CVVLength)]
        public int CVV { get; set; }

        [Required]
        [Display(Name = "Name on Card")]
        [StringLength(MaxFullNameLenght, MinimumLength = MinFullNameLenght)]
        public string FullName { get; set; } = null!;

        [Display(Name = "Exp Month")]
        [Required]
        public string ExpMonth { get; set; } = null!;

        [Required]
        [Display(Name = "Exp Year")]
        [Range(ExpYearMin, ExpYearMax)]
        public int ExpYear { get; set; }

        public int? CarId { get; set; }
        public Car? Car { get; set; }

        public int? BookingId { get; set; }
        public Booking? Book { get; set; }

        public Dealer? Dealer{ get; set; }
    }
}
