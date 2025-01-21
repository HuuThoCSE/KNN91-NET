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
    public partial class Emergency_management : Form
    {
        string type_user = "";
        int id_emp;
        SqlConnection conn;
        string asset_name = "";
        int id_em;
        public Emergency_management()
        {
            InitializeComponent();
            type_user = login_info.type_user;
            id_emp = login_info.id_emp;
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

        private void bt_send_Click(object sender, EventArgs e)
        {
            if (asset_name == "")
            {
                MessageBox.Show("Please select a row");
            }
            else
            {
                EmergencyMaintainanceRequestDetails emrd = new EmergencyMaintainanceRequestDetails(asset_name, id_em);
                emrd.Show();
                this.Close();
            }
        }

        private void Emergency_management_Load_1(object sender, EventArgs e)
        {
            if (connect())
            {
                string select = "";
                if (type_user == "man")
                {
                     select = "select  ASSETSN ,ASSETNAME, EMREPORTDATE as 'Request Date' ,  LASTNAME +' '+ FIRSTNAME as 'Employee Full Name',de.NAME, emm.id  " +
                        "\r\n from ASSETS ass" +
                        "\r\n join EMERGENCYMAINTENANCES emm on emm.ASSETID = ass.ID" +
                        "\r\n join EMPLOYEES emp on emp.ID = ass.EMPLOYEEID" +
                        "\r\n join PRIORITIES pri on pri.ID = emm.PRIORITYID" +
                        "\r\njoin DEPARTMENTLOCATIONS del on del.ID = ass.DEPARTMENTLOCAIONID" +
                        "\r\njoin DEPARTMENTS de on de.ID = del.DEPARTMENTID"+
                        "\r\n where emm.EMENDDATE is null" +
                        "\r\n order by case  pri.NAME " +
                        "\r\n when 'Very High' then 0" +
                        "\r\n when 'High' then 1" +
                        "\r\n else 2" +
                        "\r\n end," +
                        "\r\n EMREPORTDATE asc";
                       bt_send.Visible = true;

                }
                else if (type_user == "emp")
                {

                     select= "select  ASSETSN ,ASSETNAME, EMREPORTDATE as 'Request Date' ,  LASTNAME +' '+ FIRSTNAME as 'Employee Full Name',de.NAME, emm.id " +
                        "\r\n from ASSETS ass" +
                        "\r\n join EMERGENCYMAINTENANCES emm on emm.ASSETID = ass.ID" +
                        "\r\n join EMPLOYEES emp on emp.ID = ass.EMPLOYEEID" +
                        "\r\n join PRIORITIES pri on pri.ID = emm.PRIORITYID" +
                        "\r\njoin DEPARTMENTLOCATIONS del on del.ID = ass.DEPARTMENTLOCAIONID" +
                        "\r\njoin DEPARTMENTS de on de.ID = del.DEPARTMENTID" +
                        "\r\n where emm.EMENDDATE is null and emp.ID=" + id_emp+
                        "\r\n order by case  pri.NAME " +
                        "\r\n when 'Very High' then 0" +
                        "\r\n when 'High' then 1" +
                        "\r\n else 2" +
                        "\r\n end," +
                        "\r\n EMREPORTDATE asc";

                        bt_send.Visible = false;
                }
                SqlDataAdapter sda = new SqlDataAdapter(select, conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                foreach(DataRow dr in dt.Rows)
                {
                    int n = dgv_list.Rows.Add();
                    dgv_list.Rows[n].Cells[0].Value = dr[0].ToString();
                    dgv_list.Rows[n].Cells[1].Value = dr[1].ToString();
                    dgv_list.Rows[n].Cells[2].Value = dr[2].ToString();
                    dgv_list.Rows[n].Cells[3].Value = dr[3].ToString();
                    dgv_list.Rows[n].Cells[4].Value = dr[4].ToString();
                    dgv_list.Rows[n].Tag = dr[5].ToString();

                }

            }
        }

        private void dgv_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgv_list_SelectionChanged_1(object sender, EventArgs e)
        {
         
        }

        private void dgv_list_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            asset_name = dgv_list.Rows[e.RowIndex].Cells[1].Value.ToString();
            id_em = Convert.ToInt32(dgv_list.Rows[e.RowIndex].Tag.ToString());
        }

    }
}
