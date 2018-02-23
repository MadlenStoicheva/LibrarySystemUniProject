using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data.Entity;
using LibrarySystem.Entity;
using Data.Entity.Repositories;
using LibrarySystemProject.Models.UserViewModel;
using System.Windows.Forms;
using LibrarySystemProject.Filters;

namespace LibrarySystemProject.Controllers
{
    [AuthenticationFilter(RequireAdminRole = true)]
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            UserRepository repository = new UserRepository();
            List<User> users = repository.GetAll();

            UserListViewModel model = new UserListViewModel();
            model.Users = users;

            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User user = new User();
           // user.Id = model.Id;
            user.username = model.username;
            user.password = model.password;
            user.firstName = model.firstName;
            user.lastName = model.lastName;
            user.isAdmin = model.isAdmin;            

            var repository = new UserRepository();
            repository.Insert(user);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {

            UserRepository repository = new UserRepository();

            UserEditViewModel model = new UserEditViewModel();

            if (id.HasValue)
            {
                User user = repository.GetById(id.Value);
                model.Id = user.Id;
                model.username = user.username;
                model.password = user.password;
                model.firstName = user.firstName;
                model.lastName = user.lastName;
                model.isAdmin = user.isAdmin;

            }

            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(UserEditViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserRepository repository = new UserRepository();

            User user = new User();
            user.Id = model.Id;
            user.username = model.username;
            user.password = model.password;
            user.firstName = model.firstName;
            user.lastName = model.lastName;
            user.isAdmin = model.isAdmin;

            repository.Save(user);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {

            UserRepository repository = new UserRepository();

            User user = repository.GetById(id);

            UserDeleteViewModel model = new UserDeleteViewModel();
            model.username = user.username;
            model.password = user.password;
            model.firstName = user.firstName;
            model.lastName = user.lastName;
            model.isAdmin = user.isAdmin;

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(UserDeleteViewModel model)
        {

            UserRepository repository = new UserRepository();
            if (model.Id.ToString() != String.Empty)
            {
                repository.Delete(model.Id);
            }


            return RedirectToAction("Index");
        }

    }
}
