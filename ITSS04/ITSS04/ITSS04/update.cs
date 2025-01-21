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

namespace ITSS04
{
    public partial class update : Form
    {
        string id_order, sw, dw, date;
        SqlConnection conn;
        public update()
        {
            InitializeComponent();
            id_order = Storage.id_order;
            sw = Storage.sw;
            dw = Storage.dw;
            date = Storage.date;
        }
        public bool connect()
        {
            try
            {
                (conn = new SqlConnection(sql_cf.strConn)).Open();
                return true;
            }
            catch { 
                return false;
            }
        }
        public void load_numbatch()
        {
            string sql_bn = "select distinct batchnumber from orderitems where partid = " + cbb_pn.SelectedValue;
            SqlDataAdapter sda_bn = new SqlDataAdapter(sql_bn, conn);
            DataTable dt_bn = new DataTable();
            sda_bn.Fill(dt_bn);
            cbb_batnum.DataSource = dt_bn;
            cbb_batnum.DisplayMember = "batchnumber";
            cbb_batnum.ValueMember = "batchnumber";
            foreach (DataRow dr in dt_bn.Rows)
            {
                if (Convert.ToInt32(dr[0].ToString()) == 0)
                {
                    cbb_batnum.Enabled = false;
                }
                else
                {
                    cbb_batnum.Enabled = true;
                }
            }

        }
        public void load_part()
        {
            cbb_batnum.Enabled = true;
            cbb_pn.Enabled = true;
            string sw;
            sw = cbb_sw.Text;
            string sql_part = "select distinct p.id, p.name " +
                "\r\nfrom parts p" +
                "\r\njoin orderitems ordi on p.id = ordi.partid" +
                "\r\njoin ORDERS ord on ord.ID = ordi.ORDERID" +
                "\r\njoin WAREHOUSES wh on wh.ID = ord.SOURCEWAREHOUSEID " +
                "\r\nwhere wh.NAME = '" + sw + "'";
            SqlDataAdapter sda_part = new SqlDataAdapter(sql_part, conn);

            DataTable dt_part = new DataTable();

            sda_part.Fill(dt_part);
            if (dt_part.Rows.Count > 0)
            {
                cbb_batnum.Enabled = true;
                cbb_pn.Enabled = true;

                cbb_pn.DataSource = dt_part;
                cbb_pn.DisplayMember = "name";
                cbb_pn.ValueMember = "id";
            }
            else
            {
                cbb_pn.Text = "";
                cbb_batnum.Text = "";
                cbb_pn.Enabled = false;
                cbb_batnum.Enabled = false;
            }
        }
        public void load_info()
        {
            if(connect())
            {
                string sql_wh = "select * from warehouses";
                SqlDataAdapter adp_wh = new SqlDataAdapter(sql_wh, conn);

                DataTable dt_sw = new DataTable();
                DataTable dt_dw = new DataTable();
                adp_wh.Fill(dt_sw);
                adp_wh.Fill(dt_dw);

                cbb_sw.DataSource = dt_sw;
                cbb_sw.ValueMember = "id";
                cbb_sw.DisplayMember = "name";

                cbb_dw.DataSource = dt_dw;
                cbb_dw.ValueMember = "id";
                cbb_dw.DisplayMember = "name";

                cbb_sw.Text = sw;
                cbb_dw.Text = dw;
                dtp.Text = date.ToString();
            }
            

        }
        public Boolean check_wasehouse()
        {
            if (cbb_sw.Text == cbb_dw.Text)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        public void load_dgv()
        {
            string sql = "select p.NAME, ordi.BATCHNUMBER, ordi.AMOUNT, p.ID from PARTS p" +
                "\r\njoin ORDERITEMS ordi on p.ID = ordi.PARTID" +
                "\r\njoin ORDERS ord on ord.ID = ordi.ORDERID" +
                "\r\nwhere ord.id= '" + id_order + "'";

            SqlDataAdapter sda = new SqlDataAdapter(sql,conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
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

        private void update_Load(object sender, EventArgs e)
        {
            load_info();
            load_part();
            load_numbatch();
            load_dgv();
        }

        private void cbb_pn_SelectionChangeCommitted(object sender, EventArgs e)
        {
            load_numbatch();
        }

        private void cbb_sw_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_part();
        }
     
        private void dgv_partlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 3)
            {
                DialogResult dr = MessageBox.Show("bạn có muốn remove", "xác nhận", MessageBoxButtons.YesNo);
                if(dr == DialogResult.Yes)
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
            string batchnum = cbb_batnum.SelectedValue.ToString();
            string amount = txt_am.Text;

            dgv_partlist.Rows[row].Cells[0].Value = partname;
            dgv_partlist.Rows[row].Cells[1].Value = batchnum;
            dgv_partlist.Rows[row].Cells[2].Value = amount;

            dgv_partlist.Rows[row].Cells[3].Value = "Remove";

            dgv_partlist.Rows[row].Cells[3].Style.ForeColor = Color.Blue;
            dgv_partlist.Rows[row].Cells[3].Style.Font = new Font(dgv_partlist.Font, FontStyle.Underline);
        }
        private void bt_add_Click(object sender, EventArgs e)
        {
            string partname = cbb_pn.Text.ToString();
            string batchnum = cbb_batnum.SelectedValue.ToString();
            string amount = txt_am.Text;
            if (!string.IsNullOrEmpty(partname) && !string.IsNullOrEmpty(amount))
            {
                if(dgv_partlist.Rows.Count > 1)
                {
                    for(int i = 0;i< dgv_partlist.Rows.Count -1;i++)
                    {
                        if(partname == dgv_partlist.Rows[i].Cells[0].Value.ToString()
                            && batchnum == dgv_partlist.Rows[i].Cells[1].Value.ToString()) 
                        {
                            add_list(i);
                            dgv_partlist.Rows[i].Tag = "update";
                            break;
                        }
                        else if(i== dgv_partlist.Rows.Count - 2)
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
        public int take_idpart(string name)
        {
            string sql = "select id from parts where name='"+name+"'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader r = cmd.ExecuteReader();
            if (r.Read())
            {
               
                int id= Convert.ToInt32(r["id"]);
                r.Close();
                return id;
                
            }
            else
            {
                r.Close();
                return -1;
                
            }
        }
        private void bt_sub_Click(object sender, EventArgs e)
        {
            if(check_wasehouse())
            {
                //update order
                string sql_ord = "update ORDERS set SOURCEWAREHOUSEID = '" + cbb_sw.SelectedValue + "', " +
                    "\r\nDESTINATIONWAREHOUSEID = '" + cbb_dw.SelectedValue + "', " +
                    "\r\ndate = '" + dtp.Text + "'" +
                    "\r\nwhere id = '" + id_order + "'";
                SqlCommand cmd_ord = new SqlCommand(sql_ord, conn);
                cmd_ord.ExecuteNonQuery();
                for(int i = 0;i< dgv_partlist.RowCount-1;i++)
                {
                    string sql_ordi = "";
                    
                    int idpart = take_idpart(dgv_partlist.Rows[i].Cells[0].Value.ToString());
                    if (dgv_partlist.Rows[i].Tag=="insert")
                    {
                         sql_ordi= "insert into orderitems values" +
                            "('" + id_order + "'," + idpart + "," + dgv_partlist.Rows[i].Cells[1].Value.ToString() + "," + dgv_partlist.Rows[i].Cells[2].Value.ToString() + ")";
                    }
                    else if(dgv_partlist.Rows[i].Tag == "update")
                    {
                         sql_ordi = "update orderitems " +
                                    "set partid= " + idpart +
                                    ",  BATCHNUMBER=" + dgv_partlist.Rows[i].Cells[1].Value.ToString() +
                                    ",  amount = " + dgv_partlist.Rows[i].Cells[2].Value.ToString() +
                                    " where orderid='" + id_order+"'";
                    }
                    else
                    {
                        continue;
                    }
                    SqlCommand cmd = new SqlCommand(sql_ordi, conn);
                    int res = cmd.ExecuteNonQuery();
                }
                MessageBox.Show("submit thanh cong");
                Form1 f = new Form1();
                f.Show();
                this.Close();
            }    
        }

        private void bt_can_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Close();
        }
    }
}
