namespace RentalCars.Infrastructure.Data.Models.Constants
{
    public static class DataConstants
    {
        public static class CarConstants
        {
            public const int BrandMinLength = 2;
            public const int BrandMaxLength = 20;
            public const int ModelMinLength = 2;
            public const int ModelMaxLength = 30;
            public const int DescriptionMinLength = 10;
            public const int YearMinValue = 2000;
            public const int YearMaxValue = 2050;

        }

        public static class CategoryConstants
        {
            public const int NameMaxLenght = 30;

        }

        public static class DealerConstants
        {
            public const int NameMaxLenght = 50;
            public const int NameMinLenght = 3;
            public const int PhoneNumberMaxLength = 30;
            public const int PhoneNumberMinLength = 6;

        }

        public class DateFormatingConstant
        {
            public const string NormalDateFormat = "dd.mm.yyyy";
        }
    }
}
