using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeNguyenQuangBinh_21022010
{
    public partial class InventoryControl : Form
    {
        public InventoryControl()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InventoryControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            InventoryDashboard form = new InventoryDashboard();
            form.Show();
        }

        private void InventoryControl_Load(object sender, EventArgs e)
        {

        }
    }
}
