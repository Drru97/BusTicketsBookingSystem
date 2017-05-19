using System;
using System.Collections.Generic;
using BookingSystem.DataAccess.Abstract;
using BookingSystem.Entities;

namespace BookingSystem.DataAccess.Concrete
{
    public class TicketRepository : ITicketRepository
    {
        private readonly BookingSystemContext _context;
        public IEnumerable<Ticket> Tickets => _context.Ticket;

        public TicketRepository(BookingSystemContext context)
        {
            _context = context;
        }

        public void AddTicket(Ticket ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket), "Ticket cannot be null");
            }

            _context.Ticket.Add(ticket);
            _context.SaveChanges();
        }

        public void AddTicket(Journey journey, DateTime purchaseDateTime, decimal price, Passenger passenger, int seat)
        {
            AddTicket(new Ticket
            {
                Journey = journey,
                PurchaseDateTime = purchaseDateTime,
                PassengerId = passenger.Id,
                Price = price,
                Seat = seat
            });
        }
    }
}
