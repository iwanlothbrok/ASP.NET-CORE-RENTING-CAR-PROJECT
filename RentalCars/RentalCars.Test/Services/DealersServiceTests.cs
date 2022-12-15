namespace RentalCars.Test.Services
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Services.Dealers;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;
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
                .AddSingleton<IDealerService, DealerService>()
                .BuildServiceProvider();

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            mapper = new Mapper(configuration);

            rentalCarsDb = serviceProvider.GetService<ApplicationDbContext>()!;

            SeedDb();
        }

        [Test]
        public void GetDealerShouldReturnNull()
        {
            //Arrange
            var fakeId = 12312;

            //Act
            var service = new DealerService(rentalCarsDb, mapper);


            //Assert
            Assert.That(service.GetDealer(fakeId), Is.Null);
        }

        [Test]
        public void GetDealerShouldReturnValidDealer()
        {
            //Arrange
            var fakeId = 2;

            //Act
            var service = new DealerService(rentalCarsDb, mapper);


            //Assert
            Assert.That(service.GetDealer(fakeId), Is.Not.Null);
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

        [Test]
        public void IsDealerShouldReturnTrue()
        {
            //Arrange

            var userId = "249b1fe6-3667-43d5-9ac9-4de6a92d923a";

            //Act
            var service = new DealerService(rentalCarsDb, mapper);

            //Assert
            Assert.That(service.IsDealer(userId), Is.True);
        }

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

        [Test]
        public void IdByUserShouldReturnTrueId()
        {
            //Arrange
            var dealerId = 2;
            var userId = "249b1fe6-3667-43d5-9ac9-4de6a92d923a";

            //Act
            var service = new DealerService(rentalCarsDb, mapper);

            //Assert
            Assert.That(service.IdByUser(userId), Is.EqualTo(dealerId));
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


