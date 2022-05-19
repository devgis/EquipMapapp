using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MapApp
{
    public partial class AddEquip : Form
    {
        public AddEquip()
        {
            InitializeComponent();
        }
        public String EquipName;
        public String EquipState;
        public String Remarks;
        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbEquipName.Text.Trim()))
            {
                MessageBox.Show("请输入设备名称");
                tbEquipName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(cbEquipState.Text.Trim()))
            {
                MessageBox.Show("请输入设备名称");
                cbEquipState.Focus();
                return;
            }
            EquipName = tbEquipName.Text;
            EquipState = cbEquipState.Text;
            Remarks = tbRemarks.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void AddEquip_Load(object sender, EventArgs e)
        {
            tbEquipName.Focus();
        }
    }
}