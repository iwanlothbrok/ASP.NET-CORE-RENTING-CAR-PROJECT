namespace RentalCars.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        [AllowAnonymous]
        public IActionResult Index() => View();


        public IActionResult Error() => View();
    }
}
