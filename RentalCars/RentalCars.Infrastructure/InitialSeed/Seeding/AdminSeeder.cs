namespace RentalCars.Infrastructure.InitialSeed.Seeding
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using RentalCars.Data;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using static RentalCars.Infrastructure.InitialSeed.Seeding.Constants.SeedingConstants;

    public class AdminSeeder : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            if (!data.Users.Any(e => e.Email == adminEmail))
            {
                var userManager =
                    serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                Task
                    .Run(async () =>
                    {
                        var admin = new IdentityUser() { Email = adminEmail, UserName = adminEmail };
                        await userManager.CreateAsync(admin, adminPassword);
                        await userManager.AddToRoleAsync(admin, AdminRole);
                    })
                    .GetAwaiter()
                    .GetResult();
            }
        }
    }
}
