using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheOrganizer.Model
{
    public class Task
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }

        public User User { get; set; }
    }
}
