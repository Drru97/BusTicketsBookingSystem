using System;
using System.Collections.Generic;
using System.Data.Entity;
using BookingSystem.DataAccess.Abstract;
using BookingSystem.Entities;

namespace BookingSystem.DataAccess.Concrete
{
    public class RouteRepository : IRouteRepository
    {
        private readonly BookingSystemContext _context;
        public IEnumerable<Route> Routes => _context.Route;

        public RouteRepository(BookingSystemContext context)
        {
            _context = context;
        }

        public void AddRoute(Route route)
        {
            if (route == null)
            {
                throw new ArgumentNullException(nameof(route), "Route cannot be null");
            }

            _context.Route.Add(route);
            _context.SaveChanges();
        }

        public void AddRoute(RoutePoint departurePoint, RoutePoint arrivalPoint, double length)
        {
            AddRoute(new Route
            {
                DeparturePointId = departurePoint.Id,
                ArrivalPointId = arrivalPoint.Id,
                Length = length
            });
        }

        public Route RemoveRoute(Route route)
        {
            if (route == null)
            {
                throw new ArgumentNullException(nameof(route), "Route cannot be null");
            }

            var removableRoute = _context.Route.Find(route.Id);
            if (removableRoute != null)
            {
                _context.Route.Remove(removableRoute);
                _context.SaveChanges();
            }

            return removableRoute;
        }

        public Route UpdateRoute(Route route)
        {
            if (route == null)
            {
                throw new ArgumentNullException(nameof(route), "Route cannot be null");
            }

            _context.Entry(route).State = EntityState.Modified;
            _context.SaveChanges();

            return route;
        }
    }
}
