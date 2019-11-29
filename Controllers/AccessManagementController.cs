using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DebugToolCSharp.Models;
using DebugToolCSharp.Classes;

namespace DebugToolCSharp.Controllers
{
    public class AccessManagementController : Controller
    {
        // GET: AccessManagement
        public ActionResult Index()
        {
            return View("Index", new AccessManagement() { ListPages = Queries.GetAllPages() });
        }

        [HttpGet]
        public ActionResult EditAccess(int id) 
        {
            var mAccessManagement = new AccessManagement();
            var listPages = new List<Pages>();

            listPages.Add(Queries.GetPageById(id));
            mAccessManagement.ListPages = listPages;
            mAccessManagement.ListRoles = Queries.GetAllRoles();
            mAccessManagement.PageId = id;

            return View("EditAccess", mAccessManagement);
        }

        [HttpPost]
        public ActionResult EditAccess(AccessManagement accessManagement, string[] selectedRoles)
        {
            var resutl = Queries.UpdatePageRoles(accessManagement.PageId, Array.ConvertAll(selectedRoles, int.Parse));

            var mAccessManagement = new AccessManagement();
            var listPages = new List<Pages>();

            listPages.Add(Queries.GetPageById(accessManagement.PageId));
            mAccessManagement.ListPages = listPages;
            mAccessManagement.ListRoles = Queries.GetAllRoles();

            return View("EditAccess", mAccessManagement);
        }
    }
}