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
    public partial class LicenseForm : Form
    {
        public LicenseForm()
        {
            InitializeComponent();
        }

        private void LicenseForm_Load(object sender, EventArgs e)
        {
            label2.Text = LicenseManager.Instance.Licensee;
            label4.Text = LicenseManager.Instance.ValidUntil.ToShortDateString();

            if (LicenseManager.Instance.EnabledFeatures.Contains("AllowSaveInFile"))
                richTextBox1.AppendText("Сохранение данных в файл\n");
            if (LicenseManager.Instance.EnabledFeatures.Contains("AllowReadFromFile"))
                richTextBox1.AppendText("Загрузка данных из файла\n");
        }
    }
}
