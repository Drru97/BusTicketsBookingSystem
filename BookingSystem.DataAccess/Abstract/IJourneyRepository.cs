using System;
using System.Collections.Generic;
using BookingSystem.Entities;

namespace BookingSystem.DataAccess.Abstract
{
    public interface IJourneyRepository
    {
        IEnumerable<Journey> Journeys { get; }

        void AddJourney(Journey journey);
        void AddJourney(Route route, DateTime departureDateTime, DateTime arrivalDateTime, Bus bus, Driver driver);
        Journey RemoveJourney(Journey journey);
        Journey UpdateJourney(Journey journey);
    }
}
