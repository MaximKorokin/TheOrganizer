using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheOrganizer.Entities
{
    public class ErrorRequest
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public Guid TraceId { get; set; }
    }
}
