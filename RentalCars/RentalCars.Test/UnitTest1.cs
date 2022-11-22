namespace RentalCars.Test
{
    using Microsoft.Extensions.DependencyInjection;

    public class Tests
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;

        [SetUp]
        public void Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();


            serviceProvider = serviceCollection
                .AddSingleton(sp=> dbContext.CreateContext())
                .AddSingleton<IApplicationDbRepostoryu>
                .BuildServiceProvider();

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [TearDown]
        public void TearDown()
        {
            Assert.Pass();
        }
    }
}