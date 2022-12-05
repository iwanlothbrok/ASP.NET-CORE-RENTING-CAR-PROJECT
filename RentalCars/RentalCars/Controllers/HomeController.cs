namespace RentalCars.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using RentalCars.Core.Services.Cars;
    using RentalCars.Core.Services.Cars.Models;

    public class HomeController : Controller
    {
        public ICarService cars;
        private readonly IMemoryCache cache;

        public HomeController(ICarService cars, IMemoryCache cache)
        {
            this.cars = cars;
            this.cache = cache;
        }

        public IActionResult Index()
        {
           const string latestCarsCacheKey = "LatestCarsCacheKey";

            var latestCars = this.cache.Get<List<CarServiceModel>>(latestCarsCacheKey);

            if (latestCars == null)
            {
                latestCars = this.cars
                   .GetLastThreeCars()
                   .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(latestCarsCacheKey, latestCars, cacheOptions);
            }

            return View(latestCars);
        }

        public IActionResult Error()
       => View();
    }

}

