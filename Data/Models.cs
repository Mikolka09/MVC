using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.Data
{
    public class Book
    {
        public Guid Id { get; set; }

        [DataType(DataType.ImageUrl)]
        public Uri CoverImageUrl { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name ="Book Title", ShortName ="Title", Order = 0)]
        public string Title { get; set; }

        [Display(Name = "Book Discription", ShortName = "Description", Order = 1)]
        public string Description { get; set; }

        public string[] Codes { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:Y}")]
        public DateTime DatePublish { get; set; }

        public HashSet<Author> Authors { get; set; }

    }

    public class Author
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        public DateTime Birthday { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public HashSet<Book> Books { get; set; }
    }
}
