using System;
using System.Collections.Generic;
using BookingSystem.DataAccess.Abstract;
using BookingSystem.Entities;

namespace BookingSystem.DataAccess.Concrete
{
    public class TrafficRepository : ITrafficRepository
    {
        private readonly BookingSystemContext _context;
        public IEnumerable<Traffic> Traffics => _context.Traffic;

        public TrafficRepository(BookingSystemContext context)
        {
            _context = context;
        }

        public void AddTraffic(Traffic traffic)
        {
            if (traffic == null)
            {
                throw new ArgumentNullException(nameof(traffic), "Traffic cannot be null");
            }

            _context.Traffic.Add(traffic);
            _context.SaveChanges();
        }

        public void AddTraffic(Journey journey, Ticket ticket)
        {
            AddTraffic(new Traffic
            {
                Journey = journey,
                Ticket = ticket
            });
        }
    }
}
