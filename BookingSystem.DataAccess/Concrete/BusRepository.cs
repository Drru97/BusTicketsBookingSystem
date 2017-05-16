using System;
using System.Collections.Generic;
using BookingSystem.DataAccess.Abstract;
using BookingSystem.Entities;

namespace BookingSystem.DataAccess.Concrete
{
    public class BusRepository : IBusRepository
    {
        private readonly BookingSystemContext _context;
        public IEnumerable<Bus> Buses => _context.Bus;

        public BusRepository(BookingSystemContext context)
        {
            _context = context;
        }

        public void AddBus(Bus bus)
        {
            if (bus == null)
            {
                throw new ArgumentNullException(nameof(bus), "Bus cannot be null");
            }

            _context.Bus.Add(bus);
            _context.SaveChanges();
        }

        public Bus RemovBus(Bus bus)
        {
            if (bus == null)
            {
                throw new ArgumentNullException(nameof(bus), "Bus cannot be null");
            }

            var removableBus = _context.Bus.Find(bus);
            if (removableBus != null)
            {
                _context.Bus.Remove(removableBus);
                _context.SaveChanges();
            }

            return removableBus;
        }
    }
}
