using DatabaseOperationADO.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseOperationADO.DAL
{
    public class ProductDal
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public ProductDal()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }


        public Product1 GetProductById(int id)
        {
            Product1 prod = new Product1();
            string qry = "select * from Product1 where ProductId=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows) // existance of record in dr object
            {
                while (dr.Read())
                {
                    prod.ProductId = Convert.ToInt32(dr["ProductId"]);
                    prod.Name = dr["Name"].ToString();
                    prod.Price = Convert.ToInt32(dr["Price"]);
                }
            }
            con.Close();
            return prod;
        }


        public int SaveProduct(Product1 prod)
        {

            string qry = "insert into Product1 values(@name,@price)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", prod.Name);
            cmd.Parameters.AddWithValue("@price", prod.Price);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public int UpdateProduct(Product1 prod)
        {

            string qry = "update Product1 set Name=@name,Price=@price where ProductId=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", prod.ProductId);
            cmd.Parameters.AddWithValue("@name", prod.Name);
            cmd.Parameters.AddWithValue("@price", prod.Price);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public int DeleteProduct(int id)
        {
            string qry = "delete from Product1 where ProductId=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public DataTable GetAllProducts()
        {
            DataTable table = new DataTable();
            string qry = "select * from Product1";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            table.Load(dr);
            con.Close();
            return table;
        }


    }


}
