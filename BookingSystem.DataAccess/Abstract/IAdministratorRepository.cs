using BookingSystem.Entities;

namespace BookingSystem.DataAccess.Abstract
{
    public interface IAdministratorRepository
    {
        void AddAdministrator(Administrator administrator);
        bool Login(string username, string password);
    }
}
