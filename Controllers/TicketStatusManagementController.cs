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
            return View("Index", new TicketStatusManagement() { TicketStatus = Queries.GetAllTicketStatus() });
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
        public ActionResult EditTicketStatus(int id)
        {
            var mTicketStatus = new TicketStatus();
            var mTicketStatusManagement = new TicketStatusManagement();

            mTicketStatus = Queries.GetTicketStatusById(id);
            mTicketStatusManagement.Id = id;
            mTicketStatusManagement.Description = mTicketStatus.Description;
            mTicketStatusManagement.Message = string.Empty;

            return View("EditTicketStatus", mTicketStatusManagement);
        }

        [HttpPost]
        public ActionResult EditTicketStatus(TicketStatusManagement ticketStatusManagement)
        {
            var mTicketStatusManagement = new TicketStatusManagement();

            mTicketStatusManagement.Id = ticketStatusManagement.Id;
            mTicketStatusManagement.Success = true;

            if (string.IsNullOrEmpty(ticketStatusManagement.Description)) 
            {
                mTicketStatusManagement.Success = false;
                mTicketStatusManagement.Message = "Description is empty";
                return View("EditTicketStatus", mTicketStatusManagement);
            }

            mTicketStatusManagement.Message = Queries.UpdateTicketStatusById(ticketStatusManagement);
            mTicketStatusManagement.Success = false;

            if (string.IsNullOrEmpty(mTicketStatusManagement.Message))
            {
                mTicketStatusManagement.Success = true;
                mTicketStatusManagement.Message = "Ticket Status description updated";
            }

            var mTicketStatus = Queries.GetTicketStatusById(ticketStatusManagement.Id);

            mTicketStatusManagement.Description = mTicketStatus.Description;

            return View("EditTicketStatus", mTicketStatusManagement);
        }

        [HttpGet]
        public ActionResult DeleteTicketStatus()
        {
            var mTicketStatusManagement = new TicketStatusManagement();

            mTicketStatusManagement.TicketStatus = Queries.GetAllTicketStatus();

            return View("DeleteTicketStatus", mTicketStatusManagement);
        }

        [HttpPost]
        public ActionResult DeleteTicketStatus(TicketStatusManagement ticketStatusManagement, string[] selectedStatus)
        {
            var mTicketStatusManagement = new TicketStatusManagement();

            mTicketStatusManagement.TicketStatus = Queries.GetAllTicketStatus();

            return View("DeleteTicketStatus", mTicketStatusManagement);
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