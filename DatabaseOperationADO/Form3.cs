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
    public partial class Form3 : Form
    {
        StudentDAL studentDAL = new StudentDAL();

        public Form3()
        {
            InitializeComponent();
        }

        public void ClearAll()
        {
            txtRollNo.Clear();
            txtName.Clear();
            txtBranch.Clear();
            txtPercentage.Clear();
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                Student student = new Student();
                student.RollNo = Convert.ToInt32(txtRollNo.Text);
                student.Name = txtName.Text;
                student.Branch = txtBranch.Text;
                student.Percentage = Convert.ToInt32(txtPercentage.Text);

                int res = studentDAL.CreateStudent(student);

                if (res == 1)
                {
                    MessageBox.Show("Records Inserted");
                    txtRollNo.Enabled = true;
                    ClearAll();
                }
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

                Student student = new Student();
                student.RollNo = Convert.ToInt32(txtRollNo.Text);
                student.Name = txtName.Text;
                student.Branch = txtBranch.Text;
                student.Percentage = Convert.ToInt32(txtPercentage.Text);

                int res = studentDAL.UpdateStudent(student);

                if (res == 1)
                {
                    MessageBox.Show("Records Updated");
                    ClearAll();
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
                Student student = studentDAL.GetStudentByRollNo(Convert.ToInt32(txtRollNo.Text));
                txtName.Text = student.Name;
                txtBranch.Text = student.Branch;
                txtPercentage.Text = student.Percentage.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Student student = new Student();
                student.RollNo = Convert.ToInt32(txtRollNo.Text);

                int res = studentDAL.DeleteStudent(student);
                if (res==1)
                {
                    MessageBox.Show("Record deleted");
                    ClearAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       private void btnAddNew_Click(object sender, EventArgs e)
        {
           
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            DataTable table = studentDAL.GetAllStudents();
            dataGridView1.DataSource = table;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
