using LibrarySystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystemProject.Models.ReaderViewModel
{
    public class ReaderDeleteViewModel:BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string readerCard { get; set; }
        public DateTime expireDate { get; set; }
    }
}