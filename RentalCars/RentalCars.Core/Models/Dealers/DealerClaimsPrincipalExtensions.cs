namespace RentalCars.Core.Models.Dealers
{
using System.Security.Claims;
    public static class DealerClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
