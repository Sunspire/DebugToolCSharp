﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DebugToolCSharp.Models
{
    public class RoleManagement
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string PreviousRole { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Roles> Roles { get; set; }
    }
}