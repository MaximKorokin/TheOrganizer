using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TheOrganizer.Controllers;
using TheOrganizer.Model;
using TheOrganizer.Services;
using TheOrganizerTests.TestServices;
using Xunit;

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

        [Fact]
        public void AddCalendar()
        {
            var calendar = new Calendar()
            {
                Title = "Title123",
                OwnerId = 1
            };

            var result = _controller.AddCalendar(calendar) as ObjectResult;

            Assert.True((Calendar)result.Value != null, "result is false");
            Assert.True(((Calendar)result.Value).Title == "Title123", "result is not Title123");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
        }

        [Fact]
        public void EditCalendar()
        {
            var calendar = new Calendar()
            {
                Id = 1,
                Title = "Title1"
            };

            var result = _controller.EditCalendar(calendar) as ObjectResult;

            Assert.True(result.Value != null, "result is null");
            Assert.True(((Calendar)result.Value).Title == "Title1", "result is not Title1");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
        }

        [Fact]
        public void RemoveCalendar()
        {
            var calendar = new Calendar()
            {
                Id = 1
            };

            var result = _controller.RemoveCalendar(calendar.Id) as StatusCodeResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
        }

        [Fact]
        public void GetCalendars()
        {
            var result = _controller.GetCalendars() as ObjectResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
            Assert.True((result.Value as List<Calendar>).Count == 2, "Calendars quantity is not correct");
        }

        [Fact]
        public void GetCalendar()
        {
            var calendarId = 1;

            var result = _controller.GetCalendar(calendarId) as ObjectResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
            Assert.True((result.Value as Calendar).Title == "Title1", "Seleted calendar's title is not correct");
        }
    }
}
