using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DebugToolCSharp.Models
{
    public class TicketStatusManagement
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string PreviousDescription { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<TicketStatus> TicketStatus { get; set; }
    }
}