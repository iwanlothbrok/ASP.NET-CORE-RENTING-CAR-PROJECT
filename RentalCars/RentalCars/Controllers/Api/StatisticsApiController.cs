namespace RentalCars.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Core.Models.Statistics;
    using RentalCars.Core.Services.Statistics;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statistics;

        public StatisticsApiController(IStatisticsService statistics)
            => this.statistics = statistics;

        [HttpGet]
        public StatisticsServiceModel GetStatistics() 
            => this.statistics.Total();
    }
}
