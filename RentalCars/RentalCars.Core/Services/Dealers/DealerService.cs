namespace RentalCars.Core.Services.Dealers
{
    using RentalCars.Data;

    public class DealerService : IDealerService
    {
        private readonly ApplicationDbContext data;


        public DealerService(ApplicationDbContext data)
            => this.data = data;

        public bool IsDealer(string userId)
            => this.data
                .Dealers
                .Any(d => d.UserId == userId);


        public int IdByUser(string userId)
            => this.data
                .Dealers
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();
    }
}
