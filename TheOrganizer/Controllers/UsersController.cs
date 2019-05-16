using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheOrganizer.Entities;
using TheOrganizer.Model;
using TheOrganizer.Services;

namespace TheOrganizer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userService.Authenticate(userParam.Email, userParam.Password);

            if (user == null)
                return BadRequest("Username or password is incorrect");

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            if (users != null && users.Count() > 0)
                return Ok(users);
            return BadRequest("Could not get users");
        }

        [HttpGet("get")]
        public IActionResult GetUser([FromQuery(Name = "email")] string email)
        {
            var user = _userService.GetByEmail(email);

            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult AddUser([FromBody] User userParam)
        {
            if (!_userService.AddUser(userParam))
                return BadRequest("Something went wrong");

            var user = _userService.Authenticate(userParam.Email, userParam.Password);
            return Ok(user);
        }

        [HttpGet("current")]
        public IActionResult GetCurrentUser()
        {
            if (int.TryParse(User.Identity.Name, out int currentId))
                return Ok(_userService.GetUserById(currentId));
            return NotFound("User not found");
        }

        [HttpPut("current/change")]
        public IActionResult ChangeCurrentUser([FromBody] User user)
        {
            int id;
            if (!int.TryParse(User.Identity.Name, out id))
                return NotFound("User not found");
            user.Id = id;
            if (_userService.ChangeUser(user))
                return Ok(user);
            return BadRequest("Something is wrng with user data");
        }

        [HttpDelete("current/delete")]
        public IActionResult DeleteCurrentUser()
        {
            int id;
            if (!int.TryParse(User.Identity.Name, out id))
                return BadRequest("Can not delete");
            if (_userService.DeleteUser(id))
                return Ok();
            return NotFound("User not found");
        }
    }
}
