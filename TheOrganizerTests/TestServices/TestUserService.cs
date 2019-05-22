using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using TheOrganizer.Controllers;
using TheOrganizer.Entities;
using TheOrganizer.Helpers;
using TheOrganizer.Model;
using TheOrganizer.Services;

namespace TheOrganizerTests.TestServices
{
    internal class TestUserService : IUserService
    {
        private const string Secret = "Subscribe to Mumbo Jumbo!";

        public UsersController Controller { get; set; }

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
            if (user == null)
            {
                return false;
            }

            Users.Add(user);

            return true;
        }

        public UserEntity Authenticate(string email, string password)
        {
            var user = Users.FirstOrDefault(x => x.Email == email && x.Password == password);

            Controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "1")
                    }, "someAuthTypeName")),
                }
            };

            var userEntity = new UserEntity
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };

            return userEntity;
        }

        public bool EditUser(User newUser)
        {
            var user = Users.FirstOrDefault(u => u.Id == newUser.Id);

            if (user == null)
            {
                return false;
            }

            Users[Users.IndexOf(user)] = newUser;

            return true;
        }

        public bool RemoveUser(int id)
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
    }
}
