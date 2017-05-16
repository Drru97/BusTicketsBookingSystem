using System.Collections.Generic;
using BookingSystem.Entities;

namespace BookingSystem.DataAccess.Abstract
{
    public interface IBusRepository
    {
        IEnumerable<Bus> Buses { get; }

        void AddBus(Bus bus);
        Bus RemovBus(Bus bus);
    }
}
