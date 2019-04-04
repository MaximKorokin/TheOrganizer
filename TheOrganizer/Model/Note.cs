using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheOrganizer.Model
{
    public class Note
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }

        public User User { get; set; }
    }
}
