using System;
using System.Collections.Generic;
using System.Data.Entity;
using BookingSystem.DataAccess.Abstract;
using BookingSystem.Entities;

namespace BookingSystem.DataAccess.Concrete
{
    public class JourneyRepository : IJourneyRepository
    {
        private readonly BookingSystemContext _context;
        public IEnumerable<Journey> Journeys => _context.Journey;

        public JourneyRepository(BookingSystemContext context)
        {
            _context = context;
        }

        public void AddJourney(Journey journey)
        {
            if (journey == null)
            {
                throw new ArgumentNullException(nameof(journey), "Journey cannot be null");
            }

            _context.Journey.Add(journey);
            _context.SaveChanges();
        }

        public void AddJourney(Route route, DateTime departureDateTime, DateTime arrivalDateTime, Bus bus, Driver driver)
        {
            AddJourney(new Journey
            {
                Route = route,
                DepartureTime = departureDateTime,
                ArrivalTime = arrivalDateTime,
                Bus = bus,
                Driver = driver
            });
        }

        public Journey RemoveJourney(Journey journey)
        {
            if (journey == null)
            {
                throw new ArgumentNullException(nameof(journey), "Journey cannot be null");
            }

            var removableJourney = _context.Journey.Find(journey.Id);
            if (removableJourney != null)
            {
                _context.Journey.Remove(removableJourney);
                _context.SaveChanges();
            }

            return removableJourney;
        }

        public Journey UpdateJourney(Journey journey)
        {
            if (journey == null)
            {
                throw new ArgumentNullException(nameof(journey), "Journey cannot be null");
            }

            _context.Entry(journey).State = EntityState.Modified;
            _context.SaveChanges();

            return journey;
        }

        public IEnumerable<int> GetAllSeats(Journey journey)
        {
            var seats = new List<int>();
            for (int i = 1; i <= journey.Bus.PassengersCount; i++)
            {
                seats.Add(i);
            }

            return seats;
        }
    }
}
