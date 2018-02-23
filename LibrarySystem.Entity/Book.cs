using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Entity
{
    public class Book : BaseEntity
    {
        public string numberISBN { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string publishingHouse { get; set; }
        public DateTime releaseDate { get; set; }
        public int availability { get; set; }
    }
}
