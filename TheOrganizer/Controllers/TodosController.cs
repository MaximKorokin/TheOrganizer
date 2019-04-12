using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheOrganizer.Model;
using TheOrganizer.Services;

namespace TheOrganizer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private ITodoService _toDoService;
        public TodosController(ITodoService toDoService)
        {
            _toDoService = toDoService;
        }

        [HttpPost("add")]
        public IActionResult AddToDo([FromBody] Todo task)
        {
            //int.TryParse(User.Identity.Name, out int userId);
            //task.OwnerId = userId;
            //if (_toDoService.AddTodo(task))
            //    return Ok();
            return BadRequest("There is something wrong with ToDo info");
        }

        [HttpPut("edit")]
        public IActionResult EditToDo([FromBody] Todo task)
        {
            //int.TryParse(User.Identity.Name, out int userId);
            //task.OwnerId = userId;
            //if (_toDoService.EditTodo(task))
            //    return Ok();
            return BadRequest("There is something wrong with ToDo info");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteToDo([FromRoute] int id)
        {
            int.TryParse(User.Identity.Name, out int userId);
            if (_toDoService.RemoveTodo(id, userId))
                return Ok();
            return NotFound();
        }

        [HttpGet]
        public IActionResult GetToDos()
        {
            int.TryParse(User.Identity.Name, out int userId);
            var toDos = _toDoService.GetTodos(userId);
            return Ok(toDos);
        }

        [HttpGet("get/{id}")]
        public IActionResult GetToDo([FromRoute] int id)
        {
            int.TryParse(User.Identity.Name, out int userId);
            var res = _toDoService.GetTodo(id, userId);
            if (res != null)
                return Ok(res);
            return NotFound();
        }

        [HttpGet("check/{id}")]
        public IActionResult IsToDoDone([FromRoute] int id)
        {
            int.TryParse(User.Identity.Name, out int userId);
            var res = _toDoService.GetTodo(id, userId);
            if (res != null)
            {
                return Ok(res.IsDone);
            }
            else return StatusCode(404);
        }
    }
}