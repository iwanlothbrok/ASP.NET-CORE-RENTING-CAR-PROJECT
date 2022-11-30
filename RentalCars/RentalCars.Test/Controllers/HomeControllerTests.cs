namespace RentalCars.Test.Controllers
{
    using AutoMapper;
    using FluentAssertions;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using RentalCars.Controllers;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Services.Cars.Models;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Repositories.DatabaseRepositories;

    public class HomeControllerTests
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

        }

        [Test]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            // Arrange

            var carService = new CarService(rentalCarsDb, mapper);

            var homeController = new HomeController(carService);

            // Act
            var result = homeController.Index();

            // Assert
            result
                .Should()
                .NotBeNull()
                .And
                .BeAssignableTo<ViewResult>()
                .Which
                .Model
                .As<CarServiceModel>();
        }

        [Test]
        public void HomeControllerErrorMethod()
        {
            // Arrange
            var service = new CarService(rentalCarsDb, mapper);

            var homeController = new HomeController(
                service);

            // Act
            var result = homeController.Error();

            // Assert
            Assert.NotNull(result);
            Assert.That(result, Is.TypeOf(typeof(ViewResult)));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
