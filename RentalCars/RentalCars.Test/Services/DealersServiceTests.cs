namespace RentalCars.Test.Services
{
    using AutoMapper;
    using Microsoft.Extensions.DependencyInjection;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Services.Dealers;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Repositories.DatabaseRepositories;
    using Car = Infrastructure.Data.Models.Car;
    using Dealer = Infrastructure.Data.Models.Dealer;

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
            var repo = serviceProvider.GetService<IApplicatioDbRepository>();


            //await SeedDatabaseAsync(repo);
        }

        [Test]
        public void IsDealerShouldReturnFalse()
        {
            //Arrange
            string userId = "falseId";

            //Act
            var service = new DealerService(rentalCarsDb, mapper);


            //Assert
            Assert.That(service.IsDealer(userId), Is.False);
        }

        //[Test]
        //public void IsDealerShouldReturnTrue()
        //{
        //    var dealer = new Dealer {
        //        Id = 1,
        //        Name = "iiaaaaa",
        //        UserId= "SASD121ADASDAD",
        //        PhoneNumber = "1231231231"
        //    };

        //    rentalCarsDb.Add(dealer);
        //    rentalCarsDb.SaveChanges();

        //    //Act
        //    var service = new DealerService(rentalCarsDb, mapper);


        //    //Assert
        //    Assert.That(service.IsDealer(dealer.UserId), Is.True);
        //}

        //[Test]
        //public void BecomeShouldNotWork()
        //{
        //    //Arrange
        //    BecomeDealerFormModel dealer = new BecomeDealerFormModel()
        //    {
        //        Name = "Iwo",
        //        PhoneNumber = "0999"
        //    };

        //    string userId = "falseId";
        //    //Act
        //    var service = new DealerService(rentalCarsDb, mapper);


        //    //Assert

        //}

        [Test]
        public void IdByUserShouldReturnZero()
        {
            //Arrange
            string userId = "falseId";

            //Act
            var service = new DealerService(rentalCarsDb, mapper);

            //Assert
            Assert.That(service.IdByUser(userId), Is.EqualTo(0));
        }

        //[Test]
        //public void IdByUserShouldReturnTrueId()
        //{
        //    //Arrange
        //    var dealer = new Dealer()
        //    {
        //        Id = 1,
        //        Name = "Iwo",
        //        PhoneNumber = "09999",
        //        UserId = "d57dae59-d8e2-405d-bbd2-79ed3515a31b",
        //        Cars = new List<Car>()
        //    };

        //    //Act
        //    var service = new DealerService(rentalCarsDb, mapper);

        //    //Assert
        //    Assert.That(service.IdByUser(dealer.UserId), Is.EqualTo(1));
        //}

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        //public async Task SeedDatabaseAsync(IApplicatioDbRepository repo)
        //{
        //    var dealer = new Dealer()
        //    {
        //        Id = 2,
        //        Cars = new List<Car>(),
        //        Name = "Iwan",
        //        PhoneNumber = "0000999",
        //        UserId = "FAKEid1"

        //    };

        //    var category = new Category
        //    {
        //        Id = 13,
        //        Cars = new List<Car>(),
        //        Name = "Lux"
        //    };

        //    var car = new Car()
        //    {
        //        Brand = "bmw",
        //        Model = "m3",
        //        CarPhoto = new byte[23123],
        //        DealerId = 1,
        //        Id = 1,
        //        Price = 50,
        //        Description = "asdasdasdasdadasda",
        //        IsBooked = false,
        //        IsPublic = false,
        //        Year = 2022,
        //        CategoryId = category.Id,
        //        Category = category,
        //        Dealer = dealer,
        //    };

        //    await repo.AddAsync(dealer);
        //    await repo.AddAsync(car);
        //    await repo.SaveChangesAsync();
        //}
    }
}


