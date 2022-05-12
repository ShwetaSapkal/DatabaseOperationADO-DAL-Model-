using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseOperationADO
{
    public partial class Form8 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
        public Form8()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }

        public DataSet GetStuds()
        {
            da = new SqlDataAdapter("select * from Students", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "stud1");
            return ds;
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {

            ds = GetStuds();
            DataRow row = ds.Tables["stud1"].NewRow();
            row["RollNo"] = txtRollNo.Text;
            row["Name"] = txtName.Text;
            row["Branch"] = txtBranch.Text;
            row["Percentage"] = txtPercentage.Text;


            ds.Tables["stud1"].Rows.Add(row);

          
            int res = da.Update(ds.Tables["stud1"]);
            if (res == 1)
                MessageBox.Show("Record saved");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ds = GetStuds();

            DataRow row = ds.Tables["stud1"].Rows.Find(Convert.ToInt32(txtRollNo.Text));
            if (row != null)
            {
                txtRollNo.Text = row["RollNo"].ToString();
                txtName.Text = row["Name"].ToString();
                txtBranch.Text = row["Branch"].ToString();
                txtPercentage.Text = row["Percentage"].ToString();

            }
            else
            {
                MessageBox.Show("Record not found");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            double perc = Convert.ToDouble(txtPercentage.Text);
            if (string.IsNullOrEmpty(txtName.Text) && perc > 0)
            {
                MessageBox.Show("Enter name or percentage should be greater than 0");
            }

            else
            {


                ds = GetStuds();

                DataRow row = ds.Tables["stud1"].Rows.Find(Convert.ToInt32(txtRollNo.Text));
                if (row != null)
                {
                    row["Name"] = txtName.Text;
                    row["Branch"] = txtBranch.Text;
                    row["Percentage"] = txtPercentage.Text;


                    int res = da.Update(ds.Tables["stud1"]);
                    if (res == 1)
                        MessageBox.Show("record updated");
                }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ds = GetStuds();
            DataRow row = ds.Tables["stud1"].Rows.Find(Convert.ToInt32(txtRollNo.Text));

            if (row != null)
            {
                row.Delete();
                int res = da.Update(ds.Tables["stud1"]);
                if (res == 1)
                {
                    MessageBox.Show("Record deleted");
                }
                else
                {
                    MessageBox.Show("Not able to delete");
                }
            }

            else
            {
                MessageBox.Show("Record not found");
            }

        }
    }
}
