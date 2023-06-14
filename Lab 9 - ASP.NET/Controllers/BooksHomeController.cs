using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Lab_9___ASP.NET.DataAbstractionLayer;
using Lab_9___ASP.NET.Models;

namespace Lab_9___ASP.NET.Controllers
{
    public class BooksHomeController : Controller
    {
        // GET: BooksHome
        public ActionResult Index()
        {
            DAL dal = new DAL();
            List<Book> books = dal.GetAllBooks(); // Retrieve the list of books

            return View(books); // Pass the list of books to the view
        }

        public string GetBooks()
        {
            DAL dal = new DAL();
            List<Book> bookList = dal.GetAllBooks();

            string result = "<table class=\"table\"><thead><th>Title</th><th>Author</th><th>Pages</th><th>Genre</th><th>Is Lent?</th></thead>";

            foreach (Book book in bookList)
            {
                result += "<tr><td>" + book.Title + "</td><td>" + book.Author + "</td><td>" + book.Pages + "</td><td>" + book.Genre + "</td><td>" + book.IsLent + "</td></tr>";
            }

            result += "</table>";

            return result;
        }

        public string GetGenres()
        {
            DAL dal = new DAL();
            List<string> genresList = dal.GetGenres();

            string result = "";

            foreach (string genre in genresList)
            {
                result += "<option>" + genre + "</option>";
            }

            return result;
        }

        public string GetBooksByGenre()
        {
            string genre = Request.Params["genre"];

            DAL dal = new DAL();
            List<Book> bookList = dal.GetBooksByGenre(genre);

            string result = "<table><thead><th>Title</th><th>Author</th><th>Pages</th><th>Genre</th><th>Is Lent?</th></thead>";

            foreach (Book book in bookList)
            {
                result += "<tr><td>" + book.Title + "</td><td>" + book.Author + "</td><td>" + book.Pages + "</td><td>" + book.Genre + "</td><td>" + book.IsLent + "</td></tr>";
            }

            result += "</table>";

            return result;
        }
        public Guid GetBookIdByName()
        {
            DAL dal = new DAL();
            string title = Request.Params["title"];
            Guid bookId = dal.GetBookIdByName(title);

            return bookId;
        }
    }
}
