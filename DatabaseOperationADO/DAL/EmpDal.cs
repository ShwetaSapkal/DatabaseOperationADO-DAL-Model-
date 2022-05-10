using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseOperationADO.Model;

namespace DatabaseOperationADO.DAL
{
   public class EmpDal
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public EmpDal()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }

        public DataTable GetAllEmps()
        {
            DataTable table = new DataTable();
            string qry = "select * from Employee1";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            table.Load(dr);
            con.Close();
            return table;
        }

        public Employee1 GetEmployee1ByEmpId(int id)
        {
            Employee1 emp = new Employee1();
            string qry = "select * from Employee1 where EmpId=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    emp.EmpId = Convert.ToInt32(dr["EmpId"]);
                    emp.Name = dr["Name"].ToString();
                    emp.Salary = Convert.ToDouble(dr["Salary"]);
                    emp.Designation = dr["Designation"].ToString();
                }

            }
            con.Close();
            return emp;

        }

        public int Save(Employee1 emp)
        {
            string qry = "insert into Employee1 values(@name,@salary,@designation)";
            cmd = new SqlCommand(qry, con);
           
            cmd.Parameters.AddWithValue("@name", emp.Name);
            cmd.Parameters.AddWithValue("@salary", emp.Salary);
            cmd.Parameters.AddWithValue("@designation", emp.Designation);

            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public int Update(Employee1 emp)
        {
            string qry = "update Employee1 set Name=@name, Salary=@salary ,Designation=@designation where EmpId=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", emp.Name);
            cmd.Parameters.AddWithValue("@salary", emp.Salary);
            cmd.Parameters.AddWithValue("@id", emp.EmpId);
            cmd.Parameters.AddWithValue("@designation",emp.Designation);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public int Delete(int id)
        {
            string qry = "delete from Employee1 where EmpId=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

    }
}
