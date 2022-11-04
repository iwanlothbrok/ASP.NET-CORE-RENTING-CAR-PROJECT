namespace RentalCars.Core.Services.Bookings
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using RentalCars.Core.Models.Renting;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;

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


        public BookingQueryModel All()
        {

            var bookingQuery = this.data.Bookings
                                .Where(p => p.Status == true);



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
