using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibrarySystemProject.Models.ReaderViewModel
{
    public class ReaderCreateViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]

        [StringLength(80, MinimumLength = 3, ErrorMessage = "Minimum length is 3 and maximum length is 80")]
        [RegularExpression(@"^([A-z-]+)$", ErrorMessage = "Name can consist of letters and dashes only!")]
        public string firstName { get; set; }

        [StringLength(80, MinimumLength = 3, ErrorMessage = "Minimum length is 3 and maximum length is 80")]
        [RegularExpression(@"^([A-z-]+)$", ErrorMessage = "Name can consist of letters and dashes only!")]
        public string lastName { get; set; }

        [RegularExpression(@"^(\d{4})$", ErrorMessage = "Reader Card must be 4 digits")]
        public string readerCard { get; set; }

        [Required(ErrorMessage = "Please input a expire date! It is required!")]
        [DataType(DataType.Date)]
        public DateTime expireDate { get; set; }
    }
}