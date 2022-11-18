namespace RentalCars.Test.Services
{
    using AutoMapper;
    using RentalCars.Core.Services.Dealers;
    using RentalCars.Infrastructure.Data.Models;
    using RentalCars.Test.Mocks;
    using Xunit;

    public class DealersServiceTest
    {
        private const string UserId = "TestUserId";

        [Fact]
        public void IsDealerShouldReturtTrueWhenUserIsDealer()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            data.Dealers.Add(new Dealer { UserId = UserId });
            data.SaveChanges();

            //Act 
            bool result = dealerService.IsDealer(UserId);

            //Аssert
            Assert.True(result);
        }


    }
}
