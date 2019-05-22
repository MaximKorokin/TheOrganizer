using System.Collections.Generic;
using TheOrganizer.Entities;
using TheOrganizer.Model;

namespace TheOrganizer.Services
{
    public interface IUserService
    {
        UserEntity Authenticate(string email, string password);
        IEnumerable<User> GetAll();
        User GetByEmail(string email);
        bool AddUser(User user);
        User GetUserById(int id);
        bool EditUser(User user);
        bool RemoveUser(int id);
    }
}
