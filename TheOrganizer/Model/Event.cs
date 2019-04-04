using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheOrganizer.Model
{
    public class Event
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public User User { get; set; }
    }
}
