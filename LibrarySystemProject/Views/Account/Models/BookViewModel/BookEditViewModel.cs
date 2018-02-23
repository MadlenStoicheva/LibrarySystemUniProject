using LibrarySystem.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibrarySystemProject.Models.BookViewModel
{
    public class BookEditViewModel:BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Required]
        [RegularExpression(@"^(\d{4})$", ErrorMessage = "Number ISBN must be 4 digits")]
        public string numberISBN { get; set; }

        [Required(ErrorMessage = "Please input a title! It is required!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Minimum length is 3 and maximum length is 20")]
        [RegularExpression(@"^([A-z -]+)$", ErrorMessage = "Title can consist of letters, spaces and dashes only!")]
        public string title { get; set; }

        [Required(ErrorMessage = "Please input an author ! It is required!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Minimum length is 3 and maximum length is 20")]
        [RegularExpression(@"^([A-z -]+)$", ErrorMessage = "Author can consist of letters, spaces and dashes only!")]
        public string author { get; set; }

        [Required(ErrorMessage = "Please input a Publishing House! It is required!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Minimum length is 3 and maximum length is 20")]
        [RegularExpression(@"^([A-z -]+)$", ErrorMessage = "Publishing House can consist of letters, spaces and dashes only!")]
        public string publishingHouse { get; set; }

        public DateTime releaseDate { get; set; }

        [Required]
        public int availability { get; set; }
    }
}