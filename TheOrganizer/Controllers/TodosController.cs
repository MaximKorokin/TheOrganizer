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
        private ITodoService _todoService;
        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpPost("add")]
        public IActionResult AddTodo([FromBody] Todo todo)
        {
            int.TryParse(User.Identity.Name, out int userId);
            if (_todoService.AddTodo(todo, userId))
                return Ok();
            return BadRequest("There is something wrong with ToDo info");
        }

        [HttpPut("edit")]
        public IActionResult EditTodo([FromBody] Todo todo)
        {
            int.TryParse(User.Identity.Name, out int userId);
            if (_todoService.EditTodo(todo, userId))
                return Ok();
            return BadRequest("There is something wrong with Todo info");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteToDo([FromRoute] int id)
        {
            int.TryParse(User.Identity.Name, out int userId);
            if (_todoService.RemoveTodo(id, userId))
                return Ok();
            return NotFound();
        }

        [HttpGet("getAll/{todoListId}")]
        public IActionResult GetTodos([FromRoute] int todoListId)
        {
            int.TryParse(User.Identity.Name, out int userId);
            var todos = _todoService.GetTodos(userId, todoListId);
            if (todos != null && todos.Count() > 0)
            {
                return Ok(todos);
            }
            return StatusCode(404);
        }

        [HttpGet("get/{id}")]
        public IActionResult GetTodo([FromRoute] int id)
        {
            int.TryParse(User.Identity.Name, out int userId);
            var res = _todoService.GetTodo(id, userId);
            if (res != null)
                return Ok(res);
            return NotFound();
        }

        [HttpGet("check/{id}")]
        public IActionResult IsTodoDone([FromRoute] int id)
        {
            int.TryParse(User.Identity.Name, out int userId);
            var res = _todoService.GetTodo(id, userId);
            if (res != null)
                return Ok(res.IsDone);
            return NotFound();
        }
    }
}