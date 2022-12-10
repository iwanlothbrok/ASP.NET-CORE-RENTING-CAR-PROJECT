namespace RentalCars.Core.Services.Dealers
{
    using RentalCars.Core.Models.Dealers;
    using RentalCars.Infrastructure.Data.Models;

    public interface IDealerService
    {
        Dealer? GetDealer(int id);
        bool IsDealer(string userId);
        int IdByUser(string userId);
        void Become(BecomeDealerFormModel dealer, string userId);
    }
}
