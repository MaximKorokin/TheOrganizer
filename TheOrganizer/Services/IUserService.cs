using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheOrganizer.Entities;
using TheOrganizer.Model;

namespace TheOrganizer.Services
{
    public interface IUserService
    {
        UserEntity Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetByEmail(string email);
        bool AddUser(User user);
        User GetUserById(int id);
        bool ChangeUser(User user);
        bool DeleteUser(int id);
    }
}
