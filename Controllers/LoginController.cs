using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DebugToolCSharp.Models;
using System.Data.SQLite;
using DebugToolCSharp.Classes;

namespace DebugToolCSharp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            var mLogin = new LoginModel();
            mLogin.Sucess = true;
            return View("Index", mLogin);
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
            return RedirectToAction("../Home");
        }

        public void CreateLoginSession(LoginModel loginModel)
        {
            LoginModel userDetails = Queries.GetUserDetails(loginModel.Login, loginModel.Password);
            Session["userLogin"] = userDetails;
        }
    }
}
