namespace RentalCars.Infrastructure.InitialSeed.Seeding
{
    using Microsoft.EntityFrameworkCore;
    using Nest;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;
    using static RentalCars.Infrastructure.InitialSeed.Seeding.Constants.SeedingConstants;

    public class DealerSeeder : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            if (!data.Dealers.Any(d => d.Id == 77))
            {
                string userOneId = GetUserId(userOneEmail, data);
                var dealerOne = AddDealerInDb(data, 77, "LuxCars Auto", "0889100821", userOneId);

                string userTwoId = GetUserId(userTwoEmail, data);
                var dealerTwo = AddDealerInDb(data, 78, "Iws Auto", "0881230821", userTwoId);

                data.Database.OpenConnection();
                try
                {
                    data.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Dealers ON");
                    data.SaveChanges();
                    data.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Dealers OFF");
                }
                finally
                {
                    data.Database.CloseConnection();
                }
            }
        }

        private Dealer AddDealerInDb
            (ApplicationDbContext data, int id, string name, string phoneNumber, string userId)
        {
            Dealer dealer = new Dealer()
            {
                Id = id,
                Name = name,
                PhoneNumber = phoneNumber,
                UserId = userId
            };

            data.Dealers.Add(dealer);

            return dealer;
        }

        private string GetUserId(string email, ApplicationDbContext data)
            => data
                .Users
                .Where(c => c.Email == email)
                .FirstOrDefault(u => u.Email == email).Id;

    }
}
