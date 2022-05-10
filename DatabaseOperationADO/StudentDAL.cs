using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DatabaseOperationADO
{
    class StudentDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public StudentDAL()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }

        public int CreateStudent(Student student)
        {
            string qry = "insert into Students values(@rollno,@name,@branch,@percentage)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@rollno", student.RollNo);
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@branch", student.Branch);
            cmd.Parameters.AddWithValue("@percentage", student.Percentage);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public int UpdateStudent(Student student)
        {
            
                string qry = "update Students set Name=@name,Branch=@branch,Percentage=@percentage where RollNo=@rollno";
            cmd = new SqlCommand(qry,con);
                cmd.Parameters.AddWithValue("@rollno", student.RollNo);
                cmd.Parameters.AddWithValue("@name", student.Name);
                cmd.Parameters.AddWithValue("@branch", student.Branch);
                cmd.Parameters.AddWithValue("@percentage", student.Percentage);
                con.Open();

                int res = cmd.ExecuteNonQuery();
                con.Close();
                return res;

        }

        public Student GetStudentByRollNo(int rollno)
        { 
            Student student=new Student();
            string qry = "select * from Students where RollNo=@rollno";
            cmd = new SqlCommand(qry,con);
            cmd.Parameters.AddWithValue("@rollno",rollno);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    student.RollNo = Convert.ToInt32(dr["RollNo"]);
                    student.Name = dr["Name"].ToString();
                    student.Branch = dr["Branch"].ToString();
                    student.Percentage = Convert.ToInt32(dr["Percentage"]);
                }
            }
            con.Close();
            return student;
        }

        public int DeleteStudent(Student student)
        {
            
            string qry = "delete from Students where RollNo=@rollno";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@rollno", student.RollNo);
            con.Open();

            int res= cmd.ExecuteNonQuery();
            con.Close();
            return res;


        }

        /* public int AddNew(int rollno)
         {

         }
        */

        public DataTable GetAllStudents()
        {
            DataTable table = new DataTable();
            string qry = "select * from Students";
            cmd = new SqlCommand(qry,con);
            con.Open();
            dr = cmd.ExecuteReader();
            table.Load(dr);
            return table;
        }
    }
}
