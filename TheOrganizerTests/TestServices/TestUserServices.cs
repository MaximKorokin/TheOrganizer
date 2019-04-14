using System;
using System.Collections.Generic;
using System.Text;
using TheOrganizer.Entities;
using TheOrganizer.Model;
using TheOrganizer.Services;

namespace TheOrganizerTests.TestServices
{
    public class TestUserServices : IUserService
    {
        public bool AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public UserEntity Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool ChangeUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
