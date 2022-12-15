namespace RentalCars.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using RentalCars.Controllers;

    [Area(Constants.AreaName)]
    public class AdminController : BaseAdminController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Home()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> CreateRolesandUsers()
        {
            bool x = await this.roleManager.RoleExistsAsync("Admin");
            if (x)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                await this.roleManager.CreateAsync(role);

                //var user = new IdentityUser();
                //user.UserName = "Admin";
                //user.Email = "admin@admin.com";
                //string userPWD = "Admin.1";

                IdentityUser user = this.userManager.Users.FirstOrDefault(e => e.Email == "admin@crs.com");

                //IdentityResult chkUser = await userManager.CreateAsync(user, userPWD);

                //Add default User to Role Admin    
                if (user != null)
                {
                    IdentityResult result1 = await this.userManager.AddToRoleAsync(user, role.Name);
                }
            }

            // creating Creating Manager role     
            x = await this.roleManager.RoleExistsAsync("Manager");
            if (!x)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Manager";
                await this.roleManager.CreateAsync(role);
            }

            // creating Creating Employee role     
            x = await this.roleManager.RoleExistsAsync("Employee");
            if (!x)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Employee";
                await this.roleManager.CreateAsync(role);
            }
            return Redirect("https://localhost:7163/");
        }
    }
}