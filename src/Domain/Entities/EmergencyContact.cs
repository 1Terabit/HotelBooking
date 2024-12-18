using System;

namespace HotelBooking.Domain.Entities
{
    public class EmergencyContact
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Relationship { get; set; }
        public required Guest Guest { get; set; }
    }

}
