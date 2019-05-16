using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheOrganizer.Model;
using TheOrganizer.Services;

namespace TheOrganizer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private IContactService _contactService;
        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost("add")]
        public IActionResult AddContact([FromBody] Contact contact)
        {
            int.TryParse(User.Identity.Name, out int userId);
            contact.OwnerId = userId;
            if (_contactService.AddContact(contact))
                return Ok(contact);
            return BadRequest("There is something wrong with contact info");
        }

        [HttpPut("edit")]
        public IActionResult EditContact([FromBody] Contact contact)
        {
            int.TryParse(User.Identity.Name, out int userId);
            contact.OwnerId = userId;
            if (_contactService.EditContact(contact))
                return Ok();
            return BadRequest("There is something wrong with contact info");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult RemoveContact([FromRoute] int id)
        {
            int.TryParse(User.Identity.Name, out int userId);
            if (_contactService.RemoveContact(id, userId))
                return Ok();
            return NotFound("Contact not found");
        }

        [HttpGet]
        public IActionResult GetContacts()
        {
            int.TryParse(User.Identity.Name, out int userId);
            var contacts = _contactService.GetContacts(userId);
            if (contacts != null)
                return Ok(contacts);
            return BadRequest("Could not get contacts");
        }
    }
}
