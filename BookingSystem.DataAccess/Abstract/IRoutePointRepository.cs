using System.Collections.Generic;
using BookingSystem.Entities;

namespace BookingSystem.DataAccess.Abstract
{
    public interface IRoutePointRepository
    {
        IEnumerable<RoutePoint> RoutePoints { get; }

        void AddRoutePoint(RoutePoint routePoint);
        RoutePoint RemoveRoutePoint(RoutePoint routePoint);
        RoutePoint UpdateRoutePoint(RoutePoint routePoint);
    }
}
