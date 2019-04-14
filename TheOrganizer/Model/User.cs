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

        public List<Calendar> Calendars { get; set; }
        public List<TodoList> TodoLists { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Notebook> Notebooks { get; set; }
    }
}
