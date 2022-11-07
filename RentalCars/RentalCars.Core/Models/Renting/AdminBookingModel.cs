namespace RentalCars.Core.Models.Renting
{
    public class AdminBookingModel
    {
        public string CustomerFirstName { get; set; } = null!;

        public string CustomerLastName { get; set; } = null!;
       
        public int Id { get; set; }

        public string BookingDate { get; set; }
    
        public string ReturnDate { get; set; }

        public int CarId { get; set; }

        public int DealerId { get; set; }
        public decimal Price{ get; set; }
      
        public string CustomerId { get; set; } = null!;
        public bool Status { get; set; } = true;
    }
}
