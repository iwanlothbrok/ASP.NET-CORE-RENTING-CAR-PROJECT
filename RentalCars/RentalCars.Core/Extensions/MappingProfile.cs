namespace RentalCars.Core.Extensions
{
	using AutoMapper;
	using RentalCars.Core.Models.Cars;
	using RentalCars.Core.Models.Dealers;
	using RentalCars.Core.Models.Renting;
	using RentalCars.Core.Services.Cars.Models;
	using RentalCars.Infrastructure.Data.Models;

	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			//Mappin for cars
			this.CreateMap<CarDetailsServiceModel, CarFormModel>();
			this.CreateMap<Car, CarServiceModel>();
			this.CreateMap<Car, Car>();
			this.CreateMap<CarDetailsServiceModel, CarDetailsServiceModel>();
			this.CreateMap<Car, CarDetailsServiceModel>()
				.ForMember(c => c.UserId, cfg => cfg.MapFrom(c => c.Dealer.UserId));

			//Mappin for dealers
			this.CreateMap<BecomeDealerFormModel, Dealer>();

			//Mapping for bookings
			this.CreateMap<Booking, AdminBookingModel>()
                .ForMember(c => c.Price, cfg => cfg.MapFrom(c => c.DailyPrice));
        }
	}
}
