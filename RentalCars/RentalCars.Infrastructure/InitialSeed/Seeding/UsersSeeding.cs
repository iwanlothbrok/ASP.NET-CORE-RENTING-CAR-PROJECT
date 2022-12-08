namespace RentalCars.Infrastructure.InitialSeed.Seeding
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using RentalCars.Data;
    using static RentalCars.Infrastructure.InitialSeed.Seeding.Constants.SeedingConstants;
    public class UsersSeeder : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            var emails = new List<string>() { userOneEmail, userTwoEmail };

            if (!data.Users.Any(u => u.Email == emails.First() && u.Email == emails.Last()))
            {
                var userManager =
                    serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                foreach (var email in emails)
                {
                    string password;
                    if (email == userOneEmail)
                    {
                        password = userOnePassword;
                    }
                    else
                    {
                        password = userTwoPassword;
                    }

                    Task.
                        Run(async () =>
                        {
                            var user = new IdentityUser()
                            { Email = email, UserName = email };
                            await userManager.CreateAsync(user, password);
                        })
                        .GetAwaiter()
                        .GetResult();
                }
            }
        }
    }
}

