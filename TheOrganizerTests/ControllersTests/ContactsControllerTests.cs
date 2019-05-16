using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using TheOrganizer.Controllers;
using TheOrganizer.Model;
using TheOrganizer.Services;
using TheOrganizerTests.TestServices;
using Xunit;

namespace TheOrganizerTests.ControllersTests
{
    public class ContactsControllerTests
    {
        private readonly IContactService _contactService;
        private readonly ContactsController _controller;

        public ContactsControllerTests()
        {
            _contactService = new TestContactService();
            _controller = new ContactsController(_contactService);
            ((TestContactService)_contactService).Controller = _controller;
        }

        [Fact]
        public void AddContact()
        {
            var contact = new Contact()
            {
                Name = "Name",
                Email = "Email",
            };

            var result = _controller.AddContact(contact) as ObjectResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
        }

        [Fact]
        public void EditContact()
        {
            var contact = new Contact()
            {
                Id = 2,
                Name = "Name",
                Email = "Email",
            };

            var result = _controller.EditContact(contact) as StatusCodeResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
        }

        [Fact]
        public void RemoveContact()
        {
            var contact = new Contact()
            {
                Id = 2,
            };

            var result = _controller.RemoveContact(contact.Id) as StatusCodeResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
        }

        [Fact]
        public void GetContacts()
        {
            var result = _controller.GetContacts() as ObjectResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
            Assert.True((result.Value as List<Contact>).Count == 2, "Caontacts quantity is not correct");
        }
    }
}
