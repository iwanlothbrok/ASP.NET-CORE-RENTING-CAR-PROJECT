namespace RentalCars.Core.Services.Payments
{
    using RentalCars.Core.Models.Home;

    public interface IPaymentService
    {
        int CreatePaymentInfo(int bookingId,int carId, int debitCardId, bool IsValid, string userId);
    }
}
