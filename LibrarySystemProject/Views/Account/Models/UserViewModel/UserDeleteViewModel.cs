using LibrarySystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystemProject.Models.UserViewModel
{
    public class UserDeleteViewModel:BaseEntity
    {
        public string username { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public bool isAdmin { get; set; }
    }
}