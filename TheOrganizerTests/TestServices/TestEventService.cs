using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TheOrganizer.Controllers;
using TheOrganizer.Model;
using TheOrganizer.Services;

namespace TheOrganizerTests.TestServices
{
    internal class TestEventService : IEventService
    {
        private List<User> Users { get; set; } = new List<User>
        {
            new User
            {
                Id = 1,
                Name = "Name1",
                Email = "Email1",
                Password = "Password1",
            },
            new User
            {
                Id = 2,
                Name = "Name2",
                Email = "Email2",
                Password = "Password2",
            },
            new User
            {
                Id = 3,
                Name = "Name3",
                Email = "Email3",
                Password = "Password3",
            },
        };
        private List<Event> Events { get; set; } = new List<Event>
        {
            new Event
            {
                Id = 1,
                Title = "Title1",
                Description = "Description1",
                CalendarId = 1,
            },
            new Event
            {
                Id = 2,
                Title = "Title2",
                Description = "Description2",
                CalendarId = 1,
            },
            new Event
            {
                Id = 3,
                Title = "Title3",
                Description = "Description3",
                CalendarId = 2,
            },
        };

        private EventsController _controller;

        public EventsController Controller {
            get
            {
                return _controller;
            }
            set
            {
                _controller = value;
                Authenticate();
            }
        }



        public bool AddEvent(Event e, int userId)
        {
            if (e == null)
                return false;

            try
            {
                Events.Add(e);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool EditEvent(Event e, int userId)
        {
            var ev = Events.FirstOrDefault(eve => eve.Id == e.Id);

            if (ev != null)
                return true;
            return false;
        }

        public Event GetEvent(int id, int userId)
        {
            return Events.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Event> GetEvents(int calendarId, int userId)
        {
            return Events.Where(e => e.CalendarId == calendarId).ToList();
        }

        public bool RemoveEvent(int id, int userId)
        {
            var ev = Events.FirstOrDefault(eve => eve.Id == id);

            if (ev != null)
                return true;
            return false;
        }





        private readonly User userForAuth = new User
        {
            Id = 1,
            Name = "Name1",
            Email = "Email1",
            Password = "Password1",
        };

        private void Authenticate()
        {
            var user = Users.FirstOrDefault(x => x.Email == userForAuth.Email && x.Password == userForAuth.Password);

            Controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "1")
                    }, "someAuthTypeName")),
                }
            };
        }
    }
}
