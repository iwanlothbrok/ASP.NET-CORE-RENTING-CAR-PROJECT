namespace RentalCars.Areas.Admin.Controllers
{
    using RentalCars.Controllers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Area(Constants.AreaName)]
    public class AdminController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> CreateRolesandUsers()
        {
            bool x = await roleManager.RoleExistsAsync("Admin");
            if (x)
            {
                // first we create Admin rool    
                var role = new IdentityRole();
                role.Name = "Admin";
                await roleManager.CreateAsync(role);

                //Here we create a Admin super user who will maintain the website                   

                //var user = new IdentityUser();
                //user.UserName = "Admin";
                //user.Email = "admin@admin.com";
                //string userPWD = "Admin.1";

                IdentityUser user = userManager.Users.FirstOrDefault(e => e.Email == "admin@crs.com");

                //IdentityResult chkUser = await userManager.CreateAsync(user, userPWD);

                //Add default User to Role Admin    
                if (user != null)
                {

                    var result1 = await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            // creating Creating Manager role     
            x = await roleManager.RoleExistsAsync("Manager");
            if (!x)
            {
                var role = new IdentityRole();
                role.Name = "Manager";
                await roleManager.CreateAsync(role);
            }

            // creating Creating Employee role     
            x = await roleManager.RoleExistsAsync("Employee");
            if (!x)
            {
                var role = new IdentityRole();
                role.Name = "Employee";
                await roleManager.CreateAsync(role);
            }
            return Redirect("https://localhost:7163/");
        }
    }
}
