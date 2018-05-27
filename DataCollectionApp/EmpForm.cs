using DataCollectionModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataCollectionApp
{
    public partial class EmpForm : Form
    {
        private Employee _emp;

        public Employee Employee
        {
            get
            {
                return _emp;
            }
        }

        public EmpForm(List<Department> departments, Employee emp = null)
        {
            InitializeComponent();

            comboBoxDepartments.DataSource = departments;
            comboBoxGender.DataSource = Enum.GetValues(typeof(Gender));

            if (emp != null)
            {
                _emp = emp;

                txtBxFIO.Text = _emp.FIO;
                txtBxAddress.Text = _emp.Address;
                txtBxTel.Text = _emp.Tel;
                comboBoxDepartments.SelectedItem = _emp.Department;
                comboBoxGender.SelectedItem = _emp.Gender;
                dateTimePicker1.Value = _emp.Birthday;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_emp == null)
            {
                _emp = new Employee();
            }

            _emp.FIO = txtBxFIO.Text;
            _emp.Address = txtBxAddress.Text;
            _emp.Department = comboBoxDepartments.SelectedItem as Department;
            _emp.Tel = txtBxTel.Text;
            _emp.Gender = (Gender)comboBoxGender.SelectedItem;
            _emp.Birthday = dateTimePicker1.Value;
        }
    }
}
