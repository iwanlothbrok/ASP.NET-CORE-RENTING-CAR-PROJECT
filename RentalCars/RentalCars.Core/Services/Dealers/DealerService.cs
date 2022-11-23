namespace RentalCars.Core.Services.Dealers
{
    using AutoMapper;
    using RentalCars.Core.Models.Dealers;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;

    public class DealerService : IDealerService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public DealerService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public bool IsDealer(string userId)
            => this.data
                .Dealers
                .Any(d => d.UserId == userId);

        public void Become(BecomeDealerFormModel dealer, string userId)
        {
            Dealer dealerForm = this.mapper.Map<Dealer>(dealer);

            if (dealerForm != null)
            {
                dealerForm.UserId = userId;
                data.Dealers.Add(dealerForm);
                data.SaveChanges();
            }
        }

        public int IdByUser(string userId)
            => this.data
                .Dealers
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();
    }
}
