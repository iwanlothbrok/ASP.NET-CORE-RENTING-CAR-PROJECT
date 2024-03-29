﻿namespace RentalCars.Core.Models.Renting
{
    public class RentCarModel
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public decimal Price { get; set; }

        public bool IsPublic { get; set; }

        public bool IsBooked { get; set; }
    }
}
