using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.Web.Mvc;
using DebugToolCSharp.Models;
//using System.Data.SQLite;
using DebugToolCSharp.Classes;
using System.Configuration;


namespace DebugToolCSharp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View("Index", new LoginModel() { Sucess = true });
        }

        [HttpPost]
        public ActionResult Index(LoginModel loginModel)
        {
            var mLogin = new LoginModel();
            mLogin.Sucess = true;
            if (!Queries.GetLogin(loginModel.Login, loginModel.Password)) 
            {
                mLogin.Sucess = false;
                return View("Index", mLogin);
            }
            CreateLoginSession(loginModel);
            var redirectCookie = Request.Cookies["redirectCookie"];
            if (redirectCookie != null && !string.IsNullOrEmpty(redirectCookie["redirect_path"]))
            {
                var rp = redirectCookie["redirect_path"];
                redirectCookie["redirect_path"] = string.Empty;
                redirectCookie.Expires = DateTime.Now.AddMinutes(-1);
                Response.Cookies.Add(redirectCookie);
                return RedirectToAction(".." + rp);
            }           
            return RedirectToAction("../Home");
        }

        public ActionResult LogoutUser()
        {
            Session.Abandon();
            return RedirectToAction("../Home");
        }

        public void CreateLoginSession(LoginModel loginModel)
        {
            LoginModel userDetails = Queries.GetUserDetails(loginModel.Login, loginModel.Password);
            userDetails.Sucess = true;
            Session[ConfigurationManager.AppSettings["LoginSessionName"].ToString()] = userDetails;
        }
    }
}
