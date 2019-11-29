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
            return View("Index", new RoleManagement() { Roles = Queries.GetAllRoles() });
        }

        [HttpGet]
        public ActionResult AddRole()
        {   
            Security.VerifyLoginStatus((int)PagesEnum.Pages.RoleManagement);
            return View("AddRole", new RoleManagement() { Success = true, Message = string.Empty, Roles = Queries.GetAllRoles() });
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
            mRoleManagement.PreviousRole = mRoles.Role;

            return View("EditRole", mRoleManagement);
        }

        [HttpPost]
        public ActionResult EditRole(RoleManagement roleManagement)
        {
            var mRoleManagement = new RoleManagement();
     
            mRoleManagement.Id = roleManagement.Id;
            mRoleManagement.Success = true;
            
            if (string.IsNullOrEmpty(roleManagement.Role))
            {
                mRoleManagement.Success = false;
                mRoleManagement.Message = "Role is empty";
                return View("EditRole", mRoleManagement);
            }

            mRoleManagement.Message = Queries.UpdateRoleById(roleManagement);
            mRoleManagement.Success = false;
            
            if (string.IsNullOrEmpty(mRoleManagement.Message))
            {
                mRoleManagement.Success = true;
                mRoleManagement.Message = "Role name updated";
            }

            var mRoles = Queries.GetRoleById(roleManagement.Id);
            
            mRoleManagement.Role = mRoles.Role;
            mRoleManagement.PreviousRole = mRoles.Role;

            return View("EditRole", mRoleManagement);
        }
    }
}