using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheOrganizer.Model
{
    public class Event
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        [Column(name: "StartTime")]
        public DateTime Start { get; set; }
        [Column(name: "EndTime")]
        public DateTime End { get; set; }
        
        [JsonIgnore]
        public Calendar Calendar { get; set; }
    }
}
