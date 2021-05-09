using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Connectivity.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public  int Rate { get; set; }
    }
}