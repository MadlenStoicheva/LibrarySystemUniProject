using LibrarySystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystemProject.Models.BookViewModel
{
    public class BookDeleteViewModel :BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string numberISBN { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string publishingHouse { get; set; }
        public DateTime releaseDate { get; set; }
        public int availability { get; set; }
    }
}