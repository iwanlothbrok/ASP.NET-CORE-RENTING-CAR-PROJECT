namespace RentalCars.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(Constants.AreaName)]
    //[Route("/Admin/[controller]/[Action]/{id?}")]
    [Authorize(Roles = Constants.AdminName)]
    public class BaseAdminController : Controller
	{
	}
}
