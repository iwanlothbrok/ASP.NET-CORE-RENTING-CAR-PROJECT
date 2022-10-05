using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentalCars.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
      
    }
}
