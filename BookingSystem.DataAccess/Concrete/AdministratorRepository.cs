using System;
using System.Linq;
using BookingSystem.DataAccess.Abstract;
using BookingSystem.DataAccess.Helpers;
using BookingSystem.Entities;

namespace BookingSystem.DataAccess.Concrete
{
    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly BookingSystemContext _context;

        public AdministratorRepository(BookingSystemContext context)
        {
            _context = context;
        }

        public void AddAdministrator(Administrator administrator)
        {
            if (administrator == null)
            {
                throw new ArgumentNullException(nameof(administrator), "Administrator canmnot be null");
            }

            if (string.IsNullOrEmpty(administrator.Username) || string.IsNullOrEmpty(administrator.Password))
            {
                throw new ArgumentException("Username or password cannot be null or empty", nameof(administrator));
            }

            _context.Administrators.Add(administrator);
            _context.SaveChanges();
        }

        public Administrator RemoveAdministrator(Administrator administrator)
        {
            throw new NotImplementedException();
        }

        public bool Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Username or password must contain value");
            }

            var administrator = _context.Administrators
                .FirstOrDefault(a => a.Username == username);

            if (administrator != null)
            {
                return administrator.Password == Hashable.Hash(password);
            }

            return false;
        }
    }
}
