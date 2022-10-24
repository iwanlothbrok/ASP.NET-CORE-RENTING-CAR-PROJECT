namespace RentalCars.Core.Extensions
{
	using AutoMapper;
	using RentalCars.Core.Models.Cars;
	using RentalCars.Core.Models.Dealers;
	using RentalCars.Core.Services.Cars.Models;
	using RentalCars.Infrastructure.Data.Models;

	public class MappingProfile : Profile
	{

		public MappingProfile()
		{
			this.CreateMap<CarDetailsServiceModel, CarFormModel>();
			this.CreateMap<CarDetailsServiceModel, CarDetailsServiceModel>();
			this.CreateMap<BecomeDealerFormModel, Dealer>();
				
		}

	}
}
