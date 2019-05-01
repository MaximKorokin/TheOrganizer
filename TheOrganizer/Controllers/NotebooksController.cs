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
    public class NotebooksController : ControllerBase
    {
        private INotebookService _notebookService;
        public NotebooksController(INotebookService notebookService)
        {
            _notebookService = notebookService;
        }

        [HttpPost("add")]
        public IActionResult AddNotebook([FromBody] Notebook notebook)
        {
            int.TryParse(User.Identity.Name, out int userId);
            notebook.OwnerId = userId;
            if (_notebookService.AddNotebook(notebook) != null)
                return Ok(notebook);
            return BadRequest("There is something wrong with notebook info");
        }

        [HttpPut("edit")]
        public IActionResult EditNotebook([FromBody] Notebook notebook)
        {
            int.TryParse(User.Identity.Name, out int userId);
            notebook.OwnerId = userId;
            if (_notebookService.EditNotebook(notebook))
                return Ok();
            return BadRequest("There is something wrong with notebook info");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult RemoveNotebook([FromRoute] int id)
        {
            int.TryParse(User.Identity.Name, out int userId);
            if (_notebookService.RemoveNotebookr(id, userId))
                return Ok();
            return NotFound("Notebook not found");
        }

        [HttpGet("get/{id}")]
        public IActionResult GetNotebook([FromRoute] int id)
        {
            int.TryParse(User.Identity.Name, out int userId);
            var notebook = _notebookService.GetNotebook(id, userId);
            if (notebook != null)
                return Ok();
            return NotFound("Notebook not found");
        }

        [HttpGet("get")]
        public IActionResult GetNotebooks()
        {
            int.TryParse(User.Identity.Name, out int userId);
            var notebooks = _notebookService.GetNotebooks(userId);
            if (notebooks != null && notebooks.Count() > 0)
                return Ok(notebooks);
            return BadRequest("Could not get notebooks");
        }
    }
}