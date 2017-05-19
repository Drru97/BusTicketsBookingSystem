using System;
using System.Collections.Generic;
using BookingSystem.DataAccess.Abstract;
using BookingSystem.Entities;

namespace BookingSystem.DataAccess.Concrete
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly BookingSystemContext _context;
        public IEnumerable<Passenger> Passengers => _context.Passenger;

        public PassengerRepository(BookingSystemContext context)
        {
            _context = context;
        }

        public void AddPassenger(Passenger passenger)
        {
            if (passenger == null)
            {
                throw new ArgumentNullException(nameof(passenger), "Passenger cannot be null");
            }

            _context.Passenger.Add(passenger);
            _context.SaveChanges();
        }

        public void AddPassenger(string firstName, string lastName)
        {
            AddPassenger(new Passenger
            {
                FirstName = firstName,
                LastName = lastName
            });
        }
    }
}
