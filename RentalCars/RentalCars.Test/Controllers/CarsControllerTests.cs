namespace RentalCars.Test.Controllers
{
    using AutoMapper;
    using FluentAssertions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using RentalCars.Controllers;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Models.Cars;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Services.Cars.Models;
    using RentalCars.Core.Services.Dealers;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;

    public class CarsControllerTests
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
        public void DetailsShouldReturnViewResult()
        {
            // Arrange
            var carService = new CarService(rentalCarsDb, mapper);
            var dealersService = new DealerService(rentalCarsDb, mapper);

            var carsController = new CarsController(dealersService, carService, mapper);
            var info = "bmw" + " " + "m3" + " " + 2022;
            // Act 
            var result = carsController.Details(1, info);

            // Assert
            // Assert
            result
                .Should()
                .NotBeNull()
                .And
                .BeAssignableTo<ViewResult>()
                .Which
                .Model
                .As<CarDetailsServiceModel>();
        }
        [Test]
        public void AllShouldReturnCorrectView()
        {
            // Arrange
            var carService = new CarService(rentalCarsDb, mapper);
            var dealersService = new DealerService(rentalCarsDb, mapper);
            var carsController = new CarsController(dealersService, carService, mapper);
            var model = new AllCarsQueryModel()
            {
                Brand = null,
                SearchTerm = null,
                Sorting = CarSorting.DateCreated,
                CurrentPage = 1,
                Cars = null,
                Brands = carService.AllBrands(),
                TotalCars = 3,
            };

            // Act 
            var result = carsController.All(model);

            // Assert
            result
                .Should()
                .NotBeNull()
                .And
                .BeAssignableTo<ViewResult>()
                .Which
                .Model
                .As<AllCarsQueryModel>();

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
                Id = "249b1fe6-3667-4sdgfswde3d5-9ac9-4de6a92d923a",
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
