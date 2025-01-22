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

namespace LeNguyenQuangBinh_21022010
{
    public partial class EmergencyMaintenancesManagement : Form
    {
        public EmergencyMaintenancesManagement()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        string connString = "server=QBGamer;database=ITSS03DATA;uid=sa;pwd=1";
        private void EmergencyMaintenancesManagement_Load(object sender, EventArgs e)
        {
            loaddata();

        }
        public void loaddata()
        {
            try
            {
                conn = new SqlConnection(connString);
                conn.Open();
                string query = "select\r\n\tem.ID,\r\n\ta.AssetSN,\r\n\ta.AssetName,\r\n\tem.EMReportDate,\r\n\tconcat(e.FirstName,' ',e.LastName),\r\n\td.Name\r\nfrom Assets as a\r\njoin Employees as e on e.ID=a.EmployeeID\r\nleft join EmergencyMaintenances as em on em.AssetID=a.ID\r\nleft join DepartmentLocations as dl on dl.ID=a.DepartmentLocationID\r\njoin Departments as d on d.ID=dl.DepartmentID\r\ngroup by em.ID,a.AssetSN,a.AssetName,em.EMReportDate,concat(e.FirstName,' ',e.LastName),d.Name";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (rdr[0].ToString() == "")
                    {
                        continue;
                    }
                    dataGridView.Rows.Add(
                        rdr[0],
                        rdr[1],
                        rdr[2],
                        rdr[3],
                        rdr[4],
                        rdr[5]
                    );
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EmergencyMaintenancesManagement_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        string IDselected;

        private void btn_manage_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(IDselected);
            if(IDselected == null && dataGridView.Rows.Count>0)
            {
                IDselected=dataGridView.Rows[0].Cells[0].Value.ToString();
            }
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("There are no asset to view!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            EmergencyMaintenancesRequestDetails form = new EmergencyMaintenancesRequestDetails(IDselected);
            this.Hide();
            form.Show();
        }
        public void tet()
        {
            MessageBox.Show("test");
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    IDselected = dataGridView.Rows[e.RowIndex].Cells["AssetID"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
