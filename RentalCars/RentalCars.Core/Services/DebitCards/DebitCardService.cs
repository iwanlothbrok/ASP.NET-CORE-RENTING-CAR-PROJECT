namespace RentalCars.Core.Services.DebitCards
{
    using RentalCars.Data;
    using RentalCars.Infrastructure.Data.Models;

    public class DebitCardService : IDebitCardService
    {
        private readonly ApplicationDbContext data;

        public DebitCardService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public int CreateDebitCard(long creditCardNumber, int cvv, string fullNameOnCard, string expMonth, int expYear)
        {
            if (cvv == 0 
                || creditCardNumber == 0
                || creditCardNumber.ToString().Length != 16 
                || cvv.ToString().Length != 3)
            {
                return -1;
            }

            FakeDebitCard card = new FakeDebitCard
            {
                CVV = cvv,
                CreditCardNumber = creditCardNumber,
                ExpMonth = expMonth,
                ExpYear = expYear,
                FullName = fullNameOnCard,
            };

            if (card == null)
            {
                return -1;
            }

            this.data.FakeDebitCards.Add(card);
            this.data.SaveChanges();

            return card.Id;
        }
    }
}