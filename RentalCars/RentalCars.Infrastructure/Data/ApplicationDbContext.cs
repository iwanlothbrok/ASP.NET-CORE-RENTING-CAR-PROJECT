namespace RentalCars.Data
{
    using Microsoft.AspNetCore.Identity;
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
            builder
              .Entity<Car>()
              .HasOne(c => c.Category)
              .WithMany(c => c.Cars)
              .HasForeignKey(c => c.CategoryId)
              .OnDelete(DeleteBehavior.Restrict);

            builder
              .Entity<Car>()
              .HasOne(c => c.Dealer)
              .WithMany(d => d.Cars)
              .HasForeignKey(c => c.DealerId)
              .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Dealer>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Dealer>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                  .Entity<Booking>()
                  .HasOne<IdentityUser>()
                  .WithOne()
                  .HasForeignKey<Booking>(d => d.CustomerId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder
              .Entity<Booking>()
              .HasOne<Car>()
              .WithOne()
              .HasForeignKey<Booking>(d => d.CarId)
              .OnDelete(DeleteBehavior.Restrict);

            builder
             .Entity<Booking>()
             .HasOne<Dealer>()
             .WithOne()
             .HasForeignKey<Booking>(d => d.DealerId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.ApplyConfiguration(new InitialDataConfiguration<Category>(@"InitialSeed/categories.json"));

            base.OnModelCreating(builder);
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<FakeDebitCard> FakeDebitCards { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}