using RentalCars.Infrastructure.Data.Models;

namespace RentalCars.Core.Services.DebitCards
{
    public class DebitCardService : IDebitCardService
    {
        public int CreateDebitCard(int creditCardNumber, int cvv, string fullNameOnCard, string expMonth, int expYear)
        {
            if (cvv == 0 || expYear == 0)
            {
                return -1;
            }

            DebitCard card = new DebitCard
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

            return card.Id;
        }
    }
}