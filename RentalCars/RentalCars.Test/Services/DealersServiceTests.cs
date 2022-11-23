namespace RentalCars.Test.Services
{
    using AutoMapper;
    using Microsoft.Extensions.DependencyInjection;
    using Nest;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Services.Dealers;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Repositories.DatabaseRepositories;

    public class DealersServiceTests
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
                .AddSingleton<IDealerService, DealerService>()
                .BuildServiceProvider();         

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            mapper = new Mapper(configuration);

            rentalCarsDb = serviceProvider.GetService<ApplicationDbContext>()!;
        }

        [Test]
        public void IsDealerShouldReturnFalse()
        {
            //
            string userId = "falseId";
            //
            var service = new DealerService(rentalCarsDb, mapper);
            //
            Assert.That(service.IsDealer(userId), Is.False);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
