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
    public partial class DepartmentForm : Form
    {
        List<Department> _departments;
        public DepartmentForm(List<Department> departments)
        {
            InitializeComponent();

            listBox1.DataSource = departments;
            _departments = departments;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Department dep = new Department();
            dep.Name = textBox1.Text;
            dep.Id = _departments.Count > 0 ? _departments.Max(t => t.Id) + 1 : 1;

            _departments.Add(dep);

            listBox1.DataSource = null;
            listBox1.DataSource = _departments;
        }
    }
}
