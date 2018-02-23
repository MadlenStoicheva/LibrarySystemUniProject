using LibrarySystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystemProject.Filters
{
    public class AuthenticationFilter: ActionFilterAttribute
    {

        public bool RequireAdminRole { get; set; }

        public AuthenticationFilter()
        {
            RequireAdminRole = false;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            User user = (User)HttpContext.Current.Session["LoggedUser"];

            if (user == null)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
                return;
            }

            if (RequireAdminRole == true && user.isAdmin!=true)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
                return;
            }
        }
    }
}