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
    public partial class Form1 : Form
    {
        List<Employee> _employees = new List<Employee>();
        List<Department> _departments = new List<Department>();

        public Form1()
        {
            InitializeComponent();

            dataGridView1.Columns["birthdayDataGridViewTextBoxColumn"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void UpdateGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _employees;
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML files (*.xml)|*.xml";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileManager fileManager = new FileManager(saveFileDialog.FileName);
                fileManager.Save(_employees);
                fileManager.Save(_departments);
            }
        }

        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmpForm empForm = new EmpForm(_departments);

            if (empForm.ShowDialog() == DialogResult.OK)
            {
                Employee emp = empForm.Employee;

                emp.Id = _employees.Count > 0 ? _employees.Max(t => t.Id) + 1 : 1;

                _employees.Add(empForm.Employee);

                UpdateGrid();
            }
        }

        private void удалениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Employee emp = dataGridView1.SelectedRows[0].DataBoundItem as Employee;

                _employees.Remove(emp);

                UpdateGrid();
            }
        }

        private void редактированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Employee emp = dataGridView1.SelectedRows[0].DataBoundItem as Employee;

                EmpForm empForm = new EmpForm(_departments, emp);

                if (empForm.ShowDialog() == DialogResult.OK)
                {
                    UpdateGrid();
                }
            }
        }

        private void подразделенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DepartmentForm form = new DepartmentForm(_departments);
            form.ShowDialog();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Employee emp = dataGridView1.SelectedRows[0].DataBoundItem as Employee;

                dataGridView2.DataSource = emp.Rewards;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Employee emp = dataGridView1.SelectedRows[0].DataBoundItem as Employee;

                NewRewardForm form = new NewRewardForm();

                if (form.ShowDialog() == DialogResult.OK)
                {
                    emp.Rewards.Add(form.Reward);

                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = emp.Rewards;
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Employee emp = dataGridView1.SelectedRows[0].DataBoundItem as Employee;

                if (dataGridView2.SelectedRows.Count > 0)
                {
                    Reward reward = dataGridView2.SelectedRows[0].DataBoundItem as Reward;

                    NewRewardForm form = new NewRewardForm(reward);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        dataGridView2.DataSource = null;
                        dataGridView2.DataSource = emp.Rewards;
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Employee emp = dataGridView1.SelectedRows[0].DataBoundItem as Employee;

                if (dataGridView2.SelectedRows.Count > 0)
                {
                    Reward reward = dataGridView2.SelectedRows[0].DataBoundItem as Reward;

                    emp.Rewards.Remove(reward);

                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = emp.Rewards;
                }
            }
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileManager fileManager = new FileManager(openFileDialog.FileName);
                _employees = fileManager.LoadEmployees();
                _departments = fileManager.LoadDepartments();

                UpdateGrid();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;

            if (txtBxDepartment.Text == "")
            {
                dataGridView1.DataSource = _employees;
            }
            else
            {
                dataGridView1.DataSource = _employees.Where(t => t.Department.Name == txtBxDepartment.Text).ToList();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!LicenseManager.Instance.EnabledFeatures.Contains("AllowSaveInFile"))
                сохранитьToolStripMenuItem.Enabled = false;

            if (!LicenseManager.Instance.EnabledFeatures.Contains("AllowReadFromFile"))
                загрузитьToolStripMenuItem.Enabled = false;
        }

        private void лицензияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LicenseForm form = new LicenseForm();
            form.ShowDialog();
        }
    }
}
