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
    public class TodoListsController : ControllerBase
    {
        private ITodoListService _todoListService;

        public TodoListsController(ITodoListService todoList)
        {
            _todoListService = todoList;
        }

        [HttpPost("add")]
        public IActionResult AddTodoList([FromBody] TodoList todoList)
        {
            int.TryParse(User.Identity.Name, out int userId);
            todoList.OwnerId = userId;
            if (_todoListService.AddTodoList(todoList) != null)
                return Ok(todoList);
            return BadRequest("There is something wrong with todoList info");
        }

        [HttpPut("edit")]
        public IActionResult EditTodoList([FromBody] TodoList todoList)
        {
            int.TryParse(User.Identity.Name, out int userId);
            todoList.OwnerId = userId;
            if (_todoListService.EditTodoList(todoList))
                return Ok();
            return BadRequest("There is something wrong with todoList info");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult RemoveTodoList([FromRoute] int id)
        {
            int.TryParse(User.Identity.Name, out int userId);
            if (_todoListService.RemoveTodoList(id, userId))
                return Ok();
            return NotFound("Todo list not found");
        }

        [HttpGet("get/{id}")]
        public IActionResult GetTodoList([FromRoute] int id)
        {
            int.TryParse(User.Identity.Name, out int userId);
            var todoList = _todoListService.GetTodoList(id, userId);
            if (todoList != null)
                return Ok(todoList);
            return NotFound("Todo list not found");
        }

        [HttpGet("get")]
        public IActionResult GetTodoLists()
        {
            int.TryParse(User.Identity.Name, out int userId);
            var todoLists = _todoListService.GetTodoLists(userId);
            if (todoLists != null)
                return Ok(todoLists);
            return BadRequest("Could not get todo lists");
        }
    }
}