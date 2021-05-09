using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connectivity.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
    public int CustomerId { get; set; }
        public int Total { get; set; }
}
}