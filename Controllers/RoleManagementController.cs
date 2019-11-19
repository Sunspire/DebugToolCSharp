using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DebugToolCSharp.Models;
using DebugToolCSharp.Classes;

namespace DebugToolCSharp.Controllers
{
    public class RoleManagementController : Controller
    {
        // GET: RoleManagement
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddRole()
        {
            var mRoleManagement = new RoleManagement();
            mRoleManagement.Success = true;
            mRoleManagement.Message = string.Empty;
            return View("AddRole", mRoleManagement);
        }

        [HttpPost]
        public ActionResult AddRole(RoleManagement roleManagement)
        {
            var mRoleManagement = new RoleManagement();
            mRoleManagement.Success = true;
            if (string.IsNullOrEmpty(roleManagement.Role))
            {
                mRoleManagement.Success = false;
                mRoleManagement.Message = "Role is empty";
                return View("AddRole", mRoleManagement);
            }

            var result = Queries.AddRole(roleManagement.Role);
            if (string.IsNullOrEmpty(result))
            {
                mRoleManagement.Success = true;
                mRoleManagement.Message = "Role added";
            }
            else
            {
                mRoleManagement.Success = false;
                mRoleManagement.Message = result;
            }
            return View("AddRole", mRoleManagement);
        }
    }
}