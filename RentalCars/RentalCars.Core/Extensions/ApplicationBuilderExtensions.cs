namespace RentalCars.Core.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using RentalCars.Data;
    using RentalCars.Infrastructure.InitialSeed.Seeding;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
           this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var services = scopedServices.ServiceProvider;
            var data = scopedServices.ServiceProvider
                .GetService<ApplicationDbContext>();

            data.Database.Migrate();

            var seeder = new Seeder();
            seeder.Seed(data, services);

            data.Database.Migrate();

            return app;
        }
    }
}
