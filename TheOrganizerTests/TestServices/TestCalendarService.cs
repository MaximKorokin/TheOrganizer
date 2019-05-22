using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using TheOrganizer.Controllers;
using TheOrganizer.Model;
using TheOrganizer.Services;

namespace TheOrganizerTests.TestServices
{
    internal class TestCalendarService : ICalendarService
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
        private List<Calendar> Calendars { get; set; } = new List<Calendar>
        {
            new Calendar
            {
                Id = 1,
                Title = "Title1",
                OwnerId = 1,
            },
            new Calendar
            {
                Id = 2,
                Title = "Title1",
                OwnerId = 1,
            },
            new Calendar
            {
                Id = 3,
                Title = "Title1",
                OwnerId = 2,
            },
        };

        private CalendarsController _controller;

        public CalendarsController Controller
        {
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






        public Calendar AddCalendar(Calendar calendar)
        {
            if (calendar == null)
                return null;

            try
            {
                Calendars.Add(calendar);
            }
            catch
            {
                return null;
            }

            return calendar;
        }

        public bool EditCalendar(Calendar calendar)
        {
            if (calendar == null)
                return false;

            var cur = Calendars.FirstOrDefault(c => c.Id == calendar.Id);

            if (cur == null)
                return false;

            return true;
        }

        public Calendar GetCalendar(int calendarId, int ownerId)
        {
            return Calendars.FirstOrDefault(c => c.OwnerId == ownerId && c.Id == calendarId);
        }

        public IEnumerable<Calendar> GetCalendars(int ownerId)
        {
            return Calendars.Where(c => c.OwnerId == ownerId).ToList();
        }

        public bool RemoveCalendar(int calendarId, int ownerId)
        {
            var deleted = Calendars.Remove(Calendars.FirstOrDefault(c => c.Id == calendarId));

            return deleted;
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
