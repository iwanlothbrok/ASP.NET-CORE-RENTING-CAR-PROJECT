namespace RentalCars.Data
{

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RentalCars.Infrastructure.Data.Models;
    using RentalCars.Infrastructure.InitialSeed;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfiguration(new InitialDataConfiguration<Category>(@"InitialSeed/categories.json"));

            base.OnModelCreating(builder);
        }

        public DbSet<Car> Cars { get; set; }
        // public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}