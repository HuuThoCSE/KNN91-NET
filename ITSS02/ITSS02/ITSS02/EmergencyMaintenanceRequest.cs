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

namespace ITSS02
{
    public partial class EmergencyMaintenanceRequest : Form
    {
        string asset_name = "";
        int asset_id;
        SqlConnection conn;
        public EmergencyMaintenanceRequest(string asset)
        {
            InitializeComponent();
            asset_name = asset;
            
        }
        public Boolean connect()
        {
            try
            {
                (conn = new SqlConnection(sql_cf.strConn)).Open();
                return true;
            }
            catch
            {

                MessageBox.Show("connect database failed");
                return false;
            }

        }

        private void EmergencyMaintenanceRequest_Load(object sender, EventArgs e)     
        {
            if (connect())
            {
                //display asset
               string select_asset = "select ass.ID, ASSETSN, ASSETNAME, dp.NAME as 'department'" +
               "\r\nfrom ASSETS ass" +
               "\r\njoin DEPARTMENTLOCATIONS dpl   on dpl.ID = ass.DEPARTMENTLOCAIONID" +
               "\r\njoin DEPARTMENTS dp on dpl.DEPARTMENTID = dp.ID" +
               "\r\nwhere ASSETNAME ='" + asset_name + "'";

                SqlCommand cmd_asset = new SqlCommand(select_asset, conn);
                SqlDataReader reader_asset = cmd_asset.ExecuteReader();
                if (reader_asset.Read())
                {
                    lb_assn.Text = reader_asset["ASSETSN"].ToString();
                    lb_asname.Text = reader_asset["ASSETNAME"].ToString();
                    lb_depar.Text = reader_asset["department"].ToString();
                    asset_id = Convert.ToInt32(reader_asset["ID"].ToString());
                }
                reader_asset.Close();
                //display priority
                string select_pri = "select * from PRIORITIES";
                SqlCommand cmd_pri = new SqlCommand(select_pri, conn);
                SqlDataReader reader_pri = cmd_pri.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader_pri);
                if (dt.Rows.Count > 0)
                {
                    cbb_pri.DataSource = dt;
                    cbb_pri.DisplayMember = "Name";
                    cbb_pri.ValueMember = "ID";
                }
                reader_pri.Close();
            }
               
        }

        private void bt_sendre_Click(object sender, EventArgs e)
        {
     
            string des = txt_des.Text;
            string orther = txt_ort.Text;
            string datenow = DateTime.Now.ToString("yyyy-MM-dd");

            if(connect())
            {
                DialogResult dr = MessageBox.Show("are you sure to request?","confirm",MessageBoxButtons.YesNoCancel);
                if(dr == DialogResult.Yes)
                {
                    string ins = "INSERT INTO EMERGENCYMAINTENANCES(ASSETID,PRIORITYID,DESCRIPTIONEMERGECY,ORTHERCONSIDERATIONS,EMREPORTDATE ) VALUES" +
                    "\r\n (" + asset_id + "," + cbb_pri.SelectedValue + ",'" + des + "','" + orther + "','" + datenow + "')";
                    SqlCommand cmd_ins = new SqlCommand(ins, conn);
                    int result = cmd_ins.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Sent Request Successfull");
                        Emergency_management em = new Emergency_management();
                        em.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Request Failed");
                    }
                }

                

            }
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("everything is not saved. Do you want to exit?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Emergency_management em = new Emergency_management();
                em.Show();
                this.Close();
            }
        }
    }
}
