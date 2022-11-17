namespace RentalCars.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Core.Services.Cars.Models;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Models.Cars.Api;

    [Route("api/[controller]")]
    [ApiController]
    public class CarsApiController : ControllerBase
    {
        private readonly ICarService cars;

        public CarsApiController(ICarService cars)
            => this.cars = cars;

        [HttpGet]
        public CarQueryServiceModel All([FromQuery] AllCarsApiRequestModel query)
            => this.cars.All(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.CarsPerPage);
    }
}
