using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Connectivity.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public String Name { get; set; }
    }
}