using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Data
{
    public class Publisher
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(4)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Country { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public HashSet<Book> Books { get; set; }
    }
}
