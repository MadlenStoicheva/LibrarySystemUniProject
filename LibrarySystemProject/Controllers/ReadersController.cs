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
using LibrarySystemProject.Models.ReaderViewModel;
using LibrarySystemProject.Filters;

namespace LibrarySystemProject.Controllers
{
    
    public class ReadersController : Controller
    {
        public ActionResult Index()
        {
            ReaderRepository repository = new ReaderRepository();
            List<Reader> readers = repository.GetAll();

            ReaderListViewModel model = new ReaderListViewModel();
            model.Readers = readers;

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

    
        [HttpPost]
        public ActionResult Create(ReaderCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Reader readers = new Reader();
            readers.firstName = model.firstName;
            readers.lastName = model.lastName;
            readers.readerCard = model.readerCard;
            readers.expireDate = model.expireDate;
            
            var repository = new ReaderRepository();
            repository.Insert(readers);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {

            ReaderRepository repository = new ReaderRepository();

            ReaderEditViewModel model = new ReaderEditViewModel();

            if (id.HasValue)
            {
                Reader readers = repository.GetById(id.Value);
                model.Id=readers.Id;
                model.firstName = readers.firstName;
                model.lastName = readers.lastName;
                model.readerCard = readers.readerCard;
                model.expireDate = readers.expireDate;

            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ReaderEditViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ReaderRepository repository = new ReaderRepository();

            Reader readers = new Reader();
            readers.Id = model.Id;
            readers.firstName = model.firstName;
            readers.lastName = model.lastName;
            readers.readerCard = model.readerCard;
            readers.expireDate = model.expireDate;

            repository.Save(readers);

            return RedirectToAction("Index");
        }

        [AuthenticationFilter(RequireAdminRole = true)]
        [HttpGet]
        public ActionResult Delete(int id)
        {

            ReaderRepository repository = new ReaderRepository();

            Reader reader = repository.GetById(id);

            ReaderDeleteViewModel model = new ReaderDeleteViewModel();
            model.firstName = reader.firstName;
            model.lastName = reader.lastName;
            model.readerCard = reader.readerCard;
            model.expireDate = reader.expireDate;

            return View(model);
        }

        [AuthenticationFilter(RequireAdminRole = true)]
        [HttpPost]
        public ActionResult Delete(ReaderDeleteViewModel model)
        {

            ReaderRepository repository = new ReaderRepository();

            if (model.Id.ToString() != String.Empty)
            {
                repository.Delete(model.Id);
            }

            return RedirectToAction("Index");
        }
    }
}
