using LibrarySystem.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    class LibrarySystemContext: DbContext
    {
        public LibrarySystemContext() : base("LibrarySystemDb")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<TakeABook> TakeABooks { get; set; }

    }
    
}
