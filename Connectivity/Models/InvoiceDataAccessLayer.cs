using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace Connectivity.Models
{
    public class InvoiceDataAccessLayer
    {
        public void AddInvoice(Invoice invoice)
        {
            String CS = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using(SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spInvoiceIns", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("CustomerId", invoice.CustomerId.ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        } 
    }
}