using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DebugToolCSharp.Models;
using DebugToolCSharp.Classes;

namespace DebugToolCSharp.Controllers
{
    public class TicketStatusManagementController : Controller
    {
        // GET: TicketStatusManagement
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddTicketStatus() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTicketStatus(TicketStatus ticketStatus)
        {
            var mTicketStatus = new TicketStatus();

            if (string.IsNullOrEmpty(ticketStatus.Description))
            {
                mTicketStatus.Success = false;
                mTicketStatus.Message = "Fill in all of the fields";
                return View("AddTicketStatus", mTicketStatus);
            }

            var result = Queries.AddTicketStatus(ticketStatus);
            /*if (string.IsNullOrEmpty(result))
            {
                mUserManagement.Success = true;
                mUserManagement.Message = "User added";
            }
            else
            {
                mUserManagement.Success = false;
                mUserManagement.Message = result;
            }*/

            return View();
        }
    }
}