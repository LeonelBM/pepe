using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Lab_9___ASP.NET.DataAbstractionLayer;
using Lab_9___ASP.NET.Models;

namespace Lab_9___ASP.NET.Controllers
{
    public class DeleteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public bool Delete()
        {
            DAL dal = new DAL();

            // Extract the book ID from the URL query string
            Guid id;
            if (Guid.TryParse(Request.Params["id"], out id))
            {
                bool result = dal.DeleteBook(id);

                if (result)
                {
                    Response.Redirect("~/BooksHome/Index");
                }
            }

            return false;
        }
    }
}
