using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Connectivity.Models
{
    public class SaleTransaction
    {
        public int SaleId { get; set; }
        [Required]
        //[StringLength(100)]
        public int ProductId { get; set; }
        
        public DateTime? SaleDate { get; set; }
        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }
        [Required]
        // [StringLength(100)]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(100)]
        public String Name { get; set; }
        public Product Product { get; set; }

        public IList<Product> ProductList { get; set; }
        public IList<Customer> CustomerList { get; set; }
        [Required]

        public int Quantity { get; set; }
        public int Rate { get; set; }
        public int? Total { get; set; }
        public int? InvoiceId { get; set; }
    }
}