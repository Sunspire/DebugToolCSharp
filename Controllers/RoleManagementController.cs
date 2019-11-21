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
            var mRoleManagement = new RoleManagement();
            mRoleManagement.Roles = Queries.GetAllRoles();
            return View("Index", mRoleManagement);
        }

        [HttpGet]
        public ActionResult AddRole()
        {
            List<int> roleIds = new List<int> { 1 };
            Security.VerifyLoginStatus(roleIds);

            var mRoleManagement = new RoleManagement();
            mRoleManagement.Success = true;
            mRoleManagement.Message = string.Empty;
            mRoleManagement.Roles = Queries.GetAllRoles();
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

        [HttpGet]
        public ActionResult EditRole(int id)
        {
            var mRoles = new Roles();
            var mRoleManagement = new RoleManagement();

            mRoles = Queries.GetRoleById(id);
            mRoleManagement.Id = mRoles.Id;
            mRoleManagement.Role = mRoles.Role;

            return View("EditRole", mRoleManagement);
        }

        [HttpPost]
        public ActionResult EditRole(RoleManagement roleManagement)
        {
            var mRoleManagement = new RoleManagement();
            mRoleManagement.Success = true;
            if (string.IsNullOrEmpty(roleManagement.Role))
            {
                mRoleManagement.Success = false;
                mRoleManagement.Message = "Role is empty";
                mRoleManagement.Id = roleManagement.Id;
                return View("EditRole", mRoleManagement);
            }

            return View();
        }
    }
}