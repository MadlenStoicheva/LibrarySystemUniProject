using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibrarySystem.Entity;
using LibrarySystemProject.Models.BookViewModel;
using Data.Entity.Repositories;
using LibrarySystemProject.Filters;

namespace LibrarySystemProject.Controllers
{
    
    public class BooksController: Controller
    {
        public ActionResult Index()
        {
            BookRepository repository = new BookRepository();
            List<Book> books = repository.GetAll();

            BookListViewModel model = new BookListViewModel();
            model.Books = books;

            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BookCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Book book = new Book();
            book.numberISBN = model.numberISBN;
            book.title = model.title;
            book.author = model.author;
            book.publishingHouse = model.publishingHouse;
            book.releaseDate = model.releaseDate;
            book.availability = model.availability;

            var repository = new BookRepository();
            repository.Insert(book);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {

            BookRepository repository = new BookRepository();

            BookEditViewModel model = new BookEditViewModel();

            if (id.HasValue)
            {
                Book book = repository.GetById(id.Value);
                model.Id = book.Id;
                model.numberISBN = book.numberISBN;
                model.title = book.title;
                model.author= book.author;
                model.publishingHouse = book.publishingHouse;
                model.releaseDate = book.releaseDate;
                model.availability = book.availability;

            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BookEditViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            BookRepository repository = new BookRepository();

            Book book = new Book();
            book.Id = model.Id;
            book.numberISBN = model.numberISBN;
            book.title = model.title;
            book.author = model.author;
            book.publishingHouse = model.publishingHouse;
            book.releaseDate = model.releaseDate;
            book.availability = model.availability;


            repository.Save(book);

            return RedirectToAction("Index");
        }

        [AuthenticationFilter(RequireAdminRole = true)]
        [HttpGet]
        public ActionResult Delete(int id)
        {

            BookRepository repository = new BookRepository();

            Book book = repository.GetById(id);

            BookDeleteViewModel model = new BookDeleteViewModel();
            model.numberISBN = book.numberISBN;
            model.title = book.title;
            model.author = book.author;
            model.publishingHouse = book.publishingHouse;
            model.releaseDate = book.releaseDate;
            model.availability = book.availability;

            return View(model);
        }
        [AuthenticationFilter(RequireAdminRole = true)]
        [HttpPost]
        public ActionResult Delete(BookDeleteViewModel model)
        {

            BookRepository repository = new BookRepository();
            if (model.Id.ToString() != String.Empty)
            {
                repository.Delete(model.Id);
            }


            return RedirectToAction("Index");
        }

    }
}