namespace RentalCars.Test.Services
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Services.Dealers;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;
    using RentalCars.Infrastructure.Repositories.DatabaseRepositories;

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

            //await SeedDatabaseAsync(repo);
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
            int carId = 111;

            //rentalCarsDb = IMock<ApplicationDbContext>();

            var category = new Category()
            {
                Id = 13,
                Cars = new List<Car>(),
                Name = "Lux"
            };
            var dealer = new Dealer()
            {
                Id = 2,
                Cars = new List<Car>(),
                Name = "Iwan",
                PhoneNumber = "0000999",
                UserId = "FAKEid1"

            };

            var car = new Car()
            {
                Brand = "bmw",
                Model = "m3",
                CarPhoto = new byte[23123],
                DealerId = 1,
                Id = carId,
                Price = 50,
                Description = "asdasdasdasdadasda",
                IsBooked = false,
                IsPublic = false,
                Year = 2022,
                CategoryId = category.Id,
                Category = category,
                Dealer = dealer,
            };

            rentalCarsDb.Add(car);
            rentalCarsDb.Add(dealer);
            rentalCarsDb.Add(category);

            rentalCarsDb.SaveChanges();

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert
            Assert.That(service.FindCar(car.Id), Is.EqualTo(car));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
