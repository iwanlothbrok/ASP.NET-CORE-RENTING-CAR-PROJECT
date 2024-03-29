﻿namespace RentalCars.Core.Models.Renting
{
    public class AdminBookingModel
    {
        public int Id { get; set; }

        public string CustomerFirstName { get; set; } = null!;

        public string CustomerLastName { get; set; } = null!;
        public string CustomerPhoneNumber { get; set; } = null!;

        public string BookingDate { get; set; }

        public string ReturnDate { get; set; }

        public int CarId { get; set; }

        public int DealerId { get; set; }

        public decimal Price { get; set; }

        public string CustomerId { get; set; } = null!;

        public bool IsConfirmedByDealer { get; set; } = false;

        public bool IsConfirmedByAdmin { get; set; } = false;

        public bool IsPaid { get; set; }

        public bool IsExpired { get; set; }
    }
}
