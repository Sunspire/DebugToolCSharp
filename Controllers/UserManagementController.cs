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

            if (string.IsNullOrEmpty(userManagement.UserName) || string.IsNullOrEmpty(userManagement.UserLogin) || string.IsNullOrEmpty(userManagement.UserPassword))
            {
                mUserManagement.Success = false;
                mUserManagement.Message = "Fill in all of the fields";
                return View("AddUser", mUserManagement);
            }

            var result = Queries.AddUser(userManagement);
            if (string.IsNullOrEmpty(result))
            {
                mUserManagement.Success = true;
                mUserManagement.Message = "User added";
            }
            else
            {
                mUserManagement.Success = false;
                mUserManagement.Message = result;
            }
            
            mUserManagement.Roles = Queries.GetAllRoles();

            return View("AddUser", mUserManagement);
        }
    }
}