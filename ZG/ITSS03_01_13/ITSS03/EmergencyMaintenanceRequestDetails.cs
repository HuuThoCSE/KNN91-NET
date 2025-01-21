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
    public partial class EmergencyMaintenanceRequestDetails : Form
    {
        string asset_name = "";
        int asset_id;
        SqlConnection cmd;
        int em_id;

        public EmergencyMaintenanceRequestDetails(string inp_asset, int inp_em_id)
        {
            InitializeComponent();
            asset_name = inp_asset;
            em_id = inp_em_id;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void LoadDT(string query)
        {
            try
            {
                using (var cmd_asset = new SqlCommand(query, SqlConnManager.conn))
                SqlDataReader sdr_asset = cmd_asset.ExecuteReader();

                MessageBox.Show(sdr_asset);
                
                if (sdr_asset.Read())
                {
                    lb_asset_sn.Text = sdr_asset["ASSETSN"].ToString();
                    lb_asset_name.Text = sdr_asset["ASSETNAME"].ToString();
                    lb_dpm.Text = sdr_asset["department"].ToString();

                    if (sdr_asset["EMSTARTDATE"].ToString() != "")
                    {
                        dtp_start.Text = sdr_asset["EMSTARTDATE"].ToString();
                    }
                    else
                    {
                        dtp_start.CustomFormat = "-/-/-";
                        dtp_start.Format = DateTimePickerFormat.Custom;
                    }

                    if (reader_asset["EMENDDATE"].ToString() != "")
                    {
                        dtp_end.Text = sdr_asset["EMENDDATE"].ToString();
                    }
                    else
                    {
                        dtp_end.CustomFormat = "-/-/-";
                        dtp_end.Format = DateTimePickerFormat.Custom;
                    }
                    txt_node.Text = sdr_asset["EMTECHNICIANNOTE"].ToString();
                }
            }
        }

        private void EmergencyMaintenanceRequestDetails_Load(object sender, EventArgs e)
        {
            if (SqlConnManager.IsConnected())
            {
                string select = "select ASSETSN, ASSETNAME,  dp.NAME as 'department', em.EMSTARTDATE, em.EMENDDATE, em.EMTECHNICIANNOTE" +
                    "\r\nfrom ASSETS ass" +
                    "\r\njoin DEPARTMENTLOCATIONS dpl on dpl.ID = ass.DEPARTMENTLOCAIONID" +
                    "\r\njoin DEPARTMENTS dp on dpl.DEPARTMENTID = dp.ID" +
                    "\r\njoin EMERGENCYMAINTENANCES em on em.ASSETID = ass.ID" +
                    "\r\nwhere ass.ASSETNAME = '" + asset_name + "' and em.ID = " + em_id;


            }
        }
    }
}
