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

namespace ITSS03
{
    public partial class EMRequest : Form
    {
        string asset_name = "";
        int asset_id;
        int em_id;
        SqlConnection conn;
        public EMRequest(string inp_asset_name, int inp_id_em)
        {
            InitializeComponent();
            asset_name = inp_asset_name;
            asset_id = inp_id_em;
        }

        public Boolean connect()
        {
            try
            {
                conn = new SqlConnection();
                conn.ConnectionString = "server=thisPC\\THISPC;uid=sa;pwd=123456;database=ITS03DATA"
                conn.Open();
                return true;
            } catch
            {
                MessageBox.Show("Connect database failed");
                return false;
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void EMRequest_Load(object sender, EventArgs e)
        {
            if(connect()) {
                string select_asset = "select ASSETSN, ASSETNAME,  dp.NAME as 'department', em.EMSTARTDATE, em.EMENDDATE, em.EMTECHNICIANNOTE" +
                    "\r\nfrom ASSETS ass" +
                    "\r\njoin DEPARTMENTLOCATIONS dpl on dpl.ID = ass.DEPARTMENTLOCAIONID" +
                    "\r\njoin DEPARTMENTS dp on dpl.DEPARTMENTID = dp.ID" +
                    "\r\njoin EMERGENCYMAINTENANCES em on em.ASSETID = ass.ID" +
                    "\r\nwhere ass.ASSETNAME = '" + asset_name + "' and em.ID = " + em_id;

                SqlCommand cmd_asset = new SqlCommand(select_asset, conn);
                SqlDataReader reader_asset = cmd_asset .ExecuteReader();
                if (reader_asset.Read())
                {
                    lb_asset_sn.Text = reader_asset["ASSETSN"].ToString();
                    lb_asset_name.Text = reader_asset["ASSETNAME"].ToString();
                    lb_dpm.Text = reader_asset["department"].ToString();

                    if(reader_asset["EMSTARTDATE"].ToString() != "")
                    {
                        dtp_start.Text = reader_asset["EMSTARTDATE"].ToString().Trim();
                    } else
                    {
                        dtp_start.CustomFormat = "-/-/-";
                        dtp_start.Format = DateTimePickerFormat.Custom;
                    }

                    if (reader_asset["EMENDATE"].ToString() != "")
                    {
                        dtp_comp.Text = reader_asset["EMENDATE"].ToString();
                    } else
                    {
                        dtp_comp.CustomFormat = "-/-/-";
                        dtp_comp.Format = DateTimePickerFormat.Custom;
                    }
                    txtNote.Text = reader_asset["EMTECHICIANNOTE"].ToString();
                }
                reader_asset.Close();

                string select_part = "select * from PARTS";
                SqlCommand cmd_part = new SqlCommand(select_part, conn);
                SqlDataReader 
            }
        }
    }
}
