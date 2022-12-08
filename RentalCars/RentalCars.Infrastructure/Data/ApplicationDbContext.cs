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

           // SeedCars(builder);

            base.OnModelCreating(builder);
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        private static void SeedCars(ModelBuilder builder)
        {
            string mercPath = "wwwroot/img/GleAmg.jpg";
            var mercedesPhotoPath = Path.GetFullPath(mercPath);

            string lamboPath = "wwwroot/img/1Lambo.jpg";
            var lamboPhotoPath = Path.GetFullPath(lamboPath);


            builder.Entity<Car>().HasData(
             new Car
             {
                 Id = 81,
                 Brand = "Lamborghini",
                 Model = "Aventador",
                 Description = "The removable roof consists of two carbon fibre panels, weighing 6 kg (13 lb) each, which required the reinforcement of the rear pillar to compensate for the loss of structural integrity as well as to accommodate the rollover protection and ventilation systems for the engine. The panels are easily removable and are stored in the front luggage compartment. The Aventador Roadster has a unique engine cover design and an attachable wind deflector to improve cabin airflow at super high speeds as well as a gloss black finish on the A-pillars, windshield header, roof panels, and rear window area. With a total weight of 1,625 kg (3,583 lb) it is only 50 kg (110 lb) heavier than the coupé (the weight of the roof, plus additional stiffening in the sills and A-pillars).",
                 CarPhoto = ReadFile(lamboPhotoPath),
                 Year = 2020,
                 IsPublic = true,
                 IsBooked = false,
                 Price = 400,
                 CategoryId = 7,
                 DealerId = 78
             });


            builder.Entity<Car>().HasData(
             new Car
             {
                 Id = 80,
                 Brand = "Mercedes",
                 Model = "GLE Amg",
                 Description = "The GLE isn't the quickest in the class, but it handles very well and is exceptionally easy" +
                 " to drive in every situation. It also boasts a modern and luxurious interior. Our main complaint centers on the stiff ride and compromised cargo capacity.",
                 CarPhoto = ReadFile(mercedesPhotoPath),
                 Year = 2022,
                 IsPublic = true,
                 IsBooked = false,
                 Price = 450,
                 CategoryId = 7,
                 DealerId = 77
             });
        }
        public static byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes 
            //to read from file.
            //In this case we want to read entire file. 
            //So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);

            return data;
        }
    }
}