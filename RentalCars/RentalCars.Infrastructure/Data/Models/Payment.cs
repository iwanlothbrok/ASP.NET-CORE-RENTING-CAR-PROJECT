﻿namespace RentalCars.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Payment
    {
        [Key]
        public int Id { get; set; }

        public DateTime PaymentTime = DateTime.Now;

        [Required]
        public int BookingId { get; set; }
        public Booking? Booking { get; set; } 

        [Required]
        public int DebitCardId{ get; set; }
        public FakeDebitCard? DebitCard { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public bool IsValid { get; set; }
    }
}
