using System;
using System.Collections.Generic;
using HotelBooking.Domain.Enums;

namespace HotelBooking.Domain.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public required Guest MainGuest { get; set; }
        public required EmergencyContact EmergencyContact { get; set; }
        public required ICollection<Guest> AdditionalGuests { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public ReservationStatus Status { get; set; }
    }

}
