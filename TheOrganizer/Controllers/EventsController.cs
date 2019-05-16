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
            int.TryParse(User.Identity.Name, out int userId);
            if (_eventService.AddEvent(e, userId))
                return Ok(e);
            return BadRequest("There is something wrong with event info");
        }

        [HttpPut("edit")]
        public IActionResult EditEvent([FromBody] Event e)
        {
            int.TryParse(User.Identity.Name, out int userId);
            if (_eventService.EditEvent(e, userId))
                return Ok();
            return BadRequest("There is something wrong with event info");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult RemoveEvent([FromRoute] int id)
        {
            int.TryParse(User.Identity.Name, out int userId);
            if (_eventService.RemoveEvent(id, userId))
                return Ok();
            return NotFound("Event not found");
        }

        [HttpGet("getAll/{calendarId}")]
        public IActionResult GetEvents([FromRoute] int calendarId)
        {
            int.TryParse(User.Identity.Name, out int userId);
            var events = _eventService.GetEvents(calendarId, userId);
            if (events == null)
                return NotFound("Events not found");
            foreach (var e in events)
                ClearEvent(e);
            return Ok(events);
        }
        
        [HttpGet("get/{eventId}")]
        public IActionResult GetEvent([FromRoute] int eventId)
        {
            int.TryParse(User.Identity.Name, out int userId);
            var e = _eventService.GetEvent(eventId, userId);
            if (e != null)
            {
                ClearEvent(e);
                return Ok(e);
            }
            return NotFound("Event not found");
        }

        private Event ClearEvent(Event e)
        {
            e.Calendar = null;
            return e;
        }
    }
}
