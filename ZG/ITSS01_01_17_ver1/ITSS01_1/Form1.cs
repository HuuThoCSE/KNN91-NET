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

namespace ITSS01_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn;
        SqlDataAdapter sda;
        SqlCommand cmd;

        private bool connect()
        {
            try
            {
                conn = new SqlConnection();
                conn.ConnectionString = "server=THISPC\\THISPC; database=ITS01DATA; uid=sa; pwd=123456";
                conn.Open();
            }
            catch
            {
                MessageBox.Show("Connect databse fail");
                return false;
            }
            return true;
        }

        private void dgv_t()
        {
            if (connect())
            {
                string select = "select p.Name, tr.Name, ord.date, ordi.amount," +
                    "(select name from WAREHOUSES where id = ord.SOURCEWAREHOUSEID)," +
                    "(select name from WAREHOUSES where id = ord.DESTINATIONWAREHOUSEID),ord.id " +
                    "from parts p " +
                    "join ORDERITEMS ordi on ordi.PARTID = p.id " +
                    "join ORDERS ord on ordi.ORDERID = ord.id " +
                    "join TRANSACTIONTYPES tr on ord.TRANSACTIONTYPE = tr.id " +
                    "order by ord.date, case when tr.Name ='Purchase Order' then 0 else 1 end ";
                sda = new SqlDataAdapter(select, conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dgv.Rows.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    for (int i = 0; i <= 5; i++)
                    {
                        if (i == 2)
                        {
                            dgv.Rows[n].Cells[i].Value = ((DateTime)dr[i]).ToString("yyyy-MM-dd");
                            continue;
                        }
                        dgv.Rows[n].Cells[i].Value = dr[i].ToString();
                        dgv.Rows[n].Tag = dr[6].ToString();
                    }

                    if (dgv.Rows[n].Cells[1].Value.ToString() == "Purchase Order")
                    {
                        dgv.Rows[n].Cells[3].Style.BackColor = Color.GreenYellow;
                    }

                    dgv.Rows[n].Cells[6].Value = "Edit";
                    dgv.Rows[n].Cells[7].Value = "Remove";

                    dgv.Rows[n].Cells[6].Style.ForeColor = Color.Blue;
                    dgv.Rows[n].Cells[7].Style.ForeColor = Color.Red;

                    dgv.Rows[n].Cells[6].Style.Font = new Font(dgv.Font, FontStyle.Underline);
                    dgv.Rows[n].Cells[7].Style.Font = new Font(dgv.Font, FontStyle.Underline);
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgv_t();
        }

        private void purchaseOrderManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOrder formOrder = new FormOrder();
            formOrder.Show();
            this.Hide();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id_order = dgv.Rows[e.RowIndex].Tag.ToString();

            if (e.ColumnIndex == 7)
            {
                string order_item = "Delete ORDERITEMS where ORDERID = '" + id_order + "'";
                string order = "Delete ORDERS where ID = '" + id_order + "'";

                SqlCommand sqlcmd_orderitem = new SqlCommand(order_item, conn);
                SqlCommand sqlcmd_order = new SqlCommand(order, conn);

                DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa không, really", "Cho xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    sqlcmd_orderitem.ExecuteNonQuery();
                    int delete = sqlcmd_order.ExecuteNonQuery();
                    if (delete > 0)
                    {
                        MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgv_t();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            if (e.ColumnIndex == 6)
            {
                Storages.id_order = id_order;
                Storages.sup = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();
                Storages.wh = dgv.Rows[e.RowIndex].Cells[5].Value.ToString();
                Storages.date = dgv.Rows[e.RowIndex].Cells[2].Value.ToString();
                Form3 ud = new Form3();
                ud.Show();
                this.Hide();
            }
        }
    }
}
