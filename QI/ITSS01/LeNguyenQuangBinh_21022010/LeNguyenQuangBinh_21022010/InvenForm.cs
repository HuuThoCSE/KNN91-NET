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
    public partial class InvenForm : Form
    {
        public InvenForm()
        {
            InitializeComponent();
        }
        SqlConnection connection;
        private void InvenForm_Load(object sender, EventArgs e)
        {
            try
            {
                string connectString = "server=QBGamer;database=ITSS01DATA;uid=sa;pwd=1";
                connection = new SqlConnection(connectString);
                connection.Open();
                string query = "select\r\n\tp.Name,\r\n\ttt.Name,\r\n\tformat(o.Date,'yyyy-MM-dd'),\r\n\toi.Amount,\r\n\tcoalesce(s.Name,w1.Name),\r\n\tw2.Name\r\nfrom Orders as o\r\nleft join OrderItems as oi on o.ID=oi.OrderID\r\nleft join Parts as p on p.ID=oi.PartID\r\nleft join TransactionTypes as tt on tt.ID=o.TransactionTypeID\r\nleft join Suppliers as s on s.ID=o.SupplierID\r\nleft join Warehouses as w1 on w1.ID=o.SourceWarehouseID\r\nleft join Warehouses as w2 on w2.ID=o.DestinationWarehouseID";
                SqlCommand cmd = new SqlCommand(query, connection);
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
                        "Edit",
                        "Remove"
                    );
                }
                reader.Close();
                dataGridView.Sort(dataGridView.Columns[2], ListSortDirection.Ascending);
                dataGridView.Sort(dataGridView.Columns[1], ListSortDirection.Ascending);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error!");
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e != null)
                {
                    if (e.ColumnIndex == 7)
                    {
                        if(MessageBox.Show("Are you sure want to remove this!","Aler!",MessageBoxButtons.OKCancel)==DialogResult.OK)
                        {
                            dataGridView.Rows.Remove(dataGridView.Rows[e.RowIndex]);
                            //string query =""
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }
        }

        private void purchaseOrderManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            purchaseOrder.Show();
        }
    }
}
