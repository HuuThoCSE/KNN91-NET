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

namespace ITSS01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlCommand cmd;
        SqlConnection cnn;

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void load_data()
        {
            string strConn = "server=thisPC\\THISPC;database=ITS01DATA;uid=sa;pwd=123456";

            try
            {
                using (SqlConnection cnn = new SqlConnection(strConn))
                {
                    cnn.Open();

                    string qr = @"
                        SELECT
                            p.Name,
                            tt.Name AS TransactionType,
                            FORMAT(o.Date, 'yyyy-MM-dd') AS OrderDate,
                            oi.Amount,
                            COALESCE(s.Name, w1.Name) AS SupplierOrWarehouse,
                            w2.Name AS DestinationWarehouse
                        FROM Orders o
                        LEFT JOIN OrderItems oi ON o.ID = oi.OrderID
                        LEFT JOIN Parts p ON p.ID = oi.PartID
                        LEFT JOIN TransactionTypes tt ON tt.ID = o.TransactionTypeID
                        LEFT JOIN Suppliers s ON s.ID = o.SupplierID
                        LEFT JOIN Warehouses w1 ON w1.ID = o.SourceWarehouseID
                        LEFT JOIN Warehouses w2 ON w2.ID = o.DestinationWarehouseID";

                    using (SqlCommand cmd = new SqlCommand(qr, cnn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dgv_data.Rows.Clear();
                        while (reader.Read())
                        {
                            int rowIndex = dgv_data.Rows.Add(
                                reader["Name"].ToString(),
                                reader["TransactionType"].ToString(),
                                reader["OrderDate"].ToString(),
                                reader["Amount"].ToString(),
                                reader["SupplierOrWarehouse"].ToString(),
                                reader["DestinationWarehouse"].ToString(),
                                "Edit",
                                "Remove"
                            );

                            // Đánh dấu hàng nếu là "Purchase Order"
                            if (reader["TransactionType"].ToString() == "Purchase Order")
                            {
                                dgv_data.Rows[rowIndex].Cells[3].Style.BackColor = Color.GreenYellow;
                            }

                            // Định dạng các cột "Edit" và "Remove"
                            foreach (int colIndex in new int[] { 6, 7 })
                            {
                                var cell = dgv_data.Rows[rowIndex].Cells[colIndex];
                                cell.Style.ForeColor = Color.Blue;
                                cell.Style.Font = new Font(dgv_data.Font, FontStyle.Underline);
                            }
                        }
                    }
                }

                // Sắp xếp DataGridView
                //dgv_data.Sort(dgv_data.Columns["OrderDate"], ListSortDirection.Ascending);
                //dgv_data.Sort(dgv_data.Columns["TransactionType"], ListSortDirection.Ascending);

                dgv_data.Sort(dgv_data.Columns[2], ListSortDirection.Ascending);
                dgv_data.Sort(dgv_data.Columns[1], ListSortDirection.Ascending);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            load_data();
        }

        private void dgv_data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 7 && e.RowIndex >= 0)
            {
                using (SqlConnection conn = new SqlConnection(sql_data.strConn))
                {
                    try
                    {
                        conn.Open();


                        // Lấy tên phần từ cột đầu tiên (giả sử tên phần là khóa để xác định order)
                        string partName = dgv_data.Rows[e.RowIndex].Cells[0].Value.ToString();
                        string idOrder = "";

                        // Lấy ORDERID dựa vào tên phần
                        string sqlSelect = "SELECT ORDERID FROM ORDERITEMS ORI JOIN PARTS P ON P.ID = ORI.PARTID WHERE P.NAME = '" + partName + "'";
                        using (SqlCommand cmd = new SqlCommand(sqlSelect, conn))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                idOrder = reader["ORDERID"].ToString();
                            }
                            reader.Close();
                        }

                        // Tạo các câu lệnh DELETE
                        string sqlDeleteOrderItems = "DELETE FROM ORDERITEMS WHERE ORDERID = '" + idOrder + "'";
                        string sqlDeleteOrders = "DELETE FROM ORDERS WHERE ID = '" + idOrder + "'";
                        string sqlDeleteParts = "DELETE FROM PARTS WHERE NAME = '" + partName + "'";

                        // Hỏi người dùng xác nhận xóa
                        DialogResult dr = MessageBox.Show("Are you sure to delete this?", "Confirm deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            using (SqlCommand cmdDelete1 = new SqlCommand(sqlDeleteOrderItems, conn))
                            using (SqlCommand cmdDelete2 = new SqlCommand(sqlDeleteOrders, conn))
                            using (SqlCommand cmdDelete3 = new SqlCommand(sqlDeleteParts, conn))
                            {
                                cmdDelete1.ExecuteNonQuery();
                                cmdDelete2.ExecuteNonQuery();
                                int deleted = cmdDelete3.ExecuteNonQuery();

                                if (deleted > 0)
                                {
                                    MessageBox.Show("Delete successful", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    load_data();  // load lại dữ liệu sau khi xóa
                                }
                                else
                                {
                                    MessageBox.Show("Delete failed", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }

                    }
                    catch (Exception ex) {
                        MessageBox.Show(ex.Message, "Error!");
                    }
                }
            }
        }

        private void menu_wm_Click(object sender, EventArgs e)
        {
            FrmOrder frmOrder = new FrmOrder();
            frmOrder.Show();
            this.Hide();
        }

        private void menu_pom_Click(object sender, EventArgs e)
        {
            FrmOrder frmOrder = new FrmOrder();
            frmOrder.Show();
            this.Hide();
        }
    }
}
