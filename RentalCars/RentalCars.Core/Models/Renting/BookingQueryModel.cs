namespace RentalCars.Core.Models.Renting
{
    public class BookingQueryModel
    {
        public IEnumerable<AdminBookingModel> Bookings { get; set; } = new List<AdminBookingModel>();
    }
}
