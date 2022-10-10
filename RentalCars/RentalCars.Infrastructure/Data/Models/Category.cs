namespace RentalCars.Infrastructure.Data.Models
{
using System.ComponentModel.DataAnnotations;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.CategoryConstants;

    public class Category
    {
        /// <summary>
        /// category id 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// name of the category 
        /// </summary>
        [Required]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; }

        /// <summary>
        /// relation of the tables 
        /// </summary>
        public IEnumerable<Car> Cars { get; init; } = new List<Car>();
    }
}
