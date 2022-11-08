namespace RentalCars.Core.Services.Bookings
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

        public bool CheckUser(string id)
            => data.Bookings.Any(c => c.CustomerId == id);


        public bool CheckIfIsDealer(int id)
            => data.Bookings.Any(c => c.DealerId == id);

        public int FindCar(int id)
        => data.Bookings.Where(c => c.Id == id).Select(c => c.CarId).FirstOrDefault();


        public int CreateBooking(string firstName,
            string lastName,
            string userId,
            int dealerId,
            string bookingDate,
            decimal price,
            string returingDate,
            int carId)
        {

            var allPrice = GetCarPrice(bookingDate, returingDate, price);

            if (allPrice <= 0)
            {
                return -1;
            }
            var booking = new Booking
            {
                CustomerFirstName = firstName,
                CustomerLastName = lastName,
                CustomerId = userId,
                DealerId = dealerId,
                DailyPrice = GetCarPrice(bookingDate, returingDate, price),
                BookingDate = bookingDate,
                ReturnDate = returingDate,
                IsConfirmedByAdmin = false,
                IsConfirmedByDealer = false,
                CarId = carId
            };

            data.Bookings.Add(booking);
            data.SaveChanges();

            return booking.Id;
        }
        public bool DateChecker(string dateOfBooking, string dateOfReturning)
        {
            char[] delimiterChars = { ' ', '-', '/', '\\', ',' };

            string[] bookingDate = dateOfBooking.Split(delimiterChars);
            string[] returningDate = dateOfReturning.Split(delimiterChars);


            var bookingMonth = int.Parse(bookingDate[1]);
            var returninMonth = int.Parse(returningDate[1]);
            var bookingDay = int.Parse(bookingDate[0]);
            var returningDay = int.Parse(returningDate[0]);
            var bookingYear = int.Parse(bookingDate[2]);
            var returningYear = int.Parse(returningDate[2]);

            if (bookingDay > 31 || returningDay > 31)
            {
                return false;
            }
            if (bookingMonth > 12 || returninMonth > 12)
            {
                return false;
            }
            if (bookingYear > 2023 && bookingYear < 2022 || returningYear > 2023 && returningYear < 2022)
            {
                return false;
            }

            return true;
        }

        public bool UserHasBookedCar(string userId)
        {
            var isValid = data
                .Bookings
                .Where(c => c.CustomerId == userId)
                .FirstOrDefault();



            if (isValid == null)
            {
                return false;
            }
            return true;
        }



        public decimal GetCarPrice(string bookingDate, string returningDate, decimal price)
        {

            TimeSpan diff = DateTime.Parse(returningDate) - DateTime.Parse(bookingDate);

            var days = diff.Days;
            if (days <= 0)
            {
                return 0;
            }


            price = price * days;

            
            return price;
        }

        public void ChangeVisilityByAdmin(int id)
        {
            var booking = this.data.Bookings.Find(id);


            booking.IsConfirmedByAdmin = true;

            this.data.SaveChanges();
        }

        public void ChangeVisilityByDealer(int id)
        {
            var booking = this.data.Bookings.Find(id);


            booking.IsConfirmedByDealer = true;

            this.data.SaveChanges();
        }

        public bool IsRented(int id, int carId)
        {
            var searchingCar = car.FindCar(carId);

            if (car == null)
            {
                return false;
            }

            var bookings = this.data.Bookings.Find(id);

            if (bookings == null)
            {
                return false;
            }

            if (bookings.IsConfirmedByDealer == true && bookings.IsConfirmedByAdmin == true)
            {

                searchingCar.IsPublic = false;
                searchingCar.BookingId = id;
                data.SaveChanges();
            }


            return true;
        }

        public BookingQueryModel All()
        {
            
            var bookingQuery = this.data.Bookings
                                .Where(p => p.IsConfirmedByAdmin == false || p.IsConfirmedByDealer == false);



            var bookings = GetBookings(bookingQuery);


            return new BookingQueryModel
            {
                Bookings = bookings
            };
        }
        public bool Delete(int id)
        {
            var bookingData = this.data.Bookings.Find(id);
            var isValid = true;

            if (bookingData == null)
            {
                isValid = false;

            }

            if (isValid == true)
            {
                this.data.Bookings.Remove(bookingData);
                this.data.SaveChanges();
            }


            return isValid;

        }

        public IEnumerable<AdminBookingModel> GetBookings(IQueryable<Booking> booking)
        => booking
            .ProjectTo<AdminBookingModel>(this.mapper.ConfigurationProvider)
            .ToList();
    }
}
