namespace RentalCars.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.DebitCard;

    public class DebitCard
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(CardNumberLength, MinimumLength = CardNumberLength)]
        public int CreditCarNumber { get; set; }

        [Required]
        [StringLength(CVVLength, MinimumLength = CVVLength)]
        public int CVV { get; set; }

        [Required]
        [StringLength(MaxFullNameLenght, MinimumLength = MinFullNameLenght)]
        public string FullName { get; set; } = null!;

        [Required]
        public string ExpMonth { get; set; } = null!;

        [Required]
        [Range(ExpYearMin, ExpYearMax)]
        public int ExpYear { get; set; }
    }
}
