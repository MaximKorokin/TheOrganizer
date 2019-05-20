using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheOrganizer.Model
{
    public class Todo
    {
        public int Id { get; set; }
        public int TodoListId { get; set; }
        [MaxLength(25)]
        [MinLength(1)]
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }

        [JsonIgnore]
        public TodoList TodoList { get; set; }
    }
}
