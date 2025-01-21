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
        string UserID;

        public EmergencyMaintenancesManagement(string ID)
        {
            InitializeComponent();
            UserID = ID;
        }
        SqlConnection conn;
        string SelectAssetID;
        private void EmergencyMaintenancesManagement_Load(object sender, EventArgs e)
        {
            try
            {
                string conString = "server=QBGamer;database=ITSS02DATA;uid=sa;pwd=1";
                conn = new SqlConnection(conString);
                conn.Open();
                string query = "select\r\n\tAssets.ID,\r\n\tWarrantyDate,\r\n\tAssetSN,\r\n\tAssetName,\r\n\tformat(max(EMEndDate),'yyyy-MM-dd'),\r\n\tcount(em.ID)\r\nfrom Assets\r\njoin Employees as e on e.ID=Assets.EmployeeID\r\nleft join EmergencyMaintenances as em on em.AssetID = Assets.ID\r\nwhere e.ID='" + UserID + "'\r\ngroup by Assets.ID,AssetSN,AssetName,WarrantyDate";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    dataGridView.Rows.Add(
                        sqlDataReader[0],
                        sqlDataReader[1],
                        sqlDataReader[2],
                        sqlDataReader[3],
                        sqlDataReader[4],
                        sqlDataReader[5]
                    );
                }
                sqlDataReader.Close();
                conn.Close();
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (DateTime.Parse(row.Cells[1].Value.ToString()) > DateTime.Now)
                    {
                        row.Cells[2].Style.BackColor = Color.Red;
                        row.Cells[3].Style.BackColor = Color.Red;
                        row.Cells[4].Style.BackColor = Color.Red;
                        row.Cells[5].Style.BackColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    SelectAssetID = dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                    //MessageBox.Show(SelectAssetID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView.Rows.Count == 0)
                {
                    MessageBox.Show("There are no asset selected!","Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                if(SelectAssetID is null)
                {
                    SelectAssetID = dataGridView.Rows[0].Cells[0].Value.ToString();
                }
                EmergencyMaintenanceRequest form = new EmergencyMaintenanceRequest(SelectAssetID);
                form.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
