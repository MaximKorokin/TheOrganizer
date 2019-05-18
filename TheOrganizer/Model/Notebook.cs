using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheOrganizer.Model
{
    public class Notebook
    {
        public int Id { get; set; }
        [StringLength(25)]
        public string Title { get; set; }
        public int OwnerId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public List<Note> Notes { get; set; }
    }
}
