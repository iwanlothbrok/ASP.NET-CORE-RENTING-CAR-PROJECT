namespace RentalCars.Core.Services.Bookings
{
    using RentalCars.Core.Models.Renting;
    using RentalCars.Infrastructure.Data.Models;
    public interface IBookingService
    {
        bool IsRented(int id, int carId);
        BookingQueryModel All(bool confirmByAdmin, bool confirmByDealer);
        IEnumerable<AdminBookingModel> GetBookings(IQueryable<Booking> booking);
        public int CreateBooking(string firstName,
           string lastName,
           string userId,
           int dealerId,
           string bookingDate,
           decimal price,
           string returingDate,
           int carId);

        int FindCar(int id);
        bool CheckIfIsDealer(int id);
        bool CheckUser(string id);
        void ChangeVisilityByDealer(int id);
        void ChangeVisilityByAdmin(int id);
        bool Delete(int id);
        bool DateChecker(string dateOfBooking, string dateOfReturning);
        bool UserHasBookedCar(string userId);
        decimal GetCarPrice(string bookingDate, string returningDate, decimal price);
    }
}
