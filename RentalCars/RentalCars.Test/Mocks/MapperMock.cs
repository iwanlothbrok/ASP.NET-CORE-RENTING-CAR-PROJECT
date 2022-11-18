namespace RentalCars.Test.Mocks
{
    using AutoMapper;
    using RentalCars.Core.Extensions;

    public static class MapperMock
    {
        public static IMapper mapper
        {
            get
            {
                var mapperConfig = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                });

                return new Mapper(mapperConfig);
            }
        }
    }
}
