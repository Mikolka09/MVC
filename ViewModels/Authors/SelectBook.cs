using MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.ViewModels.Authors
{
    public class SelectBook
    {
        public Book Book { get; set; }
        public bool isSelected { get; set; }
    }
}
