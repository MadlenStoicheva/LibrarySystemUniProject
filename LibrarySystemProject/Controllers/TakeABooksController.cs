using Data.Entity.Repositories;
using LibrarySystem.Entity;
using LibrarySystemProject.Filters;
using LibrarySystemProject.Models.TakeABookViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystemProject.Controllers
{
    
    public class TakeABooksController : Controller
    {
        public ActionResult Index()
        {
            TakeABookRepository repository = new TakeABookRepository();
            List<TakeABook> takeABooks = repository.GetAll();

            TakeABookListViewModel model = new TakeABookListViewModel();
            model.TakeABooks = takeABooks;

            return View(model);
        }
        public ActionResult Create()
        {
            TakeABookCreateViewModel model = new TakeABookCreateViewModel();
            model.Books = PopuateBooksList();
            model.Readers = PopuateReadersList();
            model.dateTaken = model.dateTaken.Date;
            model.dateForReturn = model.dateForReturn.Date;
            model.dateReturn = model.dateReturn.Date;


            return View(model);
        }

        private List<SelectListItem> PopuateBooksList()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            BookRepository bookRepo = new BookRepository();
            List<Book> books = bookRepo.GetAll();
            foreach (Book book in books)
            {
                SelectListItem item = new SelectListItem();
                item.Value = book.Id.ToString();
                item.Text = $"{book.title} ({book.author})";

                result.Add(item);
            }

            return result;
        }

        private List<SelectListItem> PopuateReadersList()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            ReaderRepository readerRepo = new ReaderRepository();
            List<Reader> readers = readerRepo.GetAll();
            foreach (Reader reader in readers)
            {
                SelectListItem item = new SelectListItem();
                item.Value = reader.Id.ToString();
                item.Text = $"{reader.firstName} {reader.lastName}";

                result.Add(item);
            }

            return result;
        }

        [HttpPost]
        public ActionResult Create(TakeABookCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            TakeABook takeABook = new TakeABook();
            takeABook.BookId = model.BookId;
            takeABook.ReaderId = model.ReaderId;
            takeABook.dateTaken = model.dateTaken.Date;
            takeABook.dateForReturn = model.dateForReturn.Date;
            takeABook.dateReturn = model.dateReturn.Date;

            var repository = new TakeABookRepository();
            repository.Insert(takeABook);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {

            TakeABookRepository repository = new TakeABookRepository();

            TakeABookEditViewModel model = new TakeABookEditViewModel();

            if (id.HasValue)
            {
                TakeABook takeABook = repository.GetById(id.Value);
                model.Id = takeABook.Id;
                model.Books = PopuateBooksList();
                model.Readers = PopuateReadersList();
                model.dateTaken = takeABook.dateTaken.Date;
                model.dateForReturn = takeABook.dateForReturn.Date;
                model.dateReturn = takeABook.dateReturn.Date;


            }

            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(TakeABookEditViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            TakeABookRepository repository = new TakeABookRepository();

            TakeABook takeABook = new TakeABook();
            takeABook.Id = model.Id;
            takeABook.BookId = model.BookId;
            takeABook.ReaderId = model.ReaderId;
            takeABook.dateTaken = model.dateTaken.Date;
            takeABook.dateForReturn = model.dateForReturn.Date;
            takeABook.dateReturn = model.dateReturn.Date;

            repository.Save(takeABook);

            return RedirectToAction("Index");
        }

        [AuthenticationFilter(RequireAdminRole = true)]
        [HttpGet]
        public ActionResult Delete(int id)
        {

            TakeABookRepository repository = new TakeABookRepository();

            TakeABook takeABook = repository.GetById(id);

            TakeABookDeleteViewModel model = new TakeABookDeleteViewModel();
           // model.Id = takeABook.Id;
            model.BookId = takeABook.BookId;
            model.ReaderId = takeABook.ReaderId;
            model.dateTaken = takeABook.dateTaken;
            model.dateForReturn = takeABook.dateForReturn;
            model.dateReturn = takeABook.dateReturn;

            return View(model);
        }

        [AuthenticationFilter(RequireAdminRole = true)]
        [HttpPost]
        public ActionResult Delete(TakeABookDeleteViewModel model)
        {

            TakeABookRepository repository = new TakeABookRepository();
            if (model.Id.ToString() != String.Empty)
            {
                repository.Delete(model.Id);
            }


            return RedirectToAction("Index");
        }

    }
}
