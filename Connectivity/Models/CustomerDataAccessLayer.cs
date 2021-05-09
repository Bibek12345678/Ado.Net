using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace Connectivity.Models
{
    public class CustomerDataAccessLayer
    {
        public void AddCustomer(Customer Customer)
        {
            String CS = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spCustomerIns", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", Customer.Name.ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //public IEnumerable<Customer> Index()
        //{
        //    List<Customer> customerList = new List<Customer>();
        //    String CS = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(CS))
        //    {
        //        SqlCommand cmd = new SqlCommand("Select * from Customer", con);
        //        cmd.CommandType = CommandType.Text;
        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();
        //        while (rdr.Read())
        //        {
        //            var customer = new Customer();
        //            customer.CustomerId = Convert.ToInt32(rdr["CustomerId"]);
        //            customer.Name = rdr["Name"].ToString();

        //            customerList.Add(customer);
        //        }
        //    }

        //    return customerList;
        //}
    
    

   
        }
    
    
    
    }




