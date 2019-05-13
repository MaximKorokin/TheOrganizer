using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheOrganizer.Model
{
    public class Calendar
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int OwnerId { get; set; }
        public bool IsDisplayed { get; set; }

        public User User { get; set; }
        public List<Event> Events { get; set; }
    }
}
