using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheOrganizer.Model
{
    public class Calendar
    {
        public int Id { get; set; }
        [StringLength(25)]
        public string Title { get; set; }
        public int OwnerId { get; set; }
        public bool IsDisplayed { get; set; }

        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public List<Event> Events { get; set; }
    }
}
