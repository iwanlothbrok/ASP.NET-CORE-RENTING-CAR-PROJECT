namespace RentalCars.Test.Services
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Services.Bookings;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;
    using RentalCars.Infrastructure.Repositories.DatabaseRepositories;

    public class BookingServiceTests
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;
        private IMapper mapper;
        private ApplicationDbContext rentalCarsDb;

        [SetUp]
        public void Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IApplicatioDbRepository, ApplicatioDbRepository>()
                .AddSingleton<IdentityDbContext, ApplicationDbContext>()
                .AddSingleton<ICarService, CarService>()
                .BuildServiceProvider();

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            mapper = new Mapper(configuration);

            rentalCarsDb = serviceProvider.GetService<ApplicationDbContext>()!;

            SeedDatabase();
        }

        [Test]
        public void FindCarByBookingIdShouldReturnZero()
        {
            //Arrange
            int bookingId = 111;

            //Act
            var service = new CarService(rentalCarsDb, mapper);


            var bookingService = new BookingService(rentalCarsDb, mapper, service);

            //Assert
            Assert.That(bookingService.FindCarBookingId(bookingId), Is.Zero);
        }

        [Test]
        public void FindCarByBookingIdShouldReturnCorrectCarId()
        {
            //Arrange
            int bookingId = 1;

            //Act
            var service = new CarService(rentalCarsDb, mapper);


            var bookingService = new BookingService(rentalCarsDb, mapper, service);

            //Assert
            Assert.That(bookingService.FindCarBookingId(bookingId), Is.EqualTo(1));
        }

        [Test]
        public void GetAllBookingShouldReturnListOfBookings()
        {
            //Arrange
            var expectingType = new List<Booking>();
            var expectingCount = 1;
            //Act
            var service = new CarService(rentalCarsDb, mapper);
            var bookingService = new BookingService(rentalCarsDb, mapper, service);

            //Assert
            Assert.That(bookingService.GetBookingsAll(), Is.TypeOf(expectingType.GetType()));
            Assert.That(bookingService.GetBookingsAll().Count(), Is.EqualTo(expectingCount));
        }

        [Test]
        public void GetAllBookingShouldReturnEpmtyCollection()
        {
            //Arrange
            var expectingCount = 0;

            //Act
            var service = new CarService(rentalCarsDb, mapper);
            var bookingService = new BookingService(rentalCarsDb, mapper, service);

            //Assert
            Assert.That(bookingService.GetBookingsAll().Where(c => c.IsConfirmedByDealer == true
            && c.IsConfirmedByAdmin == true), Is.Empty);

            Assert.That(bookingService.GetBookingsAll().Where(c => c.IsConfirmedByDealer == true
            && c.IsConfirmedByAdmin == true).Count(), Is.EqualTo(expectingCount));
        }

        [Test]
        public void CreateBookingShouldHaveProblemWithThePrice()
        {
            //Arrange
            var userId = "asdas";

            //Act
            var service = new CarService(rentalCarsDb, mapper);
            var bookingService = new BookingService(rentalCarsDb, mapper, service);

            //Assert
            Assert.That(bookingService.CreateBooking("iwo", "iw", userId, 1, "20/1/2022", 0, "20/1/2022", 1), Is.EqualTo(-1));
        }

        [Test]
        public void DateCheckerShouldReturnFalse()
        {
            //Arrange
            var bookingDate = "20/1/2022";
            var returningDate = "10/1/2022";
            //Act
            var service = new CarService(rentalCarsDb, mapper);
            var bookingService = new BookingService(rentalCarsDb, mapper, service);

            //Assert
            Assert.That(bookingService.DateChecker(bookingDate, returningDate), Is.EqualTo(false));
        }

        [Test]
        public void DateCheckerShouldReturnTrue()
        {
            //Arrange
            var bookingDate = "2022/6/5";
            var returningDate = "2022/6/5";
            //Act
            var service = new CarService(rentalCarsDb, mapper);
            var bookingService = new BookingService(rentalCarsDb, mapper, service);

            //Assert
            Assert.That(bookingService.DateChecker(bookingDate, returningDate), Is.EqualTo(true));
        }

        [Test]
        public void UserHasBookedCarShouldReturnFalse()
        {
            //Arrange
            var falseId = "asda";

            //Act
            var service = new CarService(rentalCarsDb, mapper);
            var bookingService = new BookingService(rentalCarsDb, mapper, service);

            //Assert
            Assert.That(bookingService.UserHasBookedCar(falseId), Is.EqualTo(false));
        }
        [Test]
        public void UserHasBookedCarShouldReturnTrue()
        {
            //Arrange
            var id = "aeab5bc9-86c6-49ef-8f4f-faf8667d78b7";

            //Act
            var service = new CarService(rentalCarsDb, mapper);
            var bookingService = new BookingService(rentalCarsDb, mapper, service);

            //Assert
            Assert.That(bookingService.UserHasBookedCar(id), Is.EqualTo(true));
        }

        [Test]
        public void GetCarPriceShouldReturnZero()
        {
            //Arrange
            var bookingDate = "20/1/2022";
            var returningDate = "11/1/2022";
            var price = 100;

            //Act
            var service = new CarService(rentalCarsDb, mapper);
            var bookingService = new BookingService(rentalCarsDb, mapper, service);

            //Assert
            Assert.That(bookingService.GetCarPrice(bookingDate, returningDate, price), Is.EqualTo(0));
        }

        [Test]
        public void GetCarPriceShouldReturnCorrectPrice()
        {
            //Arrange
            var bookingDate = "21/5/2022";
            var returningDate = "22/5/2022";
            var price = 100;

            var expectedPrice = 200;

            //Act
            var service = new CarService(rentalCarsDb, mapper);
            var bookingService = new BookingService(rentalCarsDb, mapper, service);

            //Assert
            Assert.That(bookingService.GetCarPrice(bookingDate, returningDate, price), Is.EqualTo(expectedPrice));
        }

        [Test]
        public void IsRentedShouldReturnFalse()
        {
            //Arrange
            var falseCarId = 0;
            var falseBookingId = 0;

            var correctCarId = 1;
            var correctBookingId = 1;


            //Act
            var service = new CarService(rentalCarsDb, mapper);
            var bookingService = new BookingService(rentalCarsDb, mapper, service);

            //Assert
            Assert.That(bookingService.IsRented(falseBookingId, falseCarId), Is.EqualTo(false));
            Assert.That(bookingService.IsRented(correctBookingId, falseCarId), Is.EqualTo(false));
            Assert.That(bookingService.IsRented(falseBookingId, correctCarId), Is.EqualTo(false));
        }
        [Test]
        public void IsRentedShouldReturnTrue()
        {
            //Arrange
            var correctCarId = 1;
            var correctBookingId = 1;

            //Act
            var service = new CarService(rentalCarsDb, mapper);
            var bookingService = new BookingService(rentalCarsDb, mapper, service);

            //Assert
            Assert.That(bookingService.IsRented(correctBookingId, correctCarId), Is.EqualTo(true));
        }
        [Test]
        public void AllShouldBeEmpty()
        {
            //Arrange
            var isConfByDealers = false;
            var isConfByAdmin = true;

            var expectedCount = 0;

            //Act
            var service = new CarService(rentalCarsDb, mapper);
            var bookingService = new BookingService(rentalCarsDb, mapper, service);

            //Assert
            Assert.That(bookingService.All(isConfByAdmin, isConfByDealers).Bookings.Count(), Is.EqualTo(expectedCount));
        }

        [Test]
        public void AllShouldNotBeEmpty()
        {
            //Arrange
            var isConfByDealers = false;
            var isConfByAdmin = true;

            var expectedCount = 1;

            //Act
            var service = new CarService(rentalCarsDb, mapper);
            var bookingService = new BookingService(rentalCarsDb, mapper, service);

            //Assert
            Assert.That(bookingService.All(isConfByDealers, isConfByAdmin).Bookings.Count(), Is.EqualTo(expectedCount));
        }

        [Test]
        public void DeleteShouldReturnFalse()
        {
            //Arrange
            var id = 0;

            //Act
            var service = new CarService(rentalCarsDb, mapper);
            var bookingService = new BookingService(rentalCarsDb, mapper, service);

            //Assert
            Assert.That(bookingService.Delete(id), Is.EqualTo(false));
        }

        [Test]
        public void DeleteShouldReturnTrue()
        {
            //Arrange
            var id = 1;

            //Act
            var service = new CarService(rentalCarsDb, mapper);
            var bookingService = new BookingService(rentalCarsDb, mapper, service);

            //Assert
            Assert.That(bookingService.Delete(id), Is.EqualTo(true));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
        private void SeedDatabase()
        {
            var category = new Category()
            {
                Id = 13,
                Name = "Lux"
            };

            var category1 = new Category()
            {
                Id = 53,
                Name = "Fresh"
            };


            var buyer = new IdentityUser()
            {
                Id = "aeab5bc9-86c6-49ef-8f4f-faf8667d78b7",
                PasswordHash = "1234",
                Email = "2123@abv.bg",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };


            var user = new IdentityUser()
            {
                Id = "249b1fe6-3667-43d5-9ac9-4de6a92d923a",
                PasswordHash = "1234",
                Email = "2123@abv.bg",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };

            var dealer = new Dealer()
            {
                Id = 1,
                Name = "Iwan",
                PhoneNumber = "0000999",
                UserId = user.Id
            };

            var bmw = new Car()
            {
                Brand = "bmw",
                Model = "m3",
                CarPhoto = new byte[23123],
                DealerId = dealer.Id,
                Id = 2,
                Price = 100,
                Description = "asdasdasdasdadasda",
                IsBooked = false,
                IsPublic = true,
                Year = 2022,
                CategoryId = category.Id,
                Category = category,
                Dealer = dealer,
            };

            var mercedes = new Car()
            {
                Brand = "Mercedes",
                Model = "S500",
                CarPhoto = new byte[23123],
                DealerId = dealer.Id,
                Id = 1,
                Price = 200,
                Description = "asdasdasdasdadasda",
                IsBooked = false,
                IsPublic = false,
                Year = 2022,
                CategoryId = category.Id,
                Category = category,
                Dealer = dealer,
                BookingId = 1
            };

            var booking = new Booking()
            {
                CarId = mercedes.Id,
                IsConfirmedByDealer = true,
                IsConfirmedByAdmin = false,
                CustomerFirstName = "Iwo",
                CustomerLastName = "Iwanow",
                CustomerId = buyer.Id,
                DealerId = dealer.Id,
                BookingDate = DateTime.Now,
                ReturnDate = DateTime.Today.AddDays(1),
                DailyPrice = 1000,
                Id = 1
            };

            rentalCarsDb.Users.AddRange(user, buyer);
            rentalCarsDb.Categories.AddRange(category, category1);
            rentalCarsDb.Dealers.Add(dealer);
            rentalCarsDb.Cars.AddRange(bmw, mercedes);
            rentalCarsDb.Bookings.Add(booking);

            rentalCarsDb.SaveChanges();
        }
    }
}
