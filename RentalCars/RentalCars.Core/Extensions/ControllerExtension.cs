namespace RentalCars.Core.Extensions
{
    using Microsoft.AspNetCore.Mvc;

    public static class ControllerExtension
    {
        public static string GetControllerName(this Type controllerType)
           => controllerType.Name.Replace(nameof(Controller), string.Empty);
    }
}
