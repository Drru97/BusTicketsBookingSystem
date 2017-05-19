using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Entities;

namespace BookingSystem.DataAccess.Abstract
{
    public interface IAdministratorRepository
    {
        void AddAdministrator(Administrator administrator);
        Administrator RemoveAdministrator(Administrator administrator);
        bool Login(string username, string password);
    }
}
