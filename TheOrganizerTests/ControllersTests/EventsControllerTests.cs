using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using TheOrganizer.Controllers;
using TheOrganizer.Entities;
using TheOrganizer.Model;
using TheOrganizer.Services;
using TheOrganizerTests.TestServices;
using Xunit;

namespace TheOrganizerTests.ControllersTests
{
    public class EventsControllerTests
    {
        private readonly IEventService _eventService;
        private readonly EventsController _controller;

        public EventsControllerTests()
        {
            _eventService = new TestEventService();
            _controller = new EventsController(_eventService);
            ((TestEventService)_eventService).Controller = _controller;
        }

        [Fact]
        public void AddEvent()
        {
            var ev = new Event()
            {
                Description = "Description",
                Title = "Title",
            };

            var result = _controller.AddEvent(ev) as OkResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
        }

        [Fact]
        public void EditEvent()
        {
            var ev = new Event()
            {
                Id = 1,
                Description = "NewDescription",
                Title = "NewTitle",
            };

            var result = _controller.EditEvent(ev) as OkResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
        }

        [Fact]
        public void RemoveEvent()
        {
            var ev = new Event()
            {
                Id = 1,
                Description = "NewDescription",
                Title = "NewTitle",
            };

            var result = _controller.RemoveEvent(ev.Id) as OkResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
        }

        [Fact]
        public void GetEvents()
        {
            var result = _controller.GetEvents(1) as ObjectResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
            Assert.True(((List<Event>)result.Value).Count == 2, "Wrong events quantity");
        }

        [Fact]
        public void GetEvent()
        {
            var result = _controller.GetEvent(1) as ObjectResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
            Assert.True(((Event)result.Value).Description == "Description1", "Wrong event Description");
        }
    }
}
