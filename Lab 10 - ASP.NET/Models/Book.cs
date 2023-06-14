using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab_9___ASP.NET.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public string Genre { get; set; }
        public bool IsLent { get; set; }
    }
}