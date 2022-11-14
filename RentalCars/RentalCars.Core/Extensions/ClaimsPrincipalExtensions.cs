namespace RentalCars.Core.Extensions
{
    using System.Security.Claims;
    using static RentalCars.Infrastructure.Data.Models.Constants.DataConstants.Web;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
                        => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
           => user.IsInRole(AdminRoleName);
    }
}
