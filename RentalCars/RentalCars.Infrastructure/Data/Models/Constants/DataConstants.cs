﻿namespace RentalCars.Infrastructure.Data.Models.Constants
{
    public static class DataConstants
    {
        public static class Car
        {
            public const int BrandMinLength = 2;
            public const int BrandMaxLength = 20;
            public const int ModelMinLength = 2;
            public const int ModelMaxLength = 30;
            public const int DescriptionMinLength = 10;
            public const int YearMinValue = 2000;
            public const int YearMaxValue = 2050;
            public const int PriceMinValue = 20;
            public const int PriceMaxValue = 5000;
        }
        public class UserSeeding
        {
            public const string userOneEmail = "user1@abv.bg";
            public const string userOnePassword = "User.1";
            public const string userTwoEmail = "user2@abv.bg";
            public const string userTwoPassword = "User.2";
        }
        public class User
        {
            public const int FullNameMinLength = 2;
            public const int FullNameMaxLength = 40;
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;
        }

        public static class Category
        {
            public const int NameMaxLenght = 30;
        }

        public static class Dealer
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 3;
            public const int PhoneNumberMaxLength = 30;
            public const int PhoneNumberMinLength = 6;
        }
        public static class DebitCard
        {
            public const int CardNumberLength = 16;
            public const int CVVLength = 3;
            public const int MaxFullNameLenght = 100;
            public const int MinFullNameLenght = 6;
            public const int ExpYearMin = 2022;
            public const int ExpYearMax= 2050;
        }
        public class DateFormating
        {
            public const string NormalDateFormat = "dd.mm.yyyy";
        }

        public class Message
        {
            public const string ErrorMessage = "Something is wrong!";
            public const string SuccsessMessage = "The job is done!";
            public const string WarningMessage = "Warning!";
        }
        public class Web
        {
            public const string GlobalMessageKey = "GlobalMessage";
            public const string AdminRoleName = "Admin";
        }
    }
}
