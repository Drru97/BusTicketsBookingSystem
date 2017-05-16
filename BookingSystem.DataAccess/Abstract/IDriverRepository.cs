using System.Collections.Generic;
using BookingSystem.Entities;

namespace BookingSystem.DataAccess.Abstract
{
    public interface IDriverRepository
    {
        IEnumerable<Driver> Drivers { get; }

        void AddDriver(Driver driver);
        Driver RemoveDriver(Driver driver);
    }
}
