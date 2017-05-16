using System.Collections.Generic;
using BookingSystem.Entities;

namespace BookingSystem.DataAccess.Abstract
{
    public interface IRouteRepository
    {
        IEnumerable<Route> Routes { get; }

        void AddRoute(Route route);
        void AddRoute(RoutePoint departurePoint, RoutePoint arrivalPoint, double length);
        Route RemoveRoute(Route route);
    }
}
