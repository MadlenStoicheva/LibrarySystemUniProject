using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LibrarySystem.Entity
{
    public class User : BaseEntity
    {
        public string username { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public bool isAdmin { get; set; }
       // public List<SelectListItem> valueList;
    }
}
