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
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("get")]
        public IActionResult GetUser([FromQuery(Name = "email")] string email)
        {
            var user = _userService.GetByEmail(email);
            
            if (user == null)
                return NotFound();

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
            return NotFound();
        }
    }
}
