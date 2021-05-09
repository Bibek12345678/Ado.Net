using Connectivity.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace Connectivity.Controllers
{
    public class HomeController : Controller
    {
        ProductDataAccessLayer objProductDAL = new ProductDataAccessLayer();
        // GET: Home
        public ActionResult Index()
        {
            List<Product> productList = new List<Product>();
            String CS = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using(SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Select * From Product", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    var product = new Product();
                    product.ProductID = Convert.ToInt32(rdr["ProductID"]);
                    product.ProductName = rdr["ProductName"].ToString();
                    product.Rate = Convert.ToInt32(rdr["Rate"]);
                    productList.Add(product);
                }
            }

            return View(productList);
        }
        //public ActionResult Create(Product product)
        //{
        //    String CS = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(CS))

        //    {
        //        SqlCommand cmd = new SqlCommand("spProductIns", con);
        //        cmd.CommandType = CommandType.Text;

        //        cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
        //        cmd.Parameters.AddWithValue("@Rate", product.Rate);
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //        return RedirectToAction("Index");
        //    }
        //}

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Product());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product objProduct)
        {

            objProductDAL.AddProduct(objProduct);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            String CS = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spProductDel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            return RedirectToAction("Index");
        }


    }
}
