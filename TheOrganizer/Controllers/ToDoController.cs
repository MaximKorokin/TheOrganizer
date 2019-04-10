using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheOrganizer.Model;
using TheOrganizer.Services;
using ToDo = TheOrganizer.Model.Task;

namespace TheOrganizer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private IToDoService _toDoService;
        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        [HttpPost("add")]
        public IActionResult AddToDo([FromBody] ToDo task)
        {
            int.TryParse(User.Identity.Name, out int userId);
            task.OwnerId = userId;
            if (_toDoService.AddToDo(task))
                return Ok();
            return BadRequest("There is something wrong with ToDo info");
        }

        [HttpPut("edit")]
        public IActionResult EditToDo([FromBody] ToDo task)
        {
            int.TryParse(User.Identity.Name, out int userId);
            task.OwnerId = userId;
            if (_toDoService.EditToDo(task))
                return Ok();
            return BadRequest("There is something wrong with ToDo info");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteToDo([FromRoute] int id)
        {
            int.TryParse(User.Identity.Name, out int userId);
            if (_toDoService.RemoveToDo(id, userId))
                return Ok();
            return NotFound();
        }

        [HttpGet]
        public IActionResult GetToDos()
        {
            int.TryParse(User.Identity.Name, out int userId);
            var toDos = _toDoService.GetToDos(userId);
            return Ok(toDos);
        }

        [HttpGet("get/{id}")]
        public IActionResult GetToDo([FromRoute] int id)
        {
            int.TryParse(User.Identity.Name, out int userId);
            var res = _toDoService.GetToDo(id, userId);
            if (res != null)
                return Ok(res);
            return NotFound();
        }

        [HttpGet("check/{id}")]
        public IActionResult IsToDoDone([FromRoute] int id)
        {
            int.TryParse(User.Identity.Name, out int userId);
            var res = _toDoService.GetToDo(id, userId);
            if (res != null)
            {
                return Ok(res.IsDone);
            }
            else return StatusCode(404);
        }
    }
}