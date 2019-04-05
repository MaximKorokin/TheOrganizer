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
    public class EventsController : ControllerBase
    {
        private IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("add")]
        public IActionResult AddEvent([FromBody] Event e)
        {
            if (e.OwnerId.ToString() != User.Identity.Name)
                return Unauthorized("No permission to add events for other users");
            if (_eventService.AddEvent(e))
                return Ok();
            return BadRequest("There is something wrong with event info");
        }

        [HttpPut("edit")]
        public IActionResult EditEvent([FromBody] Event e)
        {
            if (e.OwnerId.ToString() != User.Identity.Name)
                return Unauthorized("No permission to edit events of other users");
            if (_eventService.EditEvent(e))
                return Ok();
            return BadRequest("There is something wrong with event info");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult RemoveEvent([FromRoute] int id)
        {
            int.TryParse(User.Identity.Name, out int userId);
            if (_eventService.RemoveEvent(id, userId))
                return Ok();
            return NotFound();
        }

        [HttpGet("get")]
        public IActionResult GetEvents()
        {
            int.TryParse(User.Identity.Name, out int userId);
            var events = _eventService.GetEvents(userId);
            return Ok(events);
        }
        
        [HttpGet("get/{id}")]
        public IActionResult GetEvent([FromRoute] int id)
        {
            int.TryParse(User.Identity.Name, out int userId);
            var e = _eventService.GetEvent(id, userId);
            if (e != null)
                return Ok(e);
            return NotFound();
        }
    }
}