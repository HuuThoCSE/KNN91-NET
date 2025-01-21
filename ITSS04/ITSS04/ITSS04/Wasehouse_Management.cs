using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITSS04
{
    public partial class Wasehouse_Management : Form
    {
        string action = "";
        string id_order = "";
        SqlConnection conn;
        public Wasehouse_Management(string act)
        {
            InitializeComponent();
            action = act;
            id_order = Storage.id_order;
        }
        private bool connect()
        {
            try
            {
                (conn = new SqlConnection(sql_cf.strConn)).Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connect database fail");
                return false;
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
                "\r\nwhere wh.NAME = '"+sw+"'";
                SqlDataAdapter sda_part = new SqlDataAdapter(sql_part, conn);
            
                DataTable dt_part = new DataTable();
                
                sda_part.Fill(dt_part);
            if (dt_part.Rows.Count > 0 )
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
        public void load_form_add()
        {
            if (connect())
            {
                string sql_wh = "select * from warehouses";
               
                
                SqlDataAdapter sda_wh = new SqlDataAdapter(sql_wh, conn);
               

                DataTable dt_sw = new DataTable();
                DataTable dt_dw = new DataTable();
                

                sda_wh.Fill(dt_sw);
                sda_wh.Fill(dt_dw);
                

                cbb_sw.DataSource = dt_sw;
                cbb_sw.DisplayMember = "name";
                cbb_sw.ValueMember = "id";

                cbb_dw.DataSource = dt_dw;
                cbb_dw.DisplayMember = "name";
                cbb_dw.ValueMember = "id";


                load_part();
                display_cbb_batchname();    
            }
        }

        private void Wasehouse_Management_Load(object sender, EventArgs e)
        {
            load_form_add();
        }
        public void display_cbb_batchname()
        {
            string sql_bn = "select distinct batchnumber from orderitems where partid = " + cbb_pn.SelectedValue;
            SqlDataAdapter sda_bn = new SqlDataAdapter(sql_bn, conn);
            DataTable dt_bn = new DataTable();
            sda_bn.Fill(dt_bn);
            cbb_batnum.DataSource = dt_bn;
            cbb_batnum.DisplayMember = "batchnumber";
            cbb_batnum.ValueMember = "batchnumber";
            foreach(DataRow dr in dt_bn.Rows)
            {
                if ( Convert.ToInt32( dr[0].ToString()) == 0)
                {
                    cbb_batnum.Enabled = false;
                }
                else
                {
                    cbb_batnum.Enabled = true;
                }
            }
            
        }
        public Boolean check_wasehouse()
        {
            if(cbb_sw.Text == cbb_dw.Text)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void cbb_pn_SelectionChangeCommitted(object sender, EventArgs e)
        {
            display_cbb_batchname();
        }

        private void bt_sub_Click(object sender, EventArgs e)
        {
            if(check_wasehouse())
            {
                string sw = cbb_sw.SelectedValue.ToString();
                string dw = cbb_dw.SelectedValue.ToString();
                
                string date = dtp.Text;
                Random rd = new Random();
                int idran = rd.Next(100, 1000);
                string sql = "insert into orders values" +
                    "('ord" + idran + "','tran01','sup01','" + sw + "','" + dw + "','" + date + "')";
                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.ExecuteNonQuery();
                int count = 0;
                for (int i = 0;i<dgv_partlist.RowCount-1;i++)
                {
                    string id_part = dgv_partlist.Rows[i].Tag.ToString();
                    string sql_dgv = "insert into orderitems values" +
                        "('ord"+ idran + "'," + id_part + "," + dgv_partlist.Rows[i].Cells[1].Value+","+ dgv_partlist.Rows[i].Cells[2].Value + " )";
                    SqlCommand cmd_dgv = new SqlCommand(sql_dgv, conn);
                    int kq = cmd_dgv.ExecuteNonQuery();
                    if(kq > 0)
                    {
                        count++;
                    }    
                }
                if(count == dgv_partlist.RowCount -1)
                {
                    MessageBox.Show("Submit success");
                    Form1 f = new Form1();
                    f.Show();
                    this.Close();
                }    
            }
            else
            {
                MessageBox.Show("Source Wavehouse và Destination Wasehouse phải khác nhau");
            }
        }

        private void txt_am_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }    
        }

        private void cbb_sw_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //load_part();
        }
        public void add_dgv(int row)
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
            dgv_partlist.Rows[row].Tag = cbb_pn.SelectedValue;

        }
        private void bt_add_Click(object sender, EventArgs e)
        {
            string partname = cbb_pn.Text.ToString();
            string batchnum = cbb_batnum.SelectedValue.ToString();
            string amount =txt_am.Text;
            if(!string.IsNullOrEmpty(partname) && !string.IsNullOrEmpty(amount))
            {
                if(dgv_partlist.Rows.Count > 1)
                {
                    for (int i = 0; i < dgv_partlist.RowCount - 1; i++)
                    {
                        if (partname == dgv_partlist.Rows[i].Cells[0].Value.ToString()
                            && batchnum == dgv_partlist.Rows[i].Cells[1].Value.ToString())
                        {
                            add_dgv(i);
                            break;
                        }
                        else if(i== dgv_partlist.RowCount - 2)
                        {
                            int n = dgv_partlist.Rows.Add();
                            add_dgv(n);
                            break;
                        }

                    }
                }
                else
                {
                    int n = dgv_partlist.Rows.Add();
                    add_dgv(n);
                }
            }

        }

        private void cbb_sw_SelectedValueChanged(object sender, EventArgs e)
        {
            load_part();
        }

        private void dgv_partlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 3)
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc remove part này?","xác nhận",MessageBoxButtons.YesNo);
                if(dr == DialogResult.Yes) { dgv_partlist.Rows.RemoveAt(e.RowIndex); }
                
            }    
        }

        private void bt_can_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Mọi dữ liệu chưa được lưu. xác nhận thoát", "xác nhận", MessageBoxButtons.YesNo);
            if(dr == DialogResult.Yes)
            {
                Form1 f = new Form1();
                f.Show();
                this.Hide();
            }
        }
    }
}
