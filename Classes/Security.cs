using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DebugToolCSharp.Classes
{
    public class Security
    {
        public static System.Web.WebPages.HelperResult VerifyLoginStatus()
        {
            var loginSessionName = ConfigurationManager.AppSettings["LoginSessionName"].ToString();
            if (HttpContext.Current.Session[loginSessionName] is null) 
            {
                var redirectCookie = HttpContext.Current.Request.Cookies["redirectCookie"];

                if (redirectCookie == null)
                {
                    redirectCookie = new HttpCookie("redirectCookie");
                }
                redirectCookie["redirect_path"] = HttpContext.Current.Request.Url.AbsolutePath;
                HttpContext.Current.Response.Cookies.Add(redirectCookie);

                //HttpContext.Current.Session[ConfigurationManager.AppSettings["LoginRedirectName"].ToString()] = HttpContext.Current.Request.Url.AbsolutePath;
                HttpContext.Current.Response.Redirect("/Login");
            }
            return null;
        }
    }
}