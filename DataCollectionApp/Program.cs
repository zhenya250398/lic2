using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DataCollectionApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (LicenseManager.Instance.TryLoadLicense("license.xml"))
                Application.Run(new Form1());
            else
            {
                if (!LicenseManager.Instance.HasLicense)
                    MessageBox.Show("Неверный файл лицезии!");
                else
                    MessageBox.Show("Истек срок действия лицезии!");
            }
        }
    }
}
