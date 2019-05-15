using Newtonsoft.Json;
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

        [JsonIgnore]
        public List<Calendar> Calendars { get; set; }
        [JsonIgnore]
        public List<TodoList> TodoLists { get; set; }
        [JsonIgnore]
        public List<Contact> Contacts { get; set; }
        [JsonIgnore]
        public List<Notebook> Notebooks { get; set; }
    }
}
