using DatabaseOperationADO.DAL;
using DatabaseOperationADO.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseOperationADO
{
    public partial class Form5 : Form
    {
        ProductDal prodDAL = new ProductDal();
        public Form5()
        {
            InitializeComponent();
        }

      
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Product1 prod = new Product1();
                prod.Name = txtName.Text;
                prod.Price = Convert.ToInt32(txtPrice.Text);
                int res = prodDAL.SaveProduct(prod);
                if (res == 1)
                {
                    MessageBox.Show("Record inserted");
                    txtId.Enabled = true;
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Product1 prod = prodDAL.GetProductById(Convert.ToInt32(txtId.Text));
                txtName.Text = prod.Name;
                txtPrice.Text = prod.Price.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Product1 prod = new Product1();
                prod.ProductId = Convert.ToInt32(txtId.Text);
                prod.Name = txtName.Text;
                prod.Price = Convert.ToInt32(txtPrice.Text);
                int res = prodDAL.UpdateProduct(prod);
                if (res == 1)
                {
                    MessageBox.Show("Record updated");
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int res = prodDAL.DeleteProduct(Convert.ToInt32(txtId.Text));
            if (res == 1)
                MessageBox.Show("deleted the record");
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            DataTable table = prodDAL.GetAllProducts();
            dataGridView1.DataSource = table;
        }
    }
}
