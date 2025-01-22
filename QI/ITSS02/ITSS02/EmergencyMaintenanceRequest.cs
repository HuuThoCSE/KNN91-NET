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
    public partial class EmergencyMaintenanceRequest : Form
    {
        string assetID;
        public EmergencyMaintenanceRequest(string SelectAssetID)
        {
            InitializeComponent();
            assetID = SelectAssetID;
        }
        SqlConnection conn;
        private void EmergencyMaintenanceRequest_Load(object sender, EventArgs e)
        {
            string conString = "server=QBGamer;database=ITSS02DATA;uid=sa;pwd=1";
            conn = new SqlConnection(conString);
            conn.Open();
            string query = "select Assets.AssetSN,Assets.AssetName,concat(Departments.Name,' ',Locations.Name)\r\nfrom Assets,DepartmentLocations,Departments, Locations\r\nwhere Assets.DepartmentLocationID=DepartmentLocations.ID and\r\n\tDepartmentLocations.DepartmentID=Departments.ID and\r\n\tDepartmentLocations.LocationID=Locations.ID and\r\n\tAssets.ID='"+ assetID + "'";
            MessageBox.Show(query);
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lb_assetSN.Text=reader[0].ToString();
                lb_assetName.Text=reader[1].ToString();
                lb_deparment.Text=reader[2].ToString();
            }
            reader.Close();

            query = "select * from Priorities";
            cmd= new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cbx_priority.Items.Add(reader[1].ToString());
            }
            reader.Close();
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
