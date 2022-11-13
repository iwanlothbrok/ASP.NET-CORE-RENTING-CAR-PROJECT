namespace RentalCars.Core.Services.Dealers
{
    using RentalCars.Core.Models.Dealers;

    public interface IDealerService
    {
         bool IsDealer(string userId);

         int IdByUser(string userId);

         void Become(BecomeDealerFormModel dealer, string userId);
    }
}
