using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using TheOrganizer.Controllers;
using TheOrganizer.Entities;
using TheOrganizer.Helpers;
using TheOrganizer.Model;
using TheOrganizer.Services;
using TheOrganizerTests.TestServices;
using Xunit;

namespace TheOrganizerTests.ControllersTests
{
    public class UsersControllerTests
    {
        private readonly IUserService _userService;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _userService = new TestUserService();
            _controller = new UsersController(_userService);
            ((TestUserService)_userService).Controller = _controller;
        }

        [Fact]
        public void Authenticate()
        {
            var user = new User()
            {
                Email = "Email1",
                Password = "Password1",
            };

            var result = _controller.Authenticate(user) as ObjectResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
            Assert.True((result.Value as User).Name == "Name1", "Name is not correct");
        }

        [Fact]
        public void GetAll()
        {
            var result = _controller.GetAll() as ObjectResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
            Assert.True((result.Value as List<User>).Count == 3, "List count is not 3");
            Assert.True((result.Value as List<User>)[0].Name == "Name1", "Incorrect name of user at index 0");
        }

        [Fact]
        public void GetUser()
        {
            var result = _controller.GetUser("Email2") as ObjectResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
            Assert.True((result.Value as User).Name == "Name2", "Incorrect name of user");
            Assert.True((result.Value as User).Password == "Password2", "Incorrect password of user");
        }

        [Fact]
        public void AddUser()
        {
            var user = new User
            {
                Name = "Name",
                Email = "Email",
                Password = "Password",
            };

            var result = _controller.AddUser(user) as ObjectResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
            Assert.True((result.Value as UserEntity).Name == "Name", "Incorrect Name of user");
        }

        [Fact]
        public void GetCurrentUser()
        {
            var user = new User
            {
                Email = "Email1",
                Password = "Password1",
            };

            _controller.Authenticate(user);

            var result = _controller.GetCurrentUser() as ObjectResult;

            Assert.True(result != null, "result is null");
            Assert.True((result.Value as User).Name == "Name1", "Incorrect name of user");
            Assert.True((result.Value as User).Password == "Password1", "Incorrect password of user");
        }

        [Fact]
        public void ChangeCurrentUser()
        {
            var user = new User
            {
                Id = 1,
                Email = "Email1",
                Password = "Password1",
            };

            _controller.Authenticate(user);

            user.Name = "newName";
            user.Password = "newPass";

            var result = _controller.ChangeCurrentUser(user) as ObjectResult;
            
            Assert.True(result != null, "result is null");
            Assert.True((result.Value as User).Name == "newName", "Incorrect name of user");
            Assert.True((result.Value as User).Password == "newPass", "Incorrect password of user");
        }

        [Fact]
        public void DeleteCurrentUser()
        {
            var user = new User
            {
                Id = 1,
                Email = "Email1",
                Password = "Password1",
            };

            _controller.Authenticate(user);

            var result = _controller.DeleteCurrentUser();

            var deletedUser = _userService.GetUserById(user.Id);

            Assert.True(result != null, "result is null");
            Assert.True(deletedUser == null, "deletedUser is not null (user has not been deleted)");
        }
    }
}
