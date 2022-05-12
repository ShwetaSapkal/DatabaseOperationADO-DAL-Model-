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
    public partial class Form7 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
        public Form7()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        public DataSet GetProds()
        {
            da = new SqlDataAdapter("select * from Product1", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "product");
            return ds;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            ds = GetProds();
            DataRow row = ds.Tables["product"].NewRow();
            row["Name"] = txtName.Text;
            row["Price"] = txtPrice.Text;
            
            ds.Tables["product"].Rows.Add(row);

            // reflect the changes from DataSet to Database
            int res = da.Update(ds.Tables["product"]);
            if (res == 1)
                MessageBox.Show("Record saved");

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ds = GetProds();
         
            DataRow row = ds.Tables["product"].Rows.Find(Convert.ToInt32(txtId.Text));
            if (row != null)
            {
                txtName.Text = row["Name"].ToString();
                txtPrice.Text = row["Price"].ToString();
               
            }
            else
            {
                MessageBox.Show("Record not found");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ds = GetProds();
            DataRow row = ds.Tables["product"].Rows.Find(Convert.ToInt32(txtId.Text));

            if (row != null)
            {
                row.Delete();
                int res = da.Update(ds.Tables["product"]);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            double price = Convert.ToDouble(txtPrice.Text);
            if (string.IsNullOrEmpty(txtName.Text) && price > 0)
            {
                MessageBox.Show("Enter name or price should be greater than 0");
            }

            else
            {


                ds = GetProds();
                
                DataRow row = ds.Tables["product"].Rows.Find(Convert.ToInt32(txtId.Text));
                if (row != null)
                {
                    row["Name"] = txtName.Text;
                    row["Price"] = txtPrice.Text;
                    
                    int res = da.Update(ds.Tables["product"]);
                    if (res == 1)
                        MessageBox.Show("record updated");
                }
            }

        }
    }
}
