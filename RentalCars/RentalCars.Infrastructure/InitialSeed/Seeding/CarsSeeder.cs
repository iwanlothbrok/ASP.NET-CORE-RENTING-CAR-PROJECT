namespace RentalCars.Infrastructure.InitialSeed.Seeding
{
    using Microsoft.EntityFrameworkCore;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;
    using System;

    public class CarsSeeder : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            if (!data.Cars.Any(d => d.Id == 77))
            {
                SeedCars(data,
                    77,

                    "Lamborghini",
                    "Aventador",
                    "The Aventador Roadster has a unique engine" +
                    " cover design and an attachable wind deflector to improve cabin airflow at super high speeds" +
                    " as well as a gloss black finish on the A-pillars, windshield header, roof panels, and rear " +
                    "window area.",
                     "wwwroot/img/1Lambo.jpg",
                     2021,
                     true,
                     false,
                     450,
                     7,
                     77);


                SeedCars(data,
                   78,
                   "Mercedes",
                   "GLE Amg",
                   "The GLE isn't the quickest in the class, but it handles very well and is exceptionally easy" +
                " to drive in every situation. It also boasts a modern and " +
                "luxurious interior. Our main complaint centers on the stiff ride and compromised cargo capacity.",
                     "wwwroot/img/GleAmg.jpg",
                    2022,
                    true,
                    false,
                    600,
                    7,
                    78);

                data.Database.OpenConnection();
                try
                {
                    data.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Cars ON");
                    data.SaveChanges();
                    data.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Cars OFF");
                }
                finally
                {
                    data.Database.CloseConnection();
                }
            }
        }

        private Car SeedCars
            (ApplicationDbContext data,
            int id,
            string brand,
            string model,
            string description,
            string carPhotoPath,
            int year,
            bool isPublic,
            bool isBooked,
            decimal price,
            int categoryId,
            int dealerId)
        {
            Car car = new Car
            {
                Id = id,
                Brand = brand,
                Model = model,
                Description = description,
                CarPhoto = ReadFile(carPhotoPath),
                Year = year,
                IsPublic = isPublic,
                IsBooked = isBooked,
                Price = price,
                CategoryId = categoryId,
                DealerId = dealerId
            };

            data.Cars.Add(car);

            return car;
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
