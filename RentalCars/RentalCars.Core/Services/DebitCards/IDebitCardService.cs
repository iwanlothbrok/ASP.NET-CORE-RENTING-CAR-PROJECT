namespace RentalCars.Core.Services.DebitCards
{
    public interface IDebitCardService
    {
        int CreateDebitCard(int creditCardNumber, int cvv, string fullNameOnCard, string expMonth, int expYear);
    }
}
