using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseOperationADO
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form2()
        {
            InitializeComponent();
            con = new SqlConnection(@"Server=DESKTOP-IVR2DU7\SQLEXPRESS;database=ThinkQuotient;Integrated Security=True");
        }

        public void ClearAll()
        {
            txtId.Clear();
            txtName.Clear();
            txtDesignation.Clear();
            txtSalary.Clear();
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "insert into Employee values(@id,@name,@designation,@salary)";
                cmd = new SqlCommand(query,con);

                cmd.Parameters.AddWithValue("@id",Convert.ToInt32(txtId.Text));
                cmd.Parameters.AddWithValue("@name",txtName.Text);
                cmd.Parameters.AddWithValue("@designation",txtDesignation.Text);
                cmd.Parameters.AddWithValue("@salary",Convert.ToInt32(txtSalary.Text));

                con.Open();
                int res = cmd.ExecuteNonQuery();

                if (res == 1)
                {
                    MessageBox.Show("Record inserted successfully");
                    ClearAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from Employee where EmpId=@id";
                cmd = new SqlCommand(query,con);
                cmd.Parameters.AddWithValue("@id",Convert.ToInt32(txtId.Text));
                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtName.Text = dr["EmpName"].ToString();
                        txtDesignation.Text = dr["Designation"].ToString();
                        txtSalary.Text = dr["Salary"].ToString();
                       
                    }
                }
                else 
                {
                    MessageBox.Show("Record not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "update Employee set EmpName=@name,Designation=@designation,Salary=@salary where EmpId=@id";
                cmd = new SqlCommand(query,con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                cmd.Parameters.AddWithValue("@name",txtName.Text);
                cmd.Parameters.AddWithValue("@designation",txtDesignation.Text);
                cmd.Parameters.AddWithValue("@salary",Convert.ToInt32(txtSalary.Text));
                con.Open();

                int res = cmd.ExecuteNonQuery();
                if (res == 1)
                {
                    MessageBox.Show("Record updated sccessfully");
                    ClearAll();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "delete from Employee where EmpId=@id";

                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                con.Open();

                int res = cmd.ExecuteNonQuery();
                if (res == 1)
                {
                    MessageBox.Show("Record deleted");
                    ClearAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "select max(EmpId) from Employee";
                cmd = new SqlCommand(query, con);
                con.Open();
                object obj = cmd.ExecuteScalar();
                if (obj == DBNull.Value)
                {
                    txtId.Text = "1";
                }
                else
                {
                    int id = Convert.ToInt32(obj);
                    id++;
                    txtId.Text = id.ToString();

                }
                txtId.Enabled = false;
                txtName.Clear();
                txtDesignation.Clear();
                txtSalary.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from Employee";
                cmd = new SqlCommand(query, con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    DataTable table = new DataTable();
                    table.Load(dr);
                    dataGridView1.DataSource = table;

                }
                else
                {
                    MessageBox.Show("Record not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtDesignation.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtSalary.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();


        }
    }
}
