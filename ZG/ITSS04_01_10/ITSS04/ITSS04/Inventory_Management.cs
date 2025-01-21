using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITSS04
{
    public partial class Inventory_Management : Form
    {
        public Inventory_Management()
        {
            InitializeComponent();
        }

        SqlDataAdapter sda;
        SqlConnection conn;
        SqlCommand cmd;

        private void load_dgv()
        {
            conn = ITSS04.conn.GetCnn(); // Ensure you are using the correct namespace and method to get the connection
            if (conn != null)
            {
                try
                {
                    string sql = "select  p.NAME, tr.NAME, ord.DATES, ordi.AMOUNT, " +
                        "\r\n(select name from WAREHOUSES where id=ord.SOURCEWAREHOUSEID)," +
                        "\r\n(select name from WAREHOUSES where id=ord.DESTINATIONWAREHOUSEID), ord.id" +
                        "\r\nfrom  parts p" +
                        "\r\njoin ORDERITEMS ordi  on p.id = ordi.PARTID" +
                        "\r\njoin ORDERS ord on ord.id = ordi.ORDERID" +
                        "\r\njoin TRANSACTIONTYPES tr on ord.id = tr.id" +
                        "\r\norder by ord.DATES, case when tr.NAME = 'Purchase Order' then 0 else  1 end";

                    sda = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dgv_list.Rows.Clear();
                    foreach (DataRow dr in dt.Rows)
                    {
                        int n = dgv_list.Rows.Add();
                        for (int i = 0; i < dgv_list.Columns.Count; i++)
                        {
                            if (i == 2)
                            {
                                dgv_list.Rows[n].Cells[i].Value = ((DateTime)dr[i]).ToString("yyyy-MM-dd");
                                continue;
                            }
                            dgv_list.Rows[n].Cells[i].Value = dr[i].ToString();
                        }
                        dgv_list.Rows[n].Tag = dr[6].ToString();

                        if (dgv_list.Rows[n].Cells[1].Value.ToString() == "Purchase Order")
                        {
                            dgv_list.Rows[n].Cells[3].Style.BackColor = Color.GreenYellow;
                        }

                        dgv_list.Rows[n].Cells[6].Value = "Edit";
                        dgv_list.Rows[n].Cells[7].Value = "Remove";

                        dgv_list.Rows[n].Cells[6].Style.ForeColor = Color.Blue;
                        dgv_list.Rows[n].Cells[7].Style.ForeColor = Color.Blue;

                        dgv_list.Rows[n].Cells[6].Style.Font = new Font(dgv_list.Font, FontStyle.Underline);
                        dgv_list.Rows[n].Cells[7].Style.Font = new Font(dgv_list.Font, FontStyle.Underline);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Failed to connect to the database.");
            }
        }

        private void Inventory_Management_Load(object sender, EventArgs e)
        {
            load_dgv();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void purchaseOrderManagementToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id_order = dgv_list.Rows[e.RowIndex].Tag.ToString();

            if (e.ColumnIndex == 7)
            {
                string del_oi = "delete from ORDERITEMS where ORDERID = '" + id_order + "'";
                string del_o = "delete from ORDERS where ID = '" + id_order + "'";

                SqlCommand cmd_oi = new SqlCommand(del_oi, conn);
                SqlCommand cmd_o = new SqlCommand(del_o, conn);

                DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa", "Đồng ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    cmd_oi.ExecuteNonQuery();
                    int deleted = cmd_o.ExecuteNonQuery();

                    if (deleted > 0)
                    {
                        MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        load_dgv();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (e.ColumnIndex == 6)
                {
                    Storage.id_order = id_order;
                    Storage.sw = dgv_list.Rows[e.RowIndex].Cells[4].Value.ToString();
                    Storage.dw = dgv_list.Rows[e.RowIndex].Cells[5].Value.ToString();
                    Storage.date = dgv_list.Rows[e.RowIndex].Cells[2].Value.ToString();
                    update
                }
            }
        }

        private void menu_whm_Click(object sender, EventArgs e)
        {
            Wasehouse_Management wm = new Wasehouse_Management();
            wm.Show();
            this.Hide();
        }
    }
}
