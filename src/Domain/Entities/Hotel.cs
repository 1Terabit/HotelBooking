using System;
using System.Collections.Generic;

namespace HotelBooking.Domain.Entities
{
    public class Hotel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }
        public bool IsActive { get; set; }
        public required ICollection<Room> Rooms { get; set; }
    }
}
