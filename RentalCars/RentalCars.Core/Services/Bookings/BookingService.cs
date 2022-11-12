namespace RentalCars.Core.Services.Bookings
{
    using System;
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

        public bool CheckUser(string id)
            => this.data.Bookings.Any(c => c.CustomerId == id);

        public bool CheckIfIsDealer(int id)
            => this.data.Bookings.Any(c => c.DealerId == id);

        public int FindCar(int id)
        => this.data.Bookings.Where(c => c.Id == id).Select(c => c.CarId).FirstOrDefault();

        public int CreateBooking(string firstName,
            string lastName,
            string userId,
            int dealerId,
            string bookingDate,
            decimal price,
            string returingDate,
            int carId)
        {
            decimal allPrice = GetCarPrice(bookingDate, returingDate, price);

            if (allPrice <= 0)
            {
                return -1;
            }
            Booking booking = new Booking
            {
                CustomerFirstName = firstName,
                CustomerLastName = lastName,
                CustomerId = userId,
                DealerId = dealerId,
                DailyPrice = GetCarPrice(bookingDate, returingDate, price),
                BookingDate = DateTime.Parse(bookingDate),
                ReturnDate = DateTime.Parse(returingDate),
                IsConfirmedByAdmin = false,
                IsConfirmedByDealer = false,
                CarId = carId
            };

            this.data.Bookings.Add(booking);
            this.data.SaveChanges();

            return booking.Id;
        }
        public bool DateChecker(string dateOfBooking, string dateOfReturning)
        {
            char[] delimiterChars = { ' ', '-', '/', '\\', ',' };

            string[] bookingDate = dateOfBooking.Split(delimiterChars);
            string[] returningDate = dateOfReturning.Split(delimiterChars);


            int bookingMonth = int.Parse(bookingDate[1]);
            int returninMonth = int.Parse(returningDate[1]);
            int bookingDay = int.Parse(bookingDate[0]);
            int returningDay = int.Parse(returningDate[0]);
            int bookingYear = int.Parse(bookingDate[2]);
            int returningYear = int.Parse(returningDate[2]);

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
            Booking? isValid = this.data
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
            //TimeSpan diff = returningDate - bookingDate;
            int days = diff.Days;

            if (days <= 0)
            {
                return 0;
            }

            decimal totalPrice = price * days;

            return totalPrice;
        }

        public void ChangeVisilityByAdmin(int id)
        {
            Booking? booking = this.data.Bookings.Where(c=>c.Id == id).FirstOrDefault();

            if (booking != null)
            {
                booking.IsConfirmedByAdmin = true;
                this.data.SaveChanges();
            }
        }

        public void ChangeVisilityByDealer(int id)
        {
            Booking? booking = this.data.Bookings.Find(id);

            if (booking != null)
            {
                booking.IsConfirmedByDealer = true;
                this.data.SaveChanges();
            }
        }

        public bool IsRented(int id, int carId)
        {
            Car searchingCar = this.car.FindCar(carId);

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
                searchingCar.IsBooked = true;
                searchingCar.BookingId = id;

                this.data.SaveChanges();
            }

            return true;
        }

        public BookingQueryModel All()
        {
            var bookingQuery = this.data.Bookings
                                .Where(p => p.IsConfirmedByAdmin == false || p.IsConfirmedByDealer == false);

            IEnumerable<AdminBookingModel> bookings = GetBookings(bookingQuery)
                                                                 .ToList();

            return new BookingQueryModel
            {
                Bookings = bookings
            };
        }
        public bool Delete(int id)
        {
            Booking? bookingData = this.data.Bookings.Find(id);

            bool isValid = true;

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
        =>   booking
            .ProjectTo<AdminBookingModel>(this.mapper.ConfigurationProvider)
            .ToList();
    }
}
