using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheOrganizer.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<Event> Events { get; set; }
        public List<Task> Tasks { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Note> Notes { get; set; }
    }
}
