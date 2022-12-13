namespace RentalCars.Test.Services
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using RentalCars.Core.Services.Bookings;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Services.DebitCards;
    using RentalCars.Core.Services.Payments;
    using RentalCars.Data;

    public class DebitCardServiceTests
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
                .BuildServiceProvider();

            rentalCarsDb = serviceProvider.GetService<ApplicationDbContext>()!;
        }

        [Test]
        public void CreateMethodShouldBeSuccessful()
        {
            //Act
            DebitCardService service = new DebitCardService(rentalCarsDb);

            //Assert
            Assert.That(service.CreateDebitCard(1111111111111111,123,"iw iwasd", "may", 2023), Is.TypeOf<int>());
            Assert.That(service.CreateDebitCard(1111111111111111, 123, "iw iwasd", "may", 2023), Is.GreaterThan(-1));
        }

        [Test]
        public void CreateMethodShouldBeNegativeNumber()
        {
            //Act
            DebitCardService service = new DebitCardService(rentalCarsDb);

            //Assert
            Assert.That(service.CreateDebitCard(0, 123, "iw iwasd", "may", 2023), Is.EqualTo(-1));
            Assert.That(service.CreateDebitCard(1111111111111111, 0, "iw iwasd", "may", 2023), Is.EqualTo(-1));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}
