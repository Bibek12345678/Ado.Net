using Connectivity.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Connectivity.Controllers
{
    public class InvoiceController : Controller
    {
        InvoiceDataAccessLayer idal = new InvoiceDataAccessLayer();

        public ActionResult Index()
        {
            List<Invoice> invoiceList = new List<Invoice>();
            String CS = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Select * From Invoice", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var invoice = new Invoice();
                    invoice.CustomerId = Convert.ToInt32(rdr["CustomerId"]);
                    invoice.Total = Convert.ToInt32(rdr["Total"]);
                    invoiceList.Add(invoice);
                }
            }

            return View(invoiceList);
        }

    

    [HttpGet]
        public ActionResult Create()
        {
            return View(new Invoice());
        }
        [HttpPost]
        public ActionResult Create(Invoice objInvoice)
        {
            idal.AddInvoice(objInvoice);
            return RedirectToAction("Index");
        }
    }
}