using Data.Entity.Repositories;
using LibrarySystem.Entity;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibrarySystemProject.Views.Login;
using LibrarySystemProject.Filters;

namespace LibrarySystemProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            UserRepository repo = new UserRepository();
            List<User> items = repo.GetAll(i => i.username == model.Username && i.password == model.Password);
            Session["LoggedUser"] = items.Count > 0 ? items[0] : null;

            if (items.Count <= 0)
                this.ModelState.AddModelError("failedLogin", "Login failed!");

            if (!ModelState.IsValid)
                return View(model);

            return RedirectToAction("Index", "Readers");
        }

       public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session["LoggedUser"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}