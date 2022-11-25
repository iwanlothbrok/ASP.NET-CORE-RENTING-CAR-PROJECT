namespace RentalCars.Test.Services
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Repositories.DatabaseRepositories;
    using Car = Infrastructure.Data.Models.Car;
    using Category = Infrastructure.Data.Models.Category;
    using Dealer = Infrastructure.Data.Models.Dealer;

    public class CarsServiceTests
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

            SeedDb();
        }

        [Test]
        public void FindCarShouldReturnNull()
        {
            //Arrange
            int carId = 111;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert
            Assert.That(service.FindCar(carId), Is.Null);
        }

        [Test]
        public void FindCarShouldReturnCar()
        {
            //Arrange
            int carId = 1;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert
            Assert.That(service.FindCar(carId), Is.Not.Null);
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
                IsPublic = false,
                Year = 2022,
                CategoryId = category.Id,
                Category = category,
                Dealer = dealer,
            };

            rentalCarsDb.Users.Add(user);
            rentalCarsDb.Categories.Add(category);
            rentalCarsDb.Dealers.Add(dealer);
            rentalCarsDb.Cars.Add(car);

            rentalCarsDb.SaveChanges();
        }
    }
}
