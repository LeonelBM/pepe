using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Lab_9___ASP.NET.DataAbstractionLayer;
using Lab_9___ASP.NET.Models;

namespace Lab_9___ASP.NET.Controllers
{
    public class AddController : Controller
    {
        // GET: Add
        public ActionResult Index()
        {
            return View();
        }

        public bool Add()
        {
            Guid id=Guid.NewGuid();
            string title = Request.Params["title"];
            string author = Request.Params["author"];
            int pages = int.Parse(Request.Params["pages"]);
            string genre = Request.Params["genre"];
            bool isLent = Request.Params["isLent"] == "on";


            DAL dal = new DAL();
            Book book = new Book();
            book.Id = id;
            book.Title = title;
            book.Author = author;
            book.Pages = pages;
            book.Genre = genre;
            book.IsLent = isLent;
            bool result = dal.AddBook(book);

            if (result)
            {
                Response.Redirect("~/BooksHome/Index");
            }

            return result;
        }
    }
}
