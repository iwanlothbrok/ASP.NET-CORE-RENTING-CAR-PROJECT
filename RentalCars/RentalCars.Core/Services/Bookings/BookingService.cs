﻿namespace RentalCars.Core.Services.Bookings
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using RentalCars.Core.Models.Renting;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;
    using System;

    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;
        private readonly ICarService car;

        public BookingService(ApplicationDbContext data, IMapper mapper, ICarService car)
        {
            this.data = data;
            this.mapper = mapper;
            this.car = car;
        }

        public int CreateBooking(string firstName, string lastName, string userId, int dealerId, string bookingDate,decimal price, string returingDate, bool status, int carId)
        {
            var booking = new Booking
            {
                CustomerFirstName = firstName,
                CustomerLastName = lastName,
                CustomerId = userId,
                DealerId = dealerId,
                DailyPrice = GetCarPrice(bookingDate,returingDate,price),
                BookingDate = bookingDate,
                ReturnDate = returingDate,
                IsConfirmed = status,
                CarId = carId
            };

            data.Bookings.Add(booking);
            data.SaveChanges();

            return booking.Id;
        }

        public decimal GetCarPrice(string bookingDate, string returningDate, decimal price)
        {

            TimeSpan diff = DateTime.Parse(returningDate) - DateTime.Parse(bookingDate);

            var days = diff.Days;

            price = price * days;

            return price;
        }

        public void ChangeVisility(int id)
        {
            var booking = this.data.Bookings.Find(id);
          

            booking.IsConfirmed = true;

            this.data.SaveChanges();
        }



        public BookingQueryModel All()
        {

            var bookingQuery = this.data.Bookings
                                .Where(p => p.IsConfirmed == false);



            var bookings = GetBookings(bookingQuery);


            return new BookingQueryModel
            {
                Bookings = bookings
            };
        }


        public IEnumerable<AdminBookingModel> GetBookings(IQueryable<Booking> booking)
        => booking
            .ProjectTo<AdminBookingModel>(this.mapper.ConfigurationProvider)
            .ToList();
    }
}
