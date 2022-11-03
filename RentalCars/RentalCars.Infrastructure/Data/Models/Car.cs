namespace RentalCars.Infrastructure.Data.Models
{

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.Car;

    public class Car
    {
        /// <summary>
        /// car id 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// car brand 
        /// </summary>
        [Required]
        [Range(BrandMinLength, BrandMaxLength)]
        public string Brand { get; set; }

        /// <summary>
        /// car model 
        /// </summary>
        [Required]
        [Range(ModelMinLength, ModelMaxLength)]
        public string Model { get; set; }

        /// <summary>
        /// desctription of the car  
        /// </summary>
        [Required]
        [MinLength(DescriptionMinLength)]
        public string Description { get; set; }

        /// <summary>
        /// photo of the car 
        /// </summary>
        [Required]
        public string ImageUrl { get; set; }

        /// <summary>
        /// when the car is made
        /// </summary>
        [Range(YearMinValue, YearMaxValue)]
        public int Year { get; set; }

        /// <summary>
        /// if the car is public
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// if the car is booked
        /// </summary>
        public bool IsBooked { get; set; } = false;

        /// <summary>
        /// price for 1 day for the car 
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// foreign key 
        /// </summary>  
        public int CategoryId { get; set; }

        /// <summary>
        /// foreign key
        /// </summary> 
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        /// <summary>
        /// dealer of the car 
        /// </summary>  
        public int DealerId { get; set; }

        /// <summary>
        /// dealer of the car 
        /// </summary>  
        [ForeignKey("DealerId")]
        public Dealer Dealer { get; set; }
    }
}
