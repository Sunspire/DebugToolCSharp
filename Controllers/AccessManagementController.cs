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
            var mAccessManagement = new AccessManagement();

            mAccessManagement.ListPages = Queries.GetAllPages();
            
            return View("Index", mAccessManagement);
        }

        [HttpGet]
        public ActionResult EditAccess(int id) 
        {
            var mPages = new Pages();
            var mAccessManagement = new AccessManagement();
            var listPages = new List<Pages>();

            listPages.Add(Queries.GetPageById(id));
            mAccessManagement.ListPages = listPages;

            return View("EditAccess", mAccessManagement);
        }

        [HttpPost]
        public ActionResult EditAccess(AccessManagement accessManagement)
        {
            return View();
        }
    }
}