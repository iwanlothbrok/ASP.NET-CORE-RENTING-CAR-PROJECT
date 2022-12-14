namespace RentalCars.Core.Services.Bookings
{
    using RentalCars.Core.Models.Renting;
    using RentalCars.Infrastructure.Data.Models;
    public interface IBookingService
    {
        Booking? GetBookByUserId(string userId);
        bool IsRented(int id, int carId);
        BookingQueryModel All(bool confirmByAdmin, bool confirmByDealer);
        IEnumerable<AdminBookingModel> GetBookings(IQueryable<Booking> booking);
        public int CreateBooking(string firstName,
           string lastName,
           string phoneNumber,
           string userId,
           int dealerId,
           string bookingDate,
           decimal price,
           string returingDate,
           int carId);

        void ReturningDateChecker(IEnumerable<Booking> bookings);
        int FindCarBookingId(int id);
        void ChangeVisilityByDealer(int id);
        void ChangeVisilityByAdmin(int id);
        bool Delete(int id);
        bool DateChecker(string dateOfBooking, string dateOfReturning);
        bool UserHasBookedCar(string userId);
        decimal GetCarPrice(string bookingDate, string returningDate, decimal price);
        IEnumerable<Booking> GetBookingsAll();
    }
}
