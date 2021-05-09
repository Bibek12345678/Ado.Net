using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Connectivity.Models;

namespace Connectivity.Controllers
{
    public class SaleController : Controller
    {

        SaleDataAccessLayer sdal = new SaleDataAccessLayer();
        // GET: Sale
        public ActionResult Index()
        {
            List<SaleTransaction> saleList = new List<SaleTransaction>();
            String CS = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SpSalesTransactionSel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var saleTransaction = new SaleTransaction();
                    saleTransaction.SaleId = Convert.ToInt32(rdr["SaleId"]);
                    saleTransaction.ProductName = rdr["ProductName"].ToString();
                    saleTransaction.Name = rdr["Name"].ToString();
                    saleTransaction.SaleDate = Convert.ToDateTime(rdr["SaleDate"]);
                    saleTransaction.Quantity = Convert.ToInt32(rdr["Quantity"]);
                    saleTransaction.Rate = Convert.ToInt32(rdr["Rate"]);
                    saleTransaction.Total = Convert.ToInt32(rdr["Total"]);
                    saleList.Add(saleTransaction);
                }

                return View(saleList);
            }

        }
        [HttpGet]
        public ActionResult Create()
        {
            SaleTransaction obj = new SaleTransaction();
            List<Product> products = new List<Product>();
            List<Customer> customers = new List<Customer>();
            String CS = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spSelectBoth", con);
                cmd.CommandType = CommandType.StoredProcedure;
          
                con.Open();
                // cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Customer newCus = new Customer();

                    Product newItem = new Product();
                    if (reader.GetInt32(2) == 1)
                    {
                        newCus.CustomerId = reader.GetInt32(0);
                        newCus.Name = reader.GetString(1);
                        customers.Add(newCus);

                    }
                   
                    if (reader.GetInt32(2) == 0)
                    {
                        newItem.ProductID = reader.GetInt32(0);
                        newItem.ProductName = reader.GetString(1);

                        products.Add(newItem);
                    }

                }
              
                con.Close();
            }
            obj.ProductList = products;
            obj.CustomerList = customers;
            ViewBag.Products = new SelectList(products, "ProductID", "ProductName");
            ViewBag.Customers = new SelectList(customers, "CustomerId", "Name");
            return View(obj);
            //return View(new SaleTransaction());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SaleTransaction objsaleTransaction)
        {
            objsaleTransaction.SaleDate = DateTime.Now;
            sdal.AddSaleTransaction(objsaleTransaction);
            return RedirectToAction("Index");

        }
        public ActionResult Delete(int? id)
        {
            String CS = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spSaleTransactionDel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SaleId", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            SaleTransaction saleTransaction = sdal.GetSaleTransactionById(id);

            return View(saleTransaction);
        }
        [HttpPost]
        public ActionResult Edit(SaleTransaction objSale)
        {

            sdal.UpdateSaleTransaction(objSale);
            return RedirectToAction("Index");

        }
    }



}

