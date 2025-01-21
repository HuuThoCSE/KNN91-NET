using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITSS04
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
                (conn = new SqlConnection(sql_cf.strConn)).Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connect database fail");
                return false;
            }
            return true;
        }
        public void load_dtgirdview()
        {

            if (connect())
            {
                string select = "select  p.NAME, tr.NAME, ord.DATE, ordi.AMOUNT, " +
                    "\r\n(select name from WAREHOUSES where id=ord.SOURCEWAREHOUSEID)," +
                    "\r\n(select name from WAREHOUSES where id=ord.DESTINATIONWAREHOUSEID), ord.id" +
                    "\r\nfrom  parts p" +
                    "\r\njoin ORDERITEMS ordi  on p.id = ordi.PARTID" +
                    "\r\njoin ORDERS ord on ord.id = ordi.ORDERID" +
                    "\r\njoin TRANSACTIONTYPES tr on ord.transactiontype = tr.id" +
                    "\r\norder by ord.DATE, case when tr.NAME = 'Purchase Order' then 0 else  1 end";
                sda = new SqlDataAdapter(select, conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dgv_list.Rows.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    int n = dgv_list.Rows.Add();
                    for (int i = 0; i <= 5; i++)
                    {
                        if (i == 2)
                        {
                            dgv_list.Rows[n].Cells[i].Value = ((DateTime)dr[i]).ToString("yyyy-MM-dd");
                            continue;
                        }
                        dgv_list.Rows[n].Cells[i].Value = dr[i].ToString();
                        dgv_list.Rows[n].Tag = dr[6].ToString();

                    }

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
        }

        private void menu_whm_Click(object sender, EventArgs e)
        {
            Wasehouse_Management wm = new Wasehouse_Management("add");
            wm.Show();
            this.Hide();
        }
     
        private void dgv_list_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // take id order 
            string id_order = dgv_list.Rows[e.RowIndex].Tag.ToString();

            
            if (e.ColumnIndex == 7)
            {
                string del_orderitem = "delete ORDERITEMS where ORDERID= '" + id_order + "'";
                string del_order = "delete ORDERS where ID= '" + id_order + "'";
               

                SqlCommand sqlcmd_orderitem = new SqlCommand(del_orderitem, conn);
                SqlCommand sqlcmd_order = new SqlCommand(del_order, conn);
          

                DialogResult dr = MessageBox.Show("Are you sure to delete this?", "confirm delection", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    sqlcmd_orderitem.ExecuteNonQuery();
                   int deleted = sqlcmd_order.ExecuteNonQuery();
                  
                    if (deleted > 0)
                    {
                        MessageBox.Show("Delete successful", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        load_dtgirdview();
                    }
                    else
                    {
                        MessageBox.Show("Delete fail", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            if(e.ColumnIndex == 6)
            {
                Storage.id_order = id_order;
                Storage.sw = dgv_list.Rows[e.RowIndex].Cells[4].Value.ToString();
                Storage.dw = dgv_list.Rows[e.RowIndex].Cells[5].Value.ToString();
                Storage.date = dgv_list.Rows[e.RowIndex].Cells[2].Value.ToString();
                update ud = new update();
                ud.Show();  
                this.Hide();
            }    
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            load_dtgirdview();
        }

        private void menuStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
