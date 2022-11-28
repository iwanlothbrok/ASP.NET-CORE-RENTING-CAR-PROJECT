namespace RentalCars.Test.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using RentalCars.Areas.Admin.Controllers;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Models.Renting;
    using RentalCars.Core.Services.Bookings;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;
    using RentalCars.Infrastructure.Repositories.DatabaseRepositories;

    public class BookingControllerTests
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

            //Act
            var service = new CarService(rentalCarsDb, mapper);
            var bookingService = new BookingService(rentalCarsDb, mapper, service);

            var controller = new BookingController(bookingService);

            //Assert
            var result = controller.Rent() as ViewResult;
            Assert.That(result.ViewName, Is.EqualTo("Rent"));
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
