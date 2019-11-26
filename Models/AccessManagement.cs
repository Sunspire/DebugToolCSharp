using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DebugToolCSharp.Models
{
    public class AccessManagement
    {
        public Roles Role { get; set; }
        public Users User { get; set; }
        public string[] PageIds { get; set; }
        public int PageId { get; set; }
        public string PageName { get; set; }
        public List<Pages> ListPages {get; set;}
        public List<Roles> ListRoles { get; set; }

    }
}