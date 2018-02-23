using LibrarySystem.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystemProject.Models.TakeABookViewModel
{
    public class TakeABookCreateViewModel
    {
        public List<SelectListItem> Books { get; set; }
        public List<SelectListItem> Readers { get; set; }
        public int BookId { get; set; }
        public int ReaderId { get; set; }

        [Required(ErrorMessage = "Please input a date taken! It is required!")]
       // [DataType(DataType.Date)]
        public DateTime dateTaken { get; set; }


        [Required(ErrorMessage = "Please input a date taken! It is required!")]
       // [DataType(DataType.Date)]
        public DateTime dateForReturn { get; set; }


        [Required(ErrorMessage = "Please input a date taken! It is required!")]
       // [DataType(DataType.Date)]
        public DateTime dateReturn { get; set; }

       
    }
}