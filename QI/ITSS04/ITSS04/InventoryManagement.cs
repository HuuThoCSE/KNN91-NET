using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeNguyenQuangBinh_21022010
{
    public partial class InventoryManagement : Form
    {
        public InventoryManagement()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        string constring = "server=QBGamer;database=ITSS04DATA;uid=sa;pwd=1";
        private void InventoryManagement_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(constring);
                conn.Open();
                string query = "select\r\n\tp.ID as PartID,\r\n\tp.Name as PartName,\r\n\ttt.Name as TransactionType,\r\n\tformat(o.Date,'yyyy-MM-dd') as Date,\r\n\toi.Amount,\r\n\tcoalesce(s.Name,w1.Name) as Source,\r\n\tw2.Name as Destination\r\nfrom Orders as o\r\nleft join OrderItems as oi on o.ID=oi.OrderID\r\nleft join Parts as p on p.ID=oi.PartID\r\nleft join TransactionTypes as tt on tt.ID=o.TransactionTypeID\r\nleft join Suppliers as s on s.ID=o.SupplierID\r\nleft join Warehouses as w1 on w1.ID=o.SourceWarehouseID\r\nleft join Warehouses as w2 on w2.ID=o.DestinationWarehouseID";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dataGridView.Rows.Add(
                        reader[0],
                        reader[1],
                        reader[2],
                        reader[3],
                        reader[4],
                        reader[5],
                        reader[6],
                        "Edit",
                        "Remove"
                        );
                }
                reader.Close();
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells[2].Value.ToString() == "Purchase")
                    {
                        row.Cells[4].Style.BackColor = Color.LightGreen;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void warehouseManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            WarehouseManagement warehouseManagement = new WarehouseManagement();
            warehouseManagement.Show();
        }

        private void InventoryManagement_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
