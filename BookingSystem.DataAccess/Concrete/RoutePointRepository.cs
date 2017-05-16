using System;
using System.Collections.Generic;
using BookingSystem.DataAccess.Abstract;
using BookingSystem.Entities;

namespace BookingSystem.DataAccess.Concrete
{
    public class RoutePointRepository : IRoutePointRepository
    {
        private readonly BookingSystemContext _context;
        public IEnumerable<RoutePoint> RoutePoints => _context.RoutePoint;

        public RoutePointRepository(BookingSystemContext context)
        {
            _context = context;
        }

        public void AddRoutePoint(RoutePoint routePoint)
        {
            if (routePoint == null)
            {
                throw new ArgumentNullException(nameof(routePoint), "Route point cannot be null");
            }

            _context.RoutePoint.Add(routePoint);
            _context.SaveChanges();
        }

        public RoutePoint RemoveRoutePoint(RoutePoint routePoint)
        {
            if (routePoint == null)
            {
                throw new ArgumentNullException(nameof(routePoint), "Route point cannot be null");
            }

            var removableRoutePoint = _context.RoutePoint.Find(routePoint);
            if (removableRoutePoint != null)
            {
                _context.RoutePoint.Remove(routePoint);
                _context.SaveChanges();
            }

            return routePoint;
        }
    }
}
