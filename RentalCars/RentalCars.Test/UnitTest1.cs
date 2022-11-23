//namespace RentalCars.Test
//{
//    using AutoMapper;
//    using Microsoft.Extensions.DependencyInjection;
//    using RentalCars.Core.Services.Cars;
//    using RentalCars.Infrastructure.Repositories.DatabaseRepositories;

//    public class Tests
//    {
//        private ServiceProvider serviceProvider;
//        private InMemoryDbContext dbContext;

//        //[SetUp]
//        //public void Setup()
//        //{
//        //    dbContext = new InMemoryDbContext();
//        //    var serviceCollection = new ServiceCollection();


//        //    serviceProvider = serviceCollection
//        //        .AddSingleton(sp => dbContext.CreateContext())
//        //        .AddSingleton<ICarService, CarService>()
//        //        .AddSingleton<IMapper, Mapper>()
//        //        .BuildServiceProvider();
//        //}

//        //[SetUp]
//        //public async Task Setup()
//        //{
//        //    dbContext = new InMemoryDbContext();
//        //    var serviceCollection = new ServiceCollection();

//        //    serviceProvider = serviceCollection
//        //        .AddSingleton(sp => dbContext.CreateContext())
//        //        .AddSingleton<IApplicatioDbRepository, ApplicatioDbRepository>()
//        //        .AddSingleton<IOrderService, OrderService>()
//        //        .BuildServiceProvider();

//        //    var repo = serviceProvider.GetService<IApplicatioDbRepository>();
//        //    await SeedDbAsync(repo);
//        //}

//        [Test]
//        public void Test1()
//        {
//            Assert.Pass();
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            Assert.Pass();
//        }
//    }
//}