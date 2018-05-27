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
    public partial class NewRewardForm : Form
    {
        private Reward _reward;

        public Reward Reward
        {
            get
            {
                return _reward;
            }
        }

        public NewRewardForm(Reward reward = null)
        {
            InitializeComponent();

            if (reward != null)
            {
                _reward = reward;

                txtBxName.Text = _reward.Name;
                txtBxDescription.Text = _reward.Description;
                numericUpDown1.Value = _reward.Year;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_reward == null)
                _reward = new Reward();

            _reward.Name = txtBxName.Text;
            _reward.Description = txtBxDescription.Text;
            _reward.Year = (int)numericUpDown1.Value;
        }
    }
}
