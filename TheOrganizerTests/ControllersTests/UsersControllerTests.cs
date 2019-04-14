using System;
using System.Collections.Generic;
using System.Text;
using TheOrganizer.Controllers;
using TheOrganizer.Model;
using TheOrganizer.Services;
using Xunit;

namespace TheOrganizerTests.ControllersTests
{
    public class UsersControllerTests
    {
        private readonly IUserService _userService;

        public UsersControllerTests(IUserService userService)
        {
            _userService = userService;
        }

        [Fact]
        public void Authenticate()
        {
            var user = new User
            {
                Email = "user@example.com",
                Password = "string",
            };
        }
    }
}
