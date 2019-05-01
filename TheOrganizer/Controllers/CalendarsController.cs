using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheOrganizer.Model;
using TheOrganizer.Services;

namespace TheOrganizer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarsController : Controller
    {
        private ICalendarService _eventService;

        public CalendarsController(ICalendarService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("add")]
        public IActionResult AddCalendar([FromBody] Calendar calendar)
        {
            int.TryParse(User.Identity.Name, out int userId);
            calendar.OwnerId = userId;
            if (_eventService.AddCalendar(calendar) != null)
                return Ok(calendar);
            return BadRequest("There is something wrong with calendar info");
        }

        [HttpPut("edit")]
        public IActionResult EditCalendar([FromBody] Calendar calendar)
        {
            int.TryParse(User.Identity.Name, out int userId);
            calendar.OwnerId = userId;
            if (_eventService.EditCalendar(calendar))
                return Ok();
            return BadRequest("There is something wrong with calendar info");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult RemoveCalendar([FromRoute] int id)
        {
            int.TryParse(User.Identity.Name, out int userId);
            if (_eventService.RemoveCalendar(id, userId))
                return Ok();
            return NotFound("Calendar not found");
        }

        [HttpGet("get/{id}")]
        public IActionResult GetCalendar([FromRoute] int id)
        {
            int.TryParse(User.Identity.Name, out int userId);
            var calendar = _eventService.GetCalendar(id, userId);
            if (calendar != null)
                return Ok(calendar);
            return NotFound("Calendar not found");
        }

        [HttpGet("get")]
        public IActionResult GetCalendars()
        {
            int.TryParse(User.Identity.Name, out int userId);
            var calendars = _eventService.GetCalendars(userId);
            if (calendars != null && calendars.Count() > 0)
                return Ok(calendars);
            return BadRequest("Could not get calendars");
        }
    }
}
