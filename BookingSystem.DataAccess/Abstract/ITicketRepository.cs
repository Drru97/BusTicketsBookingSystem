using System;
using System.Collections.Generic;
using BookingSystem.Entities;

namespace BookingSystem.DataAccess.Abstract
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> Tickets { get; }

        void AddTicket(Ticket ticket);
        void AddTicket(Journey journey, DateTime purchaseDateTime, decimal price, Passenger passenger, int seat);
    }
}
