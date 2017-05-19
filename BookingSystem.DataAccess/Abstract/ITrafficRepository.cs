using System.Collections.Generic;
using BookingSystem.Entities;

namespace BookingSystem.DataAccess.Abstract
{
    public interface ITrafficRepository
    {
        IEnumerable<Traffic> Traffics { get; }

        void AddTraffic(Traffic traffic);
        void AddTraffic(Journey journey, Ticket ticket);
    }
}
