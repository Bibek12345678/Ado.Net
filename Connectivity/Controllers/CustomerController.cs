using Connectivity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace Connectivity.Controllers
{
    public class CustomerController : Controller
    {
        CustomerDataAccessLayer cdal = new CustomerDataAccessLayer();
        // GET: Customer
        public ActionResult Create()
        {
            return View(new Customer());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer objCustomer)
        {

            cdal.AddCustomer(objCustomer);
            return RedirectToAction("Index");
        }


        public ActionResult Index()
        {
            List<Customer> customerList = new List<Customer>();
            String CS = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Select * From Customer", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var customer = new Customer();
                    customer.CustomerId = Convert.ToInt32(rdr["CustomerId"]);
                    customer.Name = rdr["Name"].ToString();
                    customerList.Add(customer);
                }
            }

            return View(customerList);
        }
        public ActionResult Delete(int? id)
        {
            String CS = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spCustomerDel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerId", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            return RedirectToAction("Index");
        }

    }
}