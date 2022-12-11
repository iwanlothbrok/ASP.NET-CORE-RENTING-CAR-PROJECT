namespace RentalCars.Core.Services.Payments
{
    public interface IPaymentService
    {
        int CreatePaymentInfo(int bookingId,int carId, int debitCardId, bool IsValid, string userId);
    }
}
