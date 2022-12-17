namespace RentalCars.Infrastructure.InitialSeed.Seeding
{
    using RentalCars.Data;
    using System;
    using System.Collections.Generic;

    public class Seeder : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            var seeders = new List<ISeeder>()
            {
                new RoleSeeder(),
                new UsersSeeder(),
                new AdminSeeder(),
                new DealerSeeder(),
                new CategorySeeder(),
                new CarsSeeder()
            };

            foreach (var seeder in seeders)
            {
                seeder.Seed(data, serviceProvider);
            }
        }
    }
}
