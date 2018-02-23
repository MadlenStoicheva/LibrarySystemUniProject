using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LibrarySystem.Entity
{
    public class TakeABook : BaseEntity
    {
        public int BookId { get; set; }
        public int ReaderId { get; set; }
        public DateTime dateTaken { get; set; }
        public DateTime dateForReturn { get; set; }
        public DateTime dateReturn { get; set; }
        //public List<SelectListItem> Books { get; set; }
       // public List<SelectListItem> Readers { get; set; }
         public virtual Book book { get; set; }
         public virtual Reader reader { get; set; }

    }
}
