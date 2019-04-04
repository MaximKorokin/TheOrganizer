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
        User GetByEmail(string email);
        IEnumerable<User> GetAll();
    }
}
