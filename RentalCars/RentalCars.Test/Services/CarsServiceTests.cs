namespace RentalCars.Test.Services
{
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using RentalCars.Core.Extensions;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Repositories.DatabaseRepositories;
    using Car = Infrastructure.Data.Models.Car;
    using Category = Infrastructure.Data.Models.Category;
    using Dealer = Infrastructure.Data.Models.Dealer;

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

            SeedDatabase();
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
            var carId = 1;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert
            Assert.That(service.FindCar(carId), Is.Not.Null);
        }

        [Test]
        public void GetLastThreeCarsShouldBeEmpty()
        {
            //Arrange

            //Act
            var service = new CarService(rentalCarsDb, mapper);


            //Assert
            Assert.That(service.GetLastThreeCars(), Is.Empty);
        }

        [Test]
        public void GetLastThreeCarsShouldReturnCorrectly()
        {
            //Arrange
            var carCount = 1;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert
            var carsCount = service.GetLastThreeCars();

            Assert.That(service.GetLastThreeCars().Count, Is.EqualTo(carCount));
            Assert.That(service.GetLastThreeCars(), Is.Not.Empty);
        }

        [Test]
        public void DeleteShouldReturnFalseDealerIdIssue()
        {
            //Arrange
            var carId = 2;
            var dealerId = 0;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert

            Assert.That(service.Delete(carId, dealerId), Is.EqualTo(false));
        }

        [Test]
        public void DeleteShouldDeleteCarCorrectly()
        {
            //Arrange
            var carId = 2;
            var dealerId = 1;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert

            Assert.That(service.Delete(carId, dealerId), Is.EqualTo(true));
        }

        [Test]
        public void DeleteShouldReturnFalseCarIssue()
        {
            //Arrange
            var carId = 0;
            var dealerId = 1;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert

            Assert.IsFalse(service.Delete(carId, dealerId));
        }

        [Test]
        public void ChangeVisilityShouldChangeCorrectly()
        {
            //Arrange
            var carId = 2;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert

            Assert.IsTrue(service.ChangeVisility(carId));
        }

        [Test]
        public void ChangeVisilityShouldReturnFalse()
        {
            //Arrange
            var carId = 1111;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert

            Assert.IsFalse(service.ChangeVisility(carId));
        }

        [Test]
        public void DetailsShouldBeNull()
        {
            //Arrange
            var carId = 1111;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert

            Assert.IsNull(service.Details(carId));

        }

        [Test]
        public void DetailsShouldReturnTrueInfo()
        {
            //Arrange
            var carId = 1;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert

            Assert.NotNull(service.Details(carId));
        }

        [Test]
        public void AllBrandsShouldReturnCorrectly()
        {
            //Arrange
            var brandCount = 1;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert

            Assert.That(brandCount, Is.EqualTo(service.AllBrands().Count()));
        }

        [Test]
        public void AllBrandsShouldBeIncorrect()
        {
            //Arrange
            var brandCount = 10;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert

            Assert.That(brandCount, Is.Not.EqualTo(service.AllBrands().Count()));
        }

        [Test]
        public void ByUserShouldBeEmpty()
        {
            //Arrange
            var userId = "falseId";
            var carsCount = 0;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert
            Assert.That(service.ByUser(userId).Count(), Is.EqualTo(carsCount));
            Assert.IsEmpty(service.ByUser(userId));
        }

        [Test]
        public void ByUserShouldReturnThreeCars()
        {
            //Arrange
            var userId = "249b1fe6-3667-43d5-9ac9-4de6a92d923a";
            var carsCount = 3;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert
            Assert.That(service.ByUser(userId).Count(), Is.EqualTo(carsCount));
            Assert.That(service.ByUser(userId), Is.Not.Empty);
        }
        [Test]
        public void IsByDealerShouldFalse()
        {
            //Arrange
            var dealerId = 777;
            var carId = 0;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert
            Assert.That(service.IsByDealer(carId, dealerId), Is.False);
        }

        [Test]
        public void IsByDealerShouldTrue()
        {
            //Arrange
            var dealerId = 1;
            var carId = 1;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert
            Assert.That(service.IsByDealer(carId, dealerId), Is.True);
        }

        [Test]
        public void AllCategoriesShouldBeNine()
        {
            //Arrange
            var categoriesCount = 9;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert
            Assert.That(service.AllCategories().Count(), Is.EqualTo(categoriesCount));
        }

        [Test]
        public void AllCategoriesShouldBeEmpty()
        {
            //Arrange
            var categoryId = 77;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert
            Assert.IsEmpty(service.AllCategories().Where(c => c.Id == categoryId));
        }

        [Test]
        public void AllCarsShouldThree()
        {
            //Arrange
            var carsCount = 3;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert
            Assert.That(service.AllCars().Count(), Is.EqualTo(carsCount));
        }

        [Test]
        public void AllCarsShouldBeEmpty()
        {
            //Arrange
            var carId = 77;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert
            Assert.IsEmpty(service.AllCars().Where(c => c.Id == carId));
        }

        [Test]
        public void CarExistsShouldReturnFalse()
        {
            //Arrange
            int carId = 77;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert
            Assert.IsFalse(service.CarsExists(carId));
        }

        [Test]
        public void CarExistsShouldReturnTrue()
        {
            //Arrange
            int carId = 1;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert
            Assert.IsTrue(service.CarsExists(carId));
        }

        [Test]
        public void CategoryExistsShouldReturnFalse()
        {
            //Arrange
            int categoryId = 77;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert
            Assert.IsFalse(service.CategoryExists(categoryId));
        }

        [Test]
        public void CategoryExistsShouldReturnTrue()
        {
            //Arrange
            int categoryId = 13;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            //Assert
            Assert.IsTrue(service.CategoryExists(categoryId));
        }

        [Test]
        public async Task CreateShouldReturnZeroAndShouldNotWork()
        {
            //Arrange
            var carId = 0;

            var Brand = "bmw";
            var Model = "m3";
            List<IFormFile> CarPhoto = new List<IFormFile>();
            var DealerId = 1;
            var Price = 50;
            var Description = "asdasdasdasdadasda";
            var Year = 2022;
            var CategoryId = 1;

            //Act
            var service = new CarService(rentalCarsDb, mapper);

            int a = await service.Create(
                    Brand,
                    Model,
                    Description,
                    Price,
                    CarPhoto,
                    Year,
                    CategoryId,
                    DealerId);
            //Assert
            Assert.That(a, Is.EqualTo(carId));
        }

        [Test]
        public async Task EditShouldReturnFalseWithFakeCarId()
        {
            //Arrange
            var carId = 777;

            var Brand = "bmw";
            var Model = "m3";
            List<IFormFile> CarPhoto = new List<IFormFile>();
            var Price = 50;
            var Description = "asdasdasdasdadasda";
            var Year = 2022;
            var CategoryId = 1;

            //Act
            var service = new CarService(rentalCarsDb, mapper);


            //Assert
            Assert.IsFalse(await service.Edit(carId, Brand, Model, Price, Description, CarPhoto, Year, CategoryId));
        }

        [Test]
        public async Task EditShouldReturnFalseWithEmptyCarPhoto()
        {
            //Arrange
            var carId = 1;

            var Brand = "bmw";
            var Model = "m3";
            List<IFormFile> CarPhoto = new List<IFormFile>();
            var Price = 50;
            var Description = "asdasdasdasdadasda";
            var Year = 2022;
            var CategoryId = 1;

            //Act
            var service = new CarService(rentalCarsDb, mapper);


            //Assert
            Assert.IsFalse(await service.Edit(carId, Brand, Model, Price, Description, CarPhoto, Year, CategoryId));
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
                IsPublic = false,
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
                IsPublic = false,
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
                IsPublic = false,
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
