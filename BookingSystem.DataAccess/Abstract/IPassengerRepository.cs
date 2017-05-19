using System.Collections.Generic;
using BookingSystem.Entities;

namespace BookingSystem.DataAccess.Abstract
{
    public interface IPassengerRepository
    {
        IEnumerable<Passenger> Passengers { get; }

        void AddPassenger(Passenger passenger);
        void AddPassenger(string firstName, string lastName);
    }
}
