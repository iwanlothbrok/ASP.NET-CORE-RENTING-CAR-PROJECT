namespace RentalCars.Infrastructure.Repositories.DatabaseRepositories
{
    using RentalCars.Data;

    public class ApplicatioDbRepository : Repository, IApplicatioDbRepository
    {
        public ApplicatioDbRepository(ApplicationDbContext context)
        {
            this.Context = context;
        }
    }
}