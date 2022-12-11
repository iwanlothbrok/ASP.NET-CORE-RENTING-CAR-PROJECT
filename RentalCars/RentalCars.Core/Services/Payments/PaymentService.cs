namespace RentalCars.Core.Services.Payments
{
    using RentalCars.Core.Services.Bookings;
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;

    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext data;
        private readonly IBookingService booking;
        

        public PaymentService(ApplicationDbContext data, IBookingService booking)
        {
            this.data = data;
            this.booking = booking;
        }
        public int CreatePaymentInfo(int bookingId,int carId, int debitCardId, bool IsValid, string userId)
        {
            if (bookingId == 0)
            {
                return -1;
            }
            if (carId == 0)
            {
                return -1;
            }
            if (debitCardId == 0)
            {
                return -1;
            }
            Payment payment = new Payment
            {
                CarId = carId,
                BookingId = bookingId,
                DebitCardId = debitCardId,
                IsValid = IsValid,
                PaymentTime = DateTime.Now
            };

            if (payment is null)
            {
                return -1;
            }
            var book = this.booking.GetBookByUserId(userId);

            book.IsPaid = true;

            this.data.Payments.Add(payment);
            this.data.SaveChanges();

            return payment.Id;
        }
    }
}
