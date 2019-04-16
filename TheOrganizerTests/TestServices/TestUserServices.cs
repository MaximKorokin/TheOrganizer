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

        private User CurUser { get; set; }

        public bool AddUser(User user)
        {
            if (user == null)
            {
                return false;
            }

            Users.Add(user);

            return true;
        }

        public UserEntity Authenticate(string email, string password)
        {
            var user = GetByEmail(email);

            if (user == null)
            {
                return null;
            }

            if(user.Password == password)
            {
                CurUser = user;

                return new UserEntity()
                {
                    Token = "token",
                };
            }

            return null;
        }

        public bool ChangeUser(User newUser)
        {
            var user = Users.FirstOrDefault(u => u.Id == newUser.Id);

            if (user == null)
            {
                return false;
            }

            Users[Users.IndexOf(user)] = newUser;

            return true;
        }

        public bool DeleteUser(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return false;
            }

            Users.Remove(user);

            return true;
        }

        public IEnumerable<User> GetAll()
        {
            return Users;
        }

        public User GetByEmail(string email)
        {
            return Users.FirstOrDefault(u => u.Email == email);
        }

        public User GetUserById(int id)
        {
            return Users.FirstOrDefault(u => u.Id == id);
        }

        public User GetCurrentUser()
        {
            return CurUser;
        }
    }
}
