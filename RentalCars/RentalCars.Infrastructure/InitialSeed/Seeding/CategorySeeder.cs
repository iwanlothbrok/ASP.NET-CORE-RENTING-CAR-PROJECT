namespace RentalCars.Infrastructure.InitialSeed.Seeding
{
    using Microsoft.EntityFrameworkCore;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;

    public class CategorySeeder : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            if (!data.Categories.Any(d => d.Id == 1))
            {
                var category1 = AddCategoryInDb(data, 1, "Mini");
                var category2 = AddCategoryInDb(data, 2, "Economy");
                var category3 = AddCategoryInDb(data, 3, "Midsize");
                var category4 = AddCategoryInDb(data, 4, "Large");
                var category5 = AddCategoryInDb(data, 5, "SUV");
                var category6 = AddCategoryInDb(data, 6, "Vans");
                var category7 = AddCategoryInDb(data, 7, "Luxury");

                data.Database.OpenConnection();
                try
                {
                    data.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Categories ON");
                    data.SaveChanges();
                    data.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Categories OFF");
                }
                finally
                {
                    data.Database.CloseConnection();
                }
            }
        }

        private Category AddCategoryInDb
            (ApplicationDbContext data, int id, string name)
        {
            Category category = new Category()
            {
                Id = id,
                Name = name,
            };

            data.Categories.Add(category);

            return category;
        }
    }
}
