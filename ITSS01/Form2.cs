using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Net.WebRequestMethods;

namespace ITSS01
{
    public partial class Form2 : Form
    {
        string action = "";
        string id_order = "";
        SqlConnection conn;
        public Form2(string act)
        {
            InitializeComponent();
            action = act;
            id_order = Storages.id_order;

        }

        public Form2()
        {
        }

        SqlDataAdapter sda;
        SqlCommand cmd;
        private bool connect()
        {
            try
            {
                (conn = new SqlConnection(sql_cf.strConn)).Open();
                return true;
            }
            catch
            {
                MessageBox.Show("Connect databse fail");
                return false;
            }
        }
        private void load_form()
        {
            if (connect())
            {
                string sql_sup = "Select * from SUPPLIERS ";
                string sql_wh = "select * from WAREHOUSES ";
                string sql_part = "select * from PARTS  ";

                SqlDataAdapter sda_sup = new SqlDataAdapter(sql_sup, conn);
                SqlDataAdapter sda_wh = new SqlDataAdapter(sql_wh, conn);
                SqlDataAdapter sda_part = new SqlDataAdapter(sql_part, conn);

                DataTable dt_sup = new DataTable();
                DataTable dt_wh = new DataTable();
                DataTable dt_part = new DataTable();

                sda_sup.Fill(dt_sup);
                sda_wh.Fill(dt_wh);
                sda_part.Fill(dt_part);

                cbb_sup.DataSource = dt_sup;
                cbb_sup.DisplayMember = "name";
                cbb_sup.ValueMember = "id";

                cbb_wh.DataSource = dt_wh;
                cbb_wh.DisplayMember = "name";
                cbb_wh.ValueMember= "id";

                cbb_pn.DataSource = dt_part;
                cbb_pn.DisplayMember = "name";
                cbb_pn.ValueMember = "id";
            }
        }

        private void add_row_dgv(int row)
        {
            dgv_partlist.Rows[row].Cells[0].Value = cbb_pn.Text.ToString();
            dgv_partlist.Rows[row].Cells[1].Value = txt_bat.Text.ToString();
            dgv_partlist.Rows[row].Cells[2].Value = txt_am.Text.ToString();
            dgv_partlist.Rows[row].Cells[3].Value = "Remove";

            dgv_partlist.Rows[row].Tag = cbb_pn.SelectedValue.ToString();

            dgv_partlist.Rows[row].Cells[3].Style.ForeColor = Color.Blue;
            dgv_partlist.Rows[row].Cells[3].Style.Font = new Font(dgv_partlist.Font, FontStyle.Underline);
        }

        public string check_required()
        {
            string name_pn = cbb_pn.Text;

            string query = "SELECT BATCHNUMBERHASREQUIRED FROM parts WHERE name = @name";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@name", name_pn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Convert.ToInt32(reader["BATCHNUMBERHASREQUIRED"]) == 0
                            ? "notrequired"
                            : "required";
                    }
                }
            }

            return null;
        }




        private void Form2_Load(object sender, EventArgs e)
        {
            load_form();
        }

        private void btn_can_Click(object sender, EventArgs e)
        {
            if (dgv_partlist.Rows.Count > 1) 
            {
                DialogResult result = MessageBox.Show("if you exit? everything will not be saved", "Notification", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    Form1 im = new Form1();
                    im.Show();
                    this.Close();
                }
            }
            else
            {
                Form1 im = new Form1();
                im.Show();
                this.Close();
            }
        }

        private void btn_sub_Click(object sender, EventArgs e)
        {
            try
            {
                if (!connect()) return;

                Random random = new Random();
                int successfulRows = 0;

                // Bắt đầu transaction để đảm bảo toàn bộ lệnh thực thi đồng bộ
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    foreach (DataGridViewRow row in dgv_partlist.Rows)
                    {
                        if (row.IsNewRow) continue; // Bỏ qua dòng trống cuối của DataGridView

                        // Tạo ID ngẫu nhiên
                        int orderId = random.Next(100, 1000);
                        string orderCode = $"ord{orderId}";
                        string supplierId = cbb_sup.SelectedValue.ToString();
                        string warehouseId = cbb_wh.SelectedValue.ToString();
                        string date = date_dtp.Value.ToString("yyyy-MM-dd");
                        string partId = row.Tag.ToString();
                        string batchNumber = row.Cells[1].Value?.ToString();
                        string amount = row.Cells[2].Value?.ToString();

                        // Câu lệnh thêm vào bảng ORDERS
                        string insertOrder = @"
                    INSERT INTO ORDERS (ID, TransactionType, SupplierID, SourceWarehouseID, Destinationwarehouseid, Date) 
                    VALUES (@ID, 'tran02', @SupplierID, @SourceWarehouseID, @Destinationwarehouseid, @Date)";

                        // Câu lệnh thêm vào bảng ORDERITEMS
                        string insertOrderItem = @"
                    INSERT INTO ORDERITEMS (OrderID, PartID, BatchNumber, Amount) 
                    VALUES (@OrderID, @PartID, @BatchNumber, @Amount)";

                        // Thực thi lệnh thêm ORDER
                        using (SqlCommand cmdOrder = new SqlCommand(insertOrder, conn, transaction))
                        {
                            cmdOrder.Parameters.AddWithValue("@ID", orderCode);
                            cmdOrder.Parameters.AddWithValue("@SupplierID", supplierId);
                            cmdOrder.Parameters.AddWithValue("@SourceWarehouseID", warehouseId);
                            cmdOrder.Parameters.AddWithValue("@Destinationwarehouseid", warehouseId);
                            cmdOrder.Parameters.AddWithValue("@Date", date);
                            cmdOrder.ExecuteNonQuery();
                        }

                        // Thực thi lệnh thêm ORDERITEMS
                        using (SqlCommand cmdOrderItem = new SqlCommand(insertOrderItem, conn, transaction))
                        {
                            cmdOrderItem.Parameters.AddWithValue("@OrderID", orderCode);
                            cmdOrderItem.Parameters.AddWithValue("@PartID", partId);
                            cmdOrderItem.Parameters.AddWithValue("@BatchNumber", batchNumber);
                            cmdOrderItem.Parameters.AddWithValue("@Amount", amount);
                            cmdOrderItem.ExecuteNonQuery();
                        }

                        successfulRows++;
                    }

                    // Commit transaction nếu tất cả các lệnh thành công
                    transaction.Commit();
                }

                // Kiểm tra số hàng được thêm thành công
                if (successfulRows == dgv_partlist.Rows.Count - 1)
                {
                    MessageBox.Show("Thêm thành công.");
                    Form1 im = new Form1();
                    im.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Có vài hàng bị lỗi để thêm.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Thêm thất bại: {ex.Message}");
            }
        }

        private void dgv_partlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                dgv_partlist.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void bn_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            // Kiểm tra các trường bắt buộc
            if ((check_required() == "required" && (string.IsNullOrEmpty(txt_bat.Text) || string.IsNullOrEmpty(txt_am.Text))) ||
                (check_required() == "notrequired" && string.IsNullOrEmpty(txt_am.Text)))
            {
                MessageBox.Show("Please enter Batch Number or Amount");
                return;
            }

            // Duyệt qua các dòng của DataGridView để xử lý logic thêm mới hoặc thay thế
            for (int i = 0; i < dgv_partlist.RowCount - 1; i++)
            {
                if (txt_bat.Text == dgv_partlist.Rows[i].Cells[0].Value.ToString())
                {
                    // Nếu part không yêu cầu batch number
                    if (check_required() == "notrequired")
                    {
                        add_row_dgv(i);
                        return;
                    }

                    // Nếu part yêu cầu batch number và batch number trùng
                    if (txt_bat.Text == dgv_partlist.Rows[i].Cells[1].Value.ToString())
                    {
                        add_row_dgv(i);
                        return;
                    }

                    // Nếu part yêu cầu batch number nhưng batch number khác
                    int newRowIndex = dgv_partlist.Rows.Add();
                    add_row_dgv(newRowIndex);
                    return;
                }
            }

            // Nếu không trùng với bất kỳ dòng nào, thêm mới
            int rowIndex = dgv_partlist.Rows.Add();
            add_row_dgv(rowIndex);
        }
    }
}
