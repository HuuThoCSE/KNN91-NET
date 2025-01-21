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
                conn = new SqlConnection();
                conn.ConnectionString = "server=ThisPC\\MSSL2022; database=ITS01DATA; uid=sa; pwd=123456";
                conn.Open();
            }
            catch
            {
                MessageBox.Show("Connect databse fail");
                return false;
            }
            return true;
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
    }
}
