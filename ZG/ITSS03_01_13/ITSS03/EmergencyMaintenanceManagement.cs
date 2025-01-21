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
    public partial class EmergencyMaintenanceManagement : Form
    {
        string type_user = "";
        int id_emp;
        string asset_name = "";
        int id_em;
        public EmergencyMaintenanceManagement()
        {
            InitializeComponent();
            type_user = login_info.type_user;
            id_emp = login_info.id_emp;

        }

        private string GetQueryByUserType()
        {
            return type_user == "man"
                ? "select  ASSETSN ,ASSETNAME, EMREPORTDATE as 'Request Date' ,  LASTNAME +' '+ FIRSTNAME as 'Employee Full Name',de.NAME, emm.id  " +
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
                    "\r\n EMREPORTDATE asc"
                : "select  ASSETSN ,ASSETNAME, EMREPORTDATE as 'Request Date' ,  LASTNAME +' '+ FIRSTNAME as 'Employee Full Name',de.NAME, emm.id  " +
                    "\r\n from ASSETS ass" +
                    "\r\n join EMERGENCYMAINTENANCES emm on emm.ASSETID = ass.ID" +
                    "\r\n join EMPLOYEES emp on emp.ID = ass.EMPLOYEEID" +
                    "\r\n join PRIORITIES pri on pri.ID = emm.PRIORITYID" +
                    "\r\njoin DEPARTMENTLOCATIONS del on del.ID = ass.DEPARTMENTLOCAIONID" +
                    "\r\njoin DEPARTMENTS de on de.ID = del.DEPARTMENTID" +
                    "\r\n where emm.EMENDDATE is null and emp.ID =" + id_emp +
                    "\r\n order by case  pri.NAME " +
                    "\r\n when 'Very High' then 0" +
                    "\r\n when 'High' then 1" +
                    "\r\n else 2" +
                    "\r\n end," +
                    "\r\n EMREPORTDATE asc";

        }

        private void LoadDataToGird(string query)
        {
            try
            {
                using (var cmd = new SqlCommand(query, SqlConnManager.conn))
                using (var sda = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    sda.Fill(dt);

                    dgv_list.Rows.Clear();
                    foreach (DataRow dr in dt.Rows)
                    {
                        int rowIndex = dgv_list.Rows.Add(); // Add a new row
                        dgv_list.Rows[rowIndex].Cells[0].Value = dr[0]; // ASSETSN
                        dgv_list.Rows[rowIndex].Cells[1].Value = dr[1]; // ASSETNAME
                        dgv_list.Rows[rowIndex].Cells[2].Value = dr[2]; // EMREPORTDATE
                        dgv_list.Rows[rowIndex].Cells[3].Value = dr[3]; // Employee Full Name
                        dgv_list.Rows[rowIndex].Cells[4].Value = dr[4]; // Department Name
                        dgv_list.Rows[rowIndex].Tag = dr[5]; // Store the 'emm.id' in the Tag property
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EmergencyMaintenanceManagement_Load(object sender, EventArgs e)
        {
            if (!SqlConnManager.IsConnected()) return;

            string query = GetQueryByUserType();
            btnMR.Visible = type_user == "man";

            LoadDataToGird(query);
        }

        private void btnMR_Click(object sender, EventArgs e)
        {
            if (asset_name == "")
            {
                MessageBox.Show("Please select an asset");
            }
            else
            {
                EmergencyMaintenanceRequestDetails mr = new EmergencyMaintenanceRequestDetails();
                mr.Show();
                this.Hide();
            }
        }

        private void dgv_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgv_list_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            asset_name = dgv_list.Rows[e.RowIndex].Cells[1].Value.ToString() ?? "";
            id_em = Convert.ToInt32(dgv_list.Rows[e.RowIndex].Tag.ToString());  
        }
    }
}
