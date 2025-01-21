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
    public partial class Emergency_Maintenance_Management : Form
    {
        string type_user = "";
        int id_emp;
        SqlConnection conn;
        string asset_name = "";
        int id_em;

        public Emergency_Maintenance_Management()
        {
            InitializeComponent();

            type_user = login_data.type_user;
            id_emp = login_data.id_emp;
        }

        public Boolean connect()
        {
            try
            {
                conn = new SqlConnection();
                conn.ConnectionString = "server=thisPC\\THISPC;uid=sa;pwd=123456;database=ITS03DATA";
                conn.Open();
                return true;
            }
            catch {
                MessageBox.Show("Connect database failed");
                return false;
            }
        }

        private void Emergency_Maintenance_Management_Load(object sender, EventArgs e)
        {
            if(connect()) {
                string select = "";

                if(type_user == "man")
                {
                    select = "select  ASSETSN ,ASSETNAME, EMREPORTDATE as 'Request Date' ,  LASTNAME +' '+ FIRSTNAME as 'Employee Full Name',de.NAME, emm.id  " +
                        "\r\n from ASSETS ass" +
                        "\r\n join EMERGENCYMAINTENANCES emm on emm.ASSETID = ass.ID" +
                        "\r\n join EMPLOYEES emp on emp.ID = ass.EMPLOYEEID" +
                        "\r\n join PRIORITIES pri on pri.ID = emm.PRIORITYID" +
                        "\r\njoin DEPARTMENTLOCATIONS del on del.ID = ass.DEPARTMENTLOCAIONID" +
                        "\r\njoin DEPARTMENTS de on de.ID = del.DEPARTMENTID" +
                        "\r\n where emm.EMENDDATE is null" +
                        "\r\n order by case  pri.NAME " +
                        "\r\n when 'Very High' then 0" +
                        "\r\n when 'High' then 1" +
                        "\r\n else 2" +
                        "\r\n end," +
                        "\r\n EMREPORTDATE asc";
                    btn_request.Visible = true;
                } else
                {
                    select = "select  ASSETSN ,ASSETNAME, EMREPORTDATE as 'Request Date' ,  LASTNAME +' '+ FIRSTNAME as 'Employee Full Name',de.NAME, emm.id " +
                        "\r\n from ASSETS ass" +
                        "\r\n join EMERGENCYMAINTENANCES emm on emm.ASSETID = ass.ID" +
                        "\r\n join EMPLOYEES emp on emp.ID = ass.EMPLOYEEID" +
                        "\r\n join PRIORITIES pri on pri.ID = emm.PRIORITYID" +
                        "\r\njoin DEPARTMENTLOCATIONS del on del.ID = ass.DEPARTMENTLOCAIONID" +
                        "\r\njoin DEPARTMENTS de on de.ID = del.DEPARTMENTID" +
                        "\r\n where emm.EMENDDATE is null and emp.ID=" + id_emp +
                        "\r\n order by case  pri.NAME " +
                        "\r\n when 'Very High' then 0" +
                        "\r\n when 'High' then 1" +
                        "\r\n else 2" +
                        "\r\n end," +
                        "\r\n EMREPORTDATE asc";
                    btn_request.Visible = false;
                }

                SqlDataAdapter sda = new SqlDataAdapter(select, conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    int n = dgv_asset.Rows.Add();
                    dgv_asset.Rows[n].Cells[0].Value = dr[0].ToString();
                    dgv_asset.Rows[n].Cells[1].Value = dr[1].ToString();
                    dgv_asset.Rows[n].Cells[2].Value = dr[2].ToString();
                    dgv_asset.Rows[n].Cells[3].Value = dr[3].ToString();
                    dgv_asset.Rows[n].Cells[4].Value = dr[4].ToString();
                    dgv_asset.Rows[n].Tag = dr[5].ToString();

                }
            }
        }

        private void dgv_asset_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            asset_name = dgv_asset.Rows[e.RowIndex].Cells[1].Value.ToString();
            id_em = Convert.ToInt32(dgv_asset.Rows[e.RowIndex].Tag.ToString());
        }

        private void btn_request_Click(object sender, EventArgs e)
        {
            if(asset_name == "")
            {
                MessageBox.Show("Please select a row");
            } else
            {
                EMRequest request = new EMRequest(asset_name, id_em);
                request.Show();
                this.Close();
            }
        }
    }
}
