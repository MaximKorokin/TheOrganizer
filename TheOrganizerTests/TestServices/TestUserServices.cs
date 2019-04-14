using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheOrganizer.Entities;
using TheOrganizer.Model;
using TheOrganizer.Services;

namespace TheOrganizerTests.TestServices
{
    public class TestUserServices : IUserService
    {
        private List<User> Users { get; set; } = new List<User>
        {
            new User
            {
                Id = 1,
                Name = "Name1",
                Email = "Email1",
                Password = "Password1",
            },
            new User
            {
                Id = 2,
                Name = "Name2",
                Email = "Email2",
                Password = "Password2",
            },
            new User
            {
                Id = 3,
                Name = "Name3",
                Email = "Email3",
                Password = "Password3",
            },
        };

        public bool AddUser(User user)
        {
            return true;
        }

        public UserEntity Authenticate(string email, string password)
        {
            return new UserEntity()
            {
                Token = "token",
            };
        }

        public bool ChangeUser(User user)
        {
            return true;
        }

        public bool DeleteUser(int id)
        {
            return true;
        }

        public IEnumerable<User> GetAll()
        {
            return Users;
        }

        public User GetByEmail(string email)
        {
            return Users.First(u => u.Email == email);
        }

        public User GetUserById(int id)
        {
            return Users.First(u => u.Id == id);
        }
    }
}
