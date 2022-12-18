namespace RentalCars.Test.Services
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Services.Bookings;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Services.Payments;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;

    public class PaymentServiceTests
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
        public void CreateMethodShouldBeSuccessful()
        {
            //Arrange
            CarService carService = new CarService(rentalCarsDb, mapper);
            BookingService bookingService = new BookingService(rentalCarsDb, mapper, carService);
            //Act
            PaymentService service = new PaymentService(rentalCarsDb, bookingService);

            //Assert
            Assert.That(service.CreatePaymentInfo(1, 1, 1, true, "buyerFakeId1"), Is.TypeOf<int>());
            Assert.That(service.CreatePaymentInfo(1, 1, 1, true, "buyerFakeId1"), Is.GreaterThan(-1));
        }

        [Test]
        public void CreateMethodShouldBeNegativeNumber()
        {
            //Arrange
            CarService carService = new CarService(rentalCarsDb, mapper);
            BookingService bookingService = new BookingService(rentalCarsDb, mapper, carService);
            //Act
            PaymentService service = new PaymentService(rentalCarsDb, bookingService);

            //Assert
            Assert.That(service.CreatePaymentInfo(0, 1, 1, true, "buyerFakeId1"), Is.EqualTo(-1));
            Assert.That(service.CreatePaymentInfo(1, 0, 1, true, "buyerFakeId1"), Is.EqualTo(-1));
            Assert.That(service.CreatePaymentInfo(1, 1, 0, true, "buyerFakeId1"), Is.EqualTo(-1));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private void SeedDatabase()
        {
            IdentityUser user = new IdentityUser()
            {
                Id = "userFakeId1",
                PasswordHash = "1234",
                Email = "2123@abv.bg",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };
            IdentityUser buyer = new IdentityUser()
            {
                Id = "buyerFakeId1",
                PasswordHash = "1234",
                Email = "asd@abv.bg",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };
            Category category = new Category()
            {
                Id = 1,
                Name = "SVU",
            };
            Dealer dealer = new Dealer()
            {
                Id = 1,
                Name = "Iwan",
                PhoneNumber = "0000999",
                UserId = user.Id
            };
            Car mercedes = new Car()
            {
                Brand = "bmw",
                Model = "m3",
                CarPhoto = new byte[23123],
                DealerId = dealer.Id,
                Id = 1,
                Price = 50,
                Description = "asdasdasdasdadasda",
                IsBooked = false,
                IsPublic = true,
                Year = 2022,
                CategoryId = category.Id,
                Dealer = dealer,
            };
            var book = new Booking()
            {
                IsConfirmedByAdmin = true,
                BookingDate = DateTime.Today,
                CarId = 1,
                DealerId = 1,
                CustomerFirstName = "aaa",
                CustomerLastName = "aaa",
                CustomerId = buyer.Id,
                CustomerPhoneNumber = "asda",
                DailyPrice = 1241,
                IsConfirmedByDealer = true,
                ReturnDate = DateTime.MaxValue,
                Id = 1
            };
            var debitCard = new FakeDebitCard()
            {
                CreditCardNumber = 1111222233334444,
                CVV = 123,
                ExpMonth = "may",
                FullName = "aaasda",
                ExpYear = 2031,
                Id = 1
            };
            rentalCarsDb.Users.AddRange(user,buyer);
            rentalCarsDb.Dealers.Add(dealer);
            rentalCarsDb.Categories.Add(category);
            rentalCarsDb.Cars.Add(mercedes);
            rentalCarsDb.Bookings.Add(book);
            rentalCarsDb.FakeDebitCards.Add(debitCard);

            rentalCarsDb.SaveChanges();
        }
    }
}
