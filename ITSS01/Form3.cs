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
using static System.Net.WebRequestMethods;

namespace ITSS01
{
    public partial class Form3 : Form
    {
        string id_order, sup, wh, date;
        SqlConnection conn;
        public Form3()
        {
            InitializeComponent();
            id_order = Storages.id_order;
            sup = Storages.sup;
            wh = Storages.wh;
            date = Storages.date;
        }
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

        private void dgv_partlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                DialogResult dr = MessageBox.Show("Bạn có muốn remove", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    if (dgv_partlist.Rows[e.RowIndex].Tag.ToString() == "data" || dgv_partlist.Rows[e.RowIndex].Tag.ToString() == "update")
                    {
                        string sql = "delete orderitems " +
                       "from ORDERITEMS ordi, parts p" +
                       " where ordi.PARTID = p.ID and orderid ='" + id_order +
                       "' and p.name ='" + dgv_partlist.Rows[e.RowIndex].Cells[0].Value.ToString() + "'" +
                       "and ordi.BATCHNUMBER =" + dgv_partlist.Rows[e.RowIndex].Cells[1].Value.ToString();
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();

                        dgv_partlist.Rows.RemoveAt(e.RowIndex);
                    }
                    else
                    {
                        dgv_partlist.Rows.RemoveAt(e.RowIndex);
                    }
                }

            }
        }

        public void add_list(int row)
        {
            string partname = cbb_pn.Text.ToString();
            string batchnum = txt_bn.Text.ToString();
            string amount = txt_am.Text;

            dgv_partlist.Rows[row].Cells[0].Value = partname;
            dgv_partlist.Rows[row].Cells[1].Value = batchnum;
            dgv_partlist.Rows[row].Cells[2].Value = amount;

            dgv_partlist.Rows[row].Cells[3].Value = "Remove";

            dgv_partlist.Rows[row].Cells[3].Style.ForeColor = Color.Blue;
            dgv_partlist.Rows[row].Cells[3].Style.Font = new Font(dgv_partlist.Font, FontStyle.Underline);
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            string partname = cbb_pn.Text.ToString();
            string batchnum = txt_bn.Text.ToString();
            string amount = txt_am.Text;
            if (!string.IsNullOrEmpty(partname) && !string.IsNullOrEmpty(amount))
            {
                if (dgv_partlist.Rows.Count > 1)
                {
                    for (int i = 0; i < dgv_partlist.Rows.Count - 1; i++)
                    {
                        if (partname == dgv_partlist.Rows[i].Cells[0].Value.ToString()
                            && batchnum == dgv_partlist.Rows[i].Cells[1].Value.ToString())
                        {
                            add_list(i);
                            dgv_partlist.Rows[i].Tag = "update";
                            break;
                        }
                        else if (i == dgv_partlist.Rows.Count - 2)
                        {
                            int n = dgv_partlist.Rows.Add();
                            add_list(n);
                            dgv_partlist.Rows[n].Tag = "insert";
                            break;
                        }

                    }
                }
                else
                {
                    int n = dgv_partlist.Rows.Add();
                    add_list(n);
                    dgv_partlist.Rows[n].Tag = "insert";
                }
            }
        }

        public void load_info()
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
                cbb_wh.ValueMember = "id";

                cbb_pn.DataSource = dt_part;
                cbb_pn.DisplayMember = "name";
                cbb_pn.ValueMember = "id";

                cbb_sup.Text = sup;
                cbb_wh.Text = wh;
                dtp_date.Text = date.ToString();
            }
        }

        private void btn_sub_Click(object sender, EventArgs e)
        {

        }

        public void load_dgv()
        {
            string sql = "select p.NAME, ordi.BATCHNUMBER, ordi.AMOUNT, p.ID from PARTS p" +
                "\r\njoin ORDERITEMS ordi on p.ID = ordi.PARTID" +
                "\r\njoin ORDERS ord on ord.ID = ordi.ORDERID" +
                "\r\nwhere ord.id= '" + id_order + "'";

            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                int n = dgv_partlist.Rows.Add(dr);
                dgv_partlist.Rows[n].Cells[0].Value = dr[0].ToString();
                dgv_partlist.Rows[n].Cells[1].Value = dr[1].ToString();
                dgv_partlist.Rows[n].Cells[2].Value = dr[2].ToString();

                dgv_partlist.Rows[n].Cells[3].Value = "Remove";

                dgv_partlist.Rows[n].Cells[3].Style.ForeColor = Color.Blue;
                dgv_partlist.Rows[n].Cells[3].Style.Font = new Font(dgv_partlist.Font, FontStyle.Underline);

                dgv_partlist.Rows[n].Tag = "data";
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            load_info();
            load_dgv();
        }

        public int take_idpart(string name)
        {
            string sql = "select id from parts where name='" + name + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader r = cmd.ExecuteReader();
            if (r.Read())
            {

                int id = Convert.ToInt32(r["id"]);
                r.Close();
                return id;

            }
            else
            {
                r.Close();
                return -1;

            }
        }
    }
}
