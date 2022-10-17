using System.ComponentModel.DataAnnotations;

namespace RentalCars.Core.Models.Dealers
{
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.Dealer;
    public class BecomeDealerFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
