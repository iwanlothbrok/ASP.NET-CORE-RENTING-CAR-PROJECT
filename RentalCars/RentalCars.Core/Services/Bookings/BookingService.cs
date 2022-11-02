namespace RentalCars.Core.Services.Bookings
{
    using AutoMapper;
    using RentalCars.Core.Models.Cars;
    using RentalCars.Core.Services.Cars.Models;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;

    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public BookingService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public int CreateBooking(string firstName, string lastName, string userId, int dealerId, string bookingDate, string returingDate, bool status, int carId)
        {
            var booking = new Booking
            {
                CustomerFirstName = firstName,
                CustomerLastName = lastName,
                CustomerId = userId,
                DealerId = dealerId,
                BookingDate = bookingDate,
                ReturnDate = returingDate,
                Status = status,
                CarId = carId
            };

            data.Bookings.Add(booking);
            data.SaveChanges();

            return booking.Id;
        }


        public void ChangeVisility(int id)
        {
            var booking = this.data.Bookings.Find(id);

            booking.Status = false;

            this.data.SaveChanges();
        }

        
    }
}
