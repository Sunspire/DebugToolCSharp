using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DebugToolCSharp.Models
{
    public class Pages
    {
        public int Id { get; set; }
        public string PageName { get; set; }
        public List<Roles> Roles { get; set; }
    }
}