namespace RentalCars.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.DebitCard;

    public class FakeDebitCard
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public long CreditCardNumber { get; set; }

        [Required]
        public int CVV { get; set; }

        [Required]
        [StringLength(MaxFullNameLenght, MinimumLength = MinFullNameLenght)]
        public string FullName { get; set; } = null!;

        [Required]
        public string ExpMonth { get; set; } = null!;

        [Required]
        [Range(ExpYearMin, ExpYearMax)]
        public int ExpYear { get; set; }

        public List<Payment>? Payments { get; set; } = new List<Payment>();
    }
}
