namespace RentalCars.Test.Controllers.Api
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using RentalCars.Controllers.Api;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Services.Statistics;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;

    public class StatisticsApiControllerTest
    {

        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;
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

            rentalCarsDb = serviceProvider.GetService<ApplicationDbContext>()!;

            SeedDatabase();
        }

        [Test]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            // Arrange
            var service = new StatisticsService(rentalCarsDb);
            var statisticsController = new StatisticsApiController(service);

            // Act
            var result = statisticsController.GetStatistics();

            // Assert

            Assert.NotNull(result);
            Assert.That(result.TotalCars, Is.EqualTo(4));
            Assert.That(result.TotalRents, Is.EqualTo(0));
            Assert.That(result.TotalUsers, Is.EqualTo(4));
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

            var user = new IdentityUser()
            {
                Id = "249b1fe6-3667gsdgsdgsdgsdg43d5-9ac9-4de6a92d923a",
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

            var car3 = new Car()
            {
                Brand = "bmw",
                Model = "m3",
                CarPhoto = new byte[23123],
                DealerId = dealer.Id,
                Id = 55,
                Price = 50,
                Description = "asdasdasdasdadasda",
                IsBooked = false,
                IsPublic = true,
                Year = 2022,
                CategoryId = category.Id,
                Category = category,
                Dealer = dealer,
            };

            var car = new Car()
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
                Category = category,
                Dealer = dealer,
            };
            var car1 = new Car()
            {
                Brand = "bmw",
                Model = "m3",
                CarPhoto = new byte[23123],
                DealerId = dealer.Id,
                Id = 2,
                Price = 50,
                Description = "asdasdasdasdadasda",
                IsBooked = false,
                IsPublic = true,
                Year = 2022,
                CategoryId = category.Id,
                Category = category,
                Dealer = dealer,
            };
            var car2 = new Car()
            {
                Brand = "bmw",
                Model = "m3",
                CarPhoto = new byte[23123],
                DealerId = dealer.Id,
                Id = 3,
                Price = 50,
                Description = "asdasdasdasdadasda",
                IsBooked = false,
                IsPublic = true,
                Year = 2022,
                CategoryId = category.Id,
                Category = category,
                Dealer = dealer,
            };

            rentalCarsDb.Users.Add(user);
            rentalCarsDb.Categories.AddRange(category, category1);
            rentalCarsDb.Dealers.Add(dealer);
            rentalCarsDb.Cars.AddRange(car, car1, car2, car3);

            rentalCarsDb.SaveChanges();
        }
    }
}
