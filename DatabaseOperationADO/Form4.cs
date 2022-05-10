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
    public partial class Form4 : Form
    {
        EmpDal empdal = new EmpDal();
        public Form4()
        {
            InitializeComponent();
        }

        private void Show_Click(object sender, EventArgs e)
        {
            DataTable table = empdal.GetAllEmps();
            dataGridView1.DataSource = table;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Employee1 emp = new Employee1();
            emp.Name = txtName.Text;
            emp.Salary = Convert.ToDouble(txtSalary.Text);
            emp.Designation = txtDesignation.Text;
            int res = empdal.Save(emp);
            if (res == 1)
                MessageBox.Show("Inserted the record");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Employee1 emp = empdal.GetEmployee1ByEmpId(Convert.ToInt32(txtId.Text));
            if (emp.EmpId > 0)
            {
                txtName.Text = emp.Name;
                txtSalary.Text = emp.Salary.ToString();
                txtDesignation.Text = emp.Designation.ToString();
            }
            else
            {
                MessageBox.Show("Record not found");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Employee1 emp = new Employee1();
            emp.EmpId = Convert.ToInt32(txtId.Text);
            emp.Name = txtName.Text;
            emp.Salary = Convert.ToDouble(txtSalary.Text);
            emp.Designation = txtDesignation.Text;
            int res = empdal.Update(emp);
            if (res == 1)
                MessageBox.Show("updated the record");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int res = empdal.Delete(Convert.ToInt32(txtId.Text));
            if (res == 1)
                MessageBox.Show("deleted the record");
        }
    }
}
