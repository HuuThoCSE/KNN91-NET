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
    public partial class Emergency_management : Form
    {
        string type_user = "";
        int id_emp;
        SqlConnection conn;
        string asset_name = "";
        public Emergency_management()
        {
            InitializeComponent();
            type_user = login_info.type_user;
            id_emp = login_info.id_emp;

            //if(type_user=="man")
            //{
            //    messagebox.show("this is management");
            //}
            //else if (type_user == "emp")
            //{
            //    messagebox.show("this is employee");
            //}
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
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Emergency_management_Load(object sender, EventArgs e)
        {
            if(connect())
            {
                if(type_user=="man")
                {
                    string select_man = "select ASSETSN, ASSETNAME, " +
                        "\r\n(select top(1) EMENDDATE  from EMERGENCYMAINTENANCES  where ASSETID = ass.ID order by EMENDDATE desc ) as 'Last closed EM'," +
                        "\r\n(select  COUNT(ASSETID) from EMERGENCYMAINTENANCES where ASSETID = ass.ID and EMENDDATE is not null) as 'number of EMs' from ASSETS ass";

                    SqlDataAdapter sda_man = new SqlDataAdapter(select_man, conn);
                    DataTable dt_man = new DataTable();
                    sda_man.Fill(dt_man);
                    dgv_list.DataSource = dt_man;

                   bt_send.Visible = false;

                }
                else if(type_user=="emp")
                {

                    string select_emp = "select ASSETSN, ASSETNAME, " +
                        "\r\n(select top(1) EMENDDATE  from EMERGENCYMAINTENANCES  where ASSETID = ass.ID order by EMSTARTDATE desc ) as 'Last closed EM'," +
                        "\r\n(select  COUNT(ASSETID) from EMERGENCYMAINTENANCES where ASSETID = ass.ID and EMENDDATE is not null) as 'number of EMs' from ASSETS ass" +
                        "\r\njoin EMPLOYEES  emp on emp.ID = ass.EMPLOYEEID" +
                        "\r\nwhere EMPLOYEEID ="+id_emp;

                    SqlDataAdapter sda_man = new SqlDataAdapter(select_emp, conn);
                    DataTable dt_emp = new DataTable();
                    sda_man.Fill(dt_emp);
                    dgv_list.DataSource = dt_emp;

                    bt_send.Visible = true;
                }
               
                for (int i = 0; i < dgv_list.Rows.Count - 1; i++)
                {
                    string select_notcomplete = "select ASSETSN, ASSETNAME, " +
                   "\r\n(select top(1) EMENDDATE  from EMERGENCYMAINTENANCES  where ASSETID = ass.ID order by EMREPORTDATE desc ) as 'Last closed EM'," +
                   "\r\n(select  COUNT(ASSETID) from EMERGENCYMAINTENANCES where ASSETID = ass.ID) as 'number of EMs' from ASSETS ass" +
                   "\r\nwhere ASSETSN ='" + dgv_list.Rows[i].Cells[0].Value.ToString()+"'";
                    SqlCommand cmd_notcomplete = new SqlCommand(select_notcomplete, conn);
                    SqlDataReader reader_notcomplete =  cmd_notcomplete.ExecuteReader();
                    if(reader_notcomplete.Read())
                    {
                        if(reader_notcomplete["Last closed EM"].ToString() == "")
                        {
                            dgv_list.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        }    
                    }
                    reader_notcomplete.Close();
                }
            }
        }

        private void bt_send_Click(object sender, EventArgs e)
        {
            if(asset_name=="")
            {
                MessageBox.Show("Please select a row");
            }
            else
            {
                EmergencyMaintenanceRequest emr = new EmergencyMaintenanceRequest(asset_name);
                emr.Show();
                this.Close();
            }
        }

        private void dgv_list_SelectionChanged(object sender, EventArgs e)
        {

            if(dgv_list.SelectedRows.Count > 0)
            {
                DataGridViewRow dgvrow = dgv_list.SelectedRows[0];
                asset_name = dgvrow.Cells[1].Value.ToString();

            }
            else
            {
                asset_name = "";
            }
        }
    }
}
