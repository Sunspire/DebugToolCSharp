using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DebugToolCSharp.Models;
using DebugToolCSharp.Classes;

namespace DebugToolCSharp.Controllers
{
    public class UserManagementController : Controller
    {
        // GET: UserManagement
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            var mUserManagement = new UserManagement();

            mUserManagement.Roles = Queries.GetAllRoles();
            
            return View("AddUser", mUserManagement);
        }

        [HttpPost]
        public ActionResult AddUser(UserManagement userManagement)
        {
            var mUserManagement = new UserManagement();

            return View();
        }
    }
}