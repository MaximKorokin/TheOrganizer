using System;
using System.Collections.Generic;
using System.Text;
using TheOrganizer.Controllers;
using TheOrganizer.Services;
using TheOrganizerTests.TestServices;

namespace TheOrganizerTests.ControllersTests
{
    public class CalendarsControllerTests
    {
        private readonly ICalendarService _calendarService;
        private readonly CalendarsController _controller;

        public CalendarsControllerTests()
        {
            _calendarService = new TestCalendarService();
            _controller = new CalendarsController(_calendarService);
            ((TestCalendarService)_calendarService).Controller = _controller;
        }
    }
}
