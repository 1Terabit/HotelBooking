using System;
using HotelBooking.Domain.Enums;

namespace HotelBooking.Domain.Entities
{
    public class Room
    {
        public Guid Id { get; set; }
        public required string RoomNumber { get; set; }
        public RoomType Type { get; set; }
        public decimal BasePrice { get; set; }
        public decimal TaxRate { get; set; }
        public bool IsActive { get; set; }
        public required string Location { get; set; }
    }
}
