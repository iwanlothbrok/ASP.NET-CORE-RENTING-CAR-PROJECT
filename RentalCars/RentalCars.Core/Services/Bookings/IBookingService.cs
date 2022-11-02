namespace RentalCars.Core.Services.Bookings
{
    public interface IBookingService
    {
         int CreateBooking(string firstName, string lastName, string userId, int dealerId, string bookingDate, string returingDate, bool status, int carId);
        void ChangeVisility(int id);
    }
}
