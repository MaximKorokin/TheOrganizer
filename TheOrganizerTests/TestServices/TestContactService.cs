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
    internal class TestContactService : IContactService
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
        private List<Contact> Contacts { get; set; } = new List<Contact>
        {
            new Contact
            {
                Id = 1,
                Name = "Name1",
                Email = "Email1",
                PhoneNumber = "Number1",
                OwnerId = 1,
            },
            new Contact
            {
                Id = 2,
                Name = "Name2",
                Email = "Email2",
                PhoneNumber = "Number2",
                OwnerId = 1,
            },
            new Contact
            {
                Id = 3,
                Name = "Name3",
                Email = "Email3",
                PhoneNumber = "Number3",
                OwnerId = 2,
            },
        };

        private ContactsController _controller;

        public ContactsController Controller
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




        public bool AddContact(Contact contact)
        {
            if (contact == null)
                return false;

            try
            {
                Contacts.Add(contact);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool EditContact(Contact contact)
        {
            if (contact == null)
                return false;

            var cur = Contacts.FirstOrDefault(c => c.Id == contact.Id);

            if (cur == null)
                return false;

            return true;
        }

        public bool RemoveContact(int contactId, int ownerId)
        {
            var deleted = Contacts.Remove(Contacts.FirstOrDefault(c => c.Id == contactId));

            return deleted;
        }

        public IEnumerable<Contact> GetContacts(int ownerId)
        {
            return Contacts.Where(c => c.OwnerId == ownerId).ToList();
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
