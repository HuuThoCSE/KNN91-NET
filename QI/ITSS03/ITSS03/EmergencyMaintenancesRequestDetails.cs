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
    public partial class EmergencyMaintenancesRequestDetails : Form
    {
        string EMID;
        public EmergencyMaintenancesRequestDetails(string ID)
        {
            InitializeComponent();
            EMID = ID;
        }
        SqlConnection conn;
        string connString = "server=QBGamer;database=ITSS03DATA;uid=sa;pwd=1";

        private void EmergencyMaintenancesRequestDetails_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(connString);
                conn.Open();
                string query = "select\r\n\ta.AssetSN,\r\n\ta.AssetName,\r\n\td.Name,\r\n\tem.EMStartDate,\r\n\tem.EMEndDate,\r\n\tem.EMTechnicianNote\r\nfrom EmergencyMaintenances as em\r\njoin Assets as a on a.ID=em.AssetID\r\njoin DepartmentLocations as dl on dl.ID=a.DepartmentLocationID\r\njoin Departments as d on d.ID=dl.DepartmentID\r\nwhere em.ID='" + EMID + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lb_assetSN.Text = reader[0].ToString();
                    lb_assetname.Text = reader[1].ToString();
                    lb_department.Text = reader[2].ToString();
                    dtp_start.Value = DateTime.Parse(reader[3].ToString());
                    dtp_end.Value = DateTime.Parse(reader[4].ToString());
                    rtb_technote.Text = reader[5].ToString();
                }
                reader.Close();
                query = "select\r\n\tp.ID,\r\n\tp.Name,\r\n\tcp.Amount\r\nfrom EmergencyMaintenances as em\r\nleft join ChangedParts as cp on cp.EmergencyMaintenanceID=em.ID\r\njoin Parts as p on p.ID=cp.PartID\r\nwhere em.ID='" + EMID + "'";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dataGridView.Rows.Add(
                        reader[0],
                        reader[1],
                        reader[2],
                        "remove"
                    );
                }
                reader.Close();
                query = "select * from Parts";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                if (dt.Rows.Count > 0)
                {
                    cb_parts.DataSource = dt;
                    cb_parts.DisplayMember = "Name";
                    cb_parts.ValueMember = "ID";
                }
                reader.Close();
                tb_amount.Controls[0].Visible = false;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EmergencyMaintenancesRequestDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            EmergencyMaintenancesManagement form = new EmergencyMaintenancesManagement();
            form.Show();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells[0].Value.ToString() == cb_parts.SelectedValue.ToString())
                    {
                        row.Cells[2].Value = Int64.Parse(row.Cells[2].Value.ToString()) + tb_amount.Value;
                        return;
                    }
                }
                dataGridView.Rows.Add(cb_parts.SelectedValue.ToString(), cb_parts.Text.ToString(), tb_amount.Value.ToString(), "remove");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            //try
            //{
            conn = new SqlConnection(connString);
            conn.Open();
            string query;
            SqlCommand cmd;
            SqlDataReader r;
            int newval;
            List<int> partid_datagrid= new List<int>();
            foreach(DataGridViewRow row  in dataGridView.Rows)
            {
                partid_datagrid.Add(int.Parse(row.Cells[0].Value.ToString()));
            }

            List<int> partid_db= new List<int>();
            query = "select PartID from ChangedParts where EmergencyMaintenanceID='"+EMID+"'";
            cmd = new SqlCommand(query, conn);
            r = cmd.ExecuteReader();
            while (r.Read())
            {
                partid_db.Add(int.Parse(r[0].ToString()));
            }
            r.Close();
            List<int> partid_delete= partid_db.Except(partid_datagrid).ToList();
            foreach(int partid in partid_delete)
            {
                query = "Delete from ChangedParts where partID=" + partid + " and EmergencyMaintenanceID='" + EMID + "'";
                cmd = new SqlCommand(query,conn);
                cmd.ExecuteNonQuery();
            }
            r.Close();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                r.Close();
                query = "select Amount from ChangedParts where EmergencyMaintenanceID='" + EMID+ "' and PartID='" + row.Cells[0].Value.ToString() +"'";
                cmd=new SqlCommand(query, conn);
                r = cmd.ExecuteReader();
                if (r.Read())
                {
                    if (int.Parse(row.Cells[2].Value.ToString()) != int.Parse(r[0].ToString()))
                    {
                        newval = (int.Parse(row.Cells[2].Value.ToString()) - int.Parse(r[0].ToString())) + int.Parse(r[0].ToString());
                    }
                    else
                    {
                        newval = int.Parse(r[0].ToString());
                    }
                    query = "update ChangedParts set Amount=" + newval + " where EmergencyMaintenanceID='" + EMID + "' and PartID='" + row.Cells[0].Value.ToString() + "'";
                    r.Close();
                    cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    query = "insert into ChangedParts values ('" + EMID + "','" + row.Cells[0].Value.ToString() + "','" + row.Cells[2].Value.ToString() + "')";
                    r.Close();
                    cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            conn.Close();
            MessageBox.Show("Submit success!","Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
            this.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 3)
            {
                dataGridView.Rows.Remove(dataGridView.Rows[e.RowIndex]);
            }
        }
    }
}
