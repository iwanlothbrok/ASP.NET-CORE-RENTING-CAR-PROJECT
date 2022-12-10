namespace RentalCars.Core.Services.Payments
{
    using RentalCars.Infrastructure.Data.Models;

    public class PaymentService : IPaymentService
    {
        public int CreatePaymentInfo(int bookingId, int debitCardId, bool IsValid)
        {
            if (bookingId == 0)
            {
                return -1;
            }
            if (debitCardId == 0)
            {
                return -1;
            }
            Payment payment = new Payment
            {
                BookingId = bookingId,
                DebitCardId = debitCardId,
                IsValid = IsValid,
                PaymentTime = DateTime.Now
            };

            if (payment is null)
            {
                return -1;
            }

            return payment.Id;
        }
    }
}
