using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace Connectivity.Models
{
    public class SaleDataAccessLayer
    {
        public void AddSaleTransaction(SaleTransaction saleTransaction)
        {
            String CS = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using(SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd= new SqlCommand("spSaleTransactionIns", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", saleTransaction.ProductId.ToString());
                cmd.Parameters.AddWithValue("@CustomerId", saleTransaction.CustomerId.ToString());
                cmd.Parameters.AddWithValue("@Quantity", saleTransaction.Quantity);
                cmd.Parameters.AddWithValue("@SaleDate", saleTransaction.SaleDate);
                //cmd.Parameters.AddWithValue("@Rate", saleTransaction.Rate);
               // cmd.Parameters.Add("@SaleDate", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                con.Open();
                cmd.ExecuteNonQuery();
                
                con.Close();
            }
        }
        public SaleTransaction GetSaleTransactionById(int id)
        {
            SaleTransaction saleTransaction = new SaleTransaction();
            String CS = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spGetSaleTransactionById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SaleId", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    saleTransaction.SaleId = Convert.ToInt32(rdr["SaleId"]);
                    saleTransaction.ProductId = Convert.ToInt32(rdr["ProductId"]);
                    saleTransaction.CustomerId = Convert.ToInt32(rdr["CustomerId"]);
                    saleTransaction.Quantity = Convert.ToInt32(rdr["Quantity"]);
                    saleTransaction.Rate = Convert.ToInt32(rdr["Rate"]);
                }
            }


                return saleTransaction;
        }
        public void UpdateSaleTransaction(SaleTransaction saleTransaction)
        {
            String CS = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            using(SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spSaleTransactionUpd", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SaleId", saleTransaction.SaleId);
                cmd.Parameters.AddWithValue("@ProductId", saleTransaction.ProductId);
                cmd.Parameters.AddWithValue("@CustomerId", saleTransaction.CustomerId);
                cmd.Parameters.AddWithValue("@Quantity", saleTransaction.Quantity);
                //cmd.Parameters.AddWithValue("@Rate", saleTransaction.Rate);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            }
            
        }
    }
