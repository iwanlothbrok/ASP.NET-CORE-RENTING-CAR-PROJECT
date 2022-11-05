﻿namespace RentalCars.Core.Services.Bookings
{
    using RentalCars.Core.Models.Renting;
    using RentalCars.Infrastructure.Data.Models;

    public interface IBookingService
    {
        BookingQueryModel All();
        IEnumerable<AdminBookingModel> GetBookings(IQueryable<Booking> booking);
        int CreateBooking(string firstName, string lastName, string userId, int dealerId, string bookingDate, decimal price, string returingDate, bool status, int carId);
        void ChangeVisility(int id);

        decimal GetCarPrice(string bookingDate, string returningDatem, decimal price);
    }
}
