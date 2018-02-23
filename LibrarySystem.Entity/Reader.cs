using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Entity
{
    public class Reader: BaseEntity
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string readerCard { get; set; }
        public DateTime expireDate { get; set; }
    }
}
