using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DebugToolCSharp.Models;
using System.Data.SQLite;

namespace DebugToolCSharp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            

            var user = new Users();

            user.Name = "Fred";

            return View("Index", user);
        }

        [HttpPost]
        public ActionResult Index(Users user)
        {
            var login = user.Login;
            var pwd = user.Password;
            var dbObject = new Database();
            var q = "select count(1) as mycount from users where login=@login and password=@pwd";

            SQLiteCommand cmd = new SQLiteCommand(q, dbObject.Connection);
            dbObject.OpenConnection();
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@pwd", pwd);
            SQLiteDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    if (int.Parse(result["mycount"].ToString()) == 1) 
                    {
                        //login
                    }
                }
            }
            dbObject.CloseConnection();
            return View("Index");
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
