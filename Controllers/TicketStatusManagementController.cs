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
            return View("AddTicketStatus", new TicketStatus() { Success = true, Message = string.Empty });
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
            if (string.IsNullOrEmpty(result))
            {
                mTicketStatus.Success = true;
                mTicketStatus.Message = "Ticket status added";
            }
            else
            {
                mTicketStatus.Success = false;
                mTicketStatus.Message = result;
            }

            return View("AddTicketStatus", mTicketStatus);
        }

        [HttpGet]
        public ActionResult AddTicketSeverity()
        {
            return View("AddTicketSeverity", new TicketSeverity() { Success = true, Message = string.Empty });
        }

        [HttpPost]
        public ActionResult AddTicketSeverity(TicketSeverity ticketSeverity)
        {
            var mTicketSeverity = new TicketSeverity();

            if (string.IsNullOrEmpty(ticketSeverity.Description))
            {
                mTicketSeverity.Success = false;
                mTicketSeverity.Message = "Fill in all of the fields";
                return View("AddTicketStatus", mTicketSeverity);
            }

            var result = Queries.AddTicketSeverity(ticketSeverity);
            if (string.IsNullOrEmpty(result))
            {
                mTicketSeverity.Success = true;
                mTicketSeverity.Message = "Ticket severity added";
            }
            else
            {
                mTicketSeverity.Success = false;
                mTicketSeverity.Message = result;
            }

            return View("AddTicketSeverity", mTicketSeverity);
        }
    }
}