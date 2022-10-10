namespace RentalCars.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [ValidateAntiForgeryToken]
    public class BaseController : Controller
    {
      
    }
}
