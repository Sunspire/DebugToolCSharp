using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DebugToolCSharp.Models
{
    public class LoginModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Sucess { get; set; }
        public int RoleId { get; set; }
    }
}