namespace RentalCars.Core.Services.Statistics
{
    using RentalCars.Core.Models.Statistics;
    using RentalCars.Data;
    using System.Linq;

    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext data;

        public StatisticsService(ApplicationDbContext data)
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalCars = this.data.Cars.Count(c => c.IsPublic);
            var totalUsers = this.data.Users.Count();
            var totalRents = this.data.Bookings.Where(c=>c.IsPaid == true).Count();

            return new StatisticsServiceModel
            {
                TotalCars = totalCars,
                TotalUsers = totalUsers,
                TotalRents = totalRents
            };
        }
    }
}
