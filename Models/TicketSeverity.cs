using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DebugToolCSharp.Models
{
    public class TicketSeverity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}