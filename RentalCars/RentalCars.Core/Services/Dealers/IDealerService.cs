namespace RentalCars.Core.Services.Dealers
{
    using RentalCars.Core.Models.Dealers;

    public interface IDealerService
    {
        public bool IsDealer(string userId);
        public int IdByUser(string userId);
        public void Become(BecomeDealerFormModel dealer,string userId);
    }
}
