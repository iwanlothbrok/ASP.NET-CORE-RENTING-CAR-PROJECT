namespace RentalCars.Test.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using RentalCars.Core.Services.Dealers;
    using RentalCars.Core.Services.Statistics;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;

    public class StatisticsServiceTests
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
                .AddSingleton<IDealerService, DealerService>()
                .BuildServiceProvider();

            rentalCarsDb = serviceProvider.GetService<ApplicationDbContext>()!;

            SeedDb();
        }

        [Test]
        public void TotalShouldReturnCorrectCounts()
        {
            //Arrange
            var service = new StatisticsService(rentalCarsDb);

            //Act
            var result = service.Total();

            //Assert
            Assert.That(result.TotalUsers, Is.EqualTo(4));
            Assert.That(result.TotalCars, Is.EqualTo(2));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private void SeedDb()
        {
            var category = new Category()
            {
                Id = 13,
                Name = "Lux"
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
                Id = 2,
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
            var audi = new Car()
            {
                Brand = "audi",
                Model = "a3",
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

            rentalCarsDb.Users.Add(user);
            rentalCarsDb.Categories.Add(category);
            rentalCarsDb.Dealers.Add(dealer);
            rentalCarsDb.Cars.AddRange(bmw,audi);

            rentalCarsDb.SaveChanges();
        }
    }
}
