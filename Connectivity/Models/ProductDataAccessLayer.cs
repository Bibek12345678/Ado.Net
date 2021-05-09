using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Connectivity.Models
{
    public class ProductDataAccessLayer
    {
        public void AddProduct(Product product)
        {
            String CS = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spProductIns", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductName", product.ProductName.ToString());
                cmd.Parameters.AddWithValue("@Rate", product.Rate);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}