namespace RentalCars.Test.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using RentalCars.Controllers;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Services.Cars;
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
        public void Test1()
        {
            var service = new CarService(rentalCarsDb, mapper);
            var controller = new HomeController(service);


            var result = controller.Index() as ViewResult;

            

            Assert.AreEqual("Index", result?.ViewName);  
        }


        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
