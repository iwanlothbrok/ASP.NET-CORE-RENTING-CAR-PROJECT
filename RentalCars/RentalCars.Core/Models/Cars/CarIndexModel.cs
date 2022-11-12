namespace RentalCars.Core.Models.Cars
{
	public class CarIndexModel
	{
		public int Id { get; set; }

		public string Brand { get; set; } = null!;

		public byte[] ImageUrl { get; set; }=null!;
	}
}
