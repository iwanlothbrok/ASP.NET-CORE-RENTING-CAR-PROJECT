namespace RentalCars.Core.Services.Payments
{
    public interface IPaymentService
    {
        int CreatePaymentInfo(int bookingId, int debitCardId, bool IsValid);
    }
}
