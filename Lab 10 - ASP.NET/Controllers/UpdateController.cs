using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Lab_9___ASP.NET.DataAbstractionLayer;
using Lab_9___ASP.NET.Models;

namespace Lab_9___ASP.NET.Controllers
{
    public class UpdateController : Controller
    {
        // GET: Update
        public ActionResult Index()
        {
            return View();
        }

        public bool Update()
        {
            DAL dal = new DAL();
            string title = Request.Params["title"];
            string author = Request.Params["author"];
            int pages = int.Parse(Request.Params["pages"]);
            string genre = Request.Params["genre"];
            bool isLent = Request.Params["isLent"] == "on";

            string oldTitle = Request.Params["oldTitle"];

            // Extract the book ID from the URL query string
            Guid id;
            if (Guid.TryParse(Request.Params["id"], out id))
            {
                Book book = new Book();
                book.Id = id;
                book.Title = title;
                book.Author = author;
                book.Pages = pages;
                book.Genre = genre;
                book.IsLent = isLent;
                bool result = dal.UpdateBook(book);

                if (result)
                {
                    Response.Redirect("~/BooksHome/Index");
                }
            }

            return false;
        }
    }
}
