namespace RentalCars.Infrastructure.InitialSeed.Seeding
{
    using RentalCars.Data;
    using System;

    public interface ISeeder
    {
        void Seed(ApplicationDbContext data, IServiceProvider serviceProvider);
    }
}
