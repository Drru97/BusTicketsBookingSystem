﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using BookingSystem.DataAccess.Abstract;
using BookingSystem.Entities;

namespace BookingSystem.DataAccess.Concrete
{
    public class DriverRepository : IDriverRepository
    {
        private readonly BookingSystemContext _context;
        public IEnumerable<Driver> Drivers => _context.Driver;

        public DriverRepository(BookingSystemContext context)
        {
            _context = context;
        }

        public void AddDriver(Driver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver), "Driver cannot be null");
            }

            _context.Driver.Add(driver);
            _context.SaveChanges();
        }

        public Driver RemoveDriver(Driver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver), "Driver cannot be null");
            }

            var removableDriver = _context.Driver.Find(driver.Id);
            if (removableDriver != null)
            {
                _context.Driver.Remove(removableDriver);
                _context.SaveChanges();
            }

            return removableDriver;
        }

        public Driver UpdateDriver(Driver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver), "Driver cannot be null");
            }

            _context.Entry(driver).State = EntityState.Modified;
            _context.SaveChanges();

            return driver;
        }
    }
}
