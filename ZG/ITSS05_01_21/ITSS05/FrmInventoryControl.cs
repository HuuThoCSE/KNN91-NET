using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITSS05
{
    public partial class FrmInventoryControl : Form
    {
        public FrmInventoryControl()
        {
            InitializeComponent();
        }

        private void Inventory_Control_Load(object sender, EventArgs e)
        {

        }

        private void FrmInventoryControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
