using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITSS03
{
    public partial class EmergencyMaintainanceRequestDetails : Form
    {
        string asset_name = "";
        int asset_id;
        SqlConnection conn;
        int em_id;
        public EmergencyMaintainanceRequestDetails(string asset, int emid)
        {
            InitializeComponent();
            asset_name = asset;
            em_id = emid;
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

        private void EmergencyMaintainanceRequestDetails_Load(object sender, EventArgs e)
        {
            if (connect())
            {
                //display asset & EM
                string select_asset = "select ASSETSN, ASSETNAME,  dp.NAME as 'department', em.EMSTARTDATE, em.EMENDDATE, em.EMTECHNICIANNOTE" +
                    "\r\nfrom ASSETS ass" +
                    "\r\njoin DEPARTMENTLOCATIONS dpl on dpl.ID = ass.DEPARTMENTLOCAIONID" +
                    "\r\njoin DEPARTMENTS dp on dpl.DEPARTMENTID = dp.ID" +
                    "\r\njoin EMERGENCYMAINTENANCES em on em.ASSETID = ass.ID" +
                    "\r\nwhere ass.ASSETNAME = '" + asset_name + "' and em.ID = " + em_id;

                SqlCommand cmd_asset = new SqlCommand(select_asset, conn);
                SqlDataReader reader_asset = cmd_asset.ExecuteReader();
                if (reader_asset.Read())
                {
                    lb_assn.Text = reader_asset["ASSETSN"].ToString();
                    lb_asname.Text = reader_asset["ASSETNAME"].ToString();
                    lb_depar.Text = reader_asset["department"].ToString();

                    if (reader_asset["EMSTARTDATE"].ToString() != "")
                    {
                        dtp_start.Text = reader_asset["EMSTARTDATE"].ToString();
                    }
                    else
                    {
                        dtp_start.CustomFormat = "-/-/-";
                        dtp_start.Format = DateTimePickerFormat.Custom;
                    }

                    if (reader_asset["EMENDDATE"].ToString() != "")
                    {
                        dtp_end.Text = reader_asset["EMENDDATE"].ToString();
                    }
                    else
                    {
                        dtp_end.CustomFormat = "-/-/-";
                        dtp_end.Format = DateTimePickerFormat.Custom;
                    }
                    txt_note.Text = reader_asset["EMTECHNICIANNOTE"].ToString();
                }
                reader_asset.Close();

                //display partname
                string select_part = "select * from PARTS";
                SqlCommand cmd_part = new SqlCommand(select_part, conn);
                SqlDataReader reader_part = cmd_part.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader_part);
                if (dt.Rows.Count > 0)
                {

                    cbb_pn.DataSource = dt;
                    cbb_pn.DisplayMember = "Name";
                    cbb_pn.ValueMember = "ID";
                }
                reader_part.Close();


                //display list partchanged
                string sql_partch = "select NAME, AMOUNT, cp.ID from CHANGEDPARTS cp " +
                    "\r\njoin PARTS on PARTS.ID = cp.PARTID" +
                    "\r\njoin EMERGENCYMAINTENANCES em on em.ID = cp.EMERGENCYMAINTENANCESID" +
                    "\r\nwhere cp.EMERGENCYMAINTENANCESID = " + em_id;
                SqlCommand cmd_partch = new SqlCommand(sql_partch, conn);
                SqlDataReader reader_partch = cmd_partch.ExecuteReader();
                DataTable dt_partch = new DataTable();
                dt_partch.Load(reader_partch);
                foreach (DataRow dr in dt_partch.Rows)
                {
                    int n = dgv_listpart.Rows.Add();
                    dgv_listpart.Rows[n].Cells[0].Value = dr["NAME"].ToString();
                    dgv_listpart.Rows[n].Cells[1].Value = dr["AMOUNT"].ToString();
                    dgv_listpart.Rows[n].Cells[2].Value = "Remove";
                    dgv_listpart.Rows[n].Cells[2].Style.ForeColor = Color.Blue;

                    dgv_listpart.Rows[n].Tag = "data";
                }
                reader_partch.Close();
            }
        }
        public int take_idpart(string name)
        {
            string sql = "select id from parts where name='" + name + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int id = Convert.ToInt32(reader["ID"]);
                reader.Close();
                return id;
            }
            else
            {
                reader.Close();
                return -1;
            }
            
        }
        private void bt_cancel_Click_1(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("everything is not saved. Do you want to exit?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Emergency_management em = new Emergency_management();
                em.Show();
                this.Close();
            }
        }
        public void add_list(int row)
        {
            string pn = cbb_pn.Text;
            string am = txt_am.Text;

            dgv_listpart.Rows[row].Cells[0].Value = pn;
            dgv_listpart.Rows[row].Cells[1].Value = am;
            dgv_listpart.Rows[row].Cells[2].Value = "Remove";

            dgv_listpart.Rows[row].Cells[2].Style.ForeColor = Color.Blue;
        }
        private void bt_add_Click(object sender, EventArgs e)
        {
            if (txt_am.Text != "")
            {
                if (dgv_listpart.RowCount > 0)
                {
                    int i;
                    for (i = 0; i < dgv_listpart.RowCount - 1; i++)
                    {

                        if (cbb_pn.Text == dgv_listpart.Rows[i].Cells[0].Value.ToString())
                        {
                            if (dgv_listpart.Rows[i].Tag.ToString() == "data" || dgv_listpart.Rows[i].Tag.ToString() == "update")
                            {
                               
                                add_list(i);
                                dgv_listpart.Rows[i].Tag = "update";
                                break;

                            }
                            else if (dgv_listpart.Rows[i].Tag.ToString() == "insert")
                            {
                                add_list(i);
                                break;
                            }
                        }
                    }
                    if (i == dgv_listpart.RowCount - 1)
                    {
                       
                        int n = dgv_listpart.Rows.Add();
                        add_list(n);
                        dgv_listpart.Rows[n].Tag = "insert";
                    }
                }
                else
                {
                    int n = dgv_listpart.Rows.Add();
                    add_list(n);
                    dgv_listpart.Rows[n].Tag = "insert";
                }
            }
            else
            {
                MessageBox.Show("Please enter Amount");
            }
        }

        private void txt_am_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        public Boolean check_date()
        {

            if (dtp_start.Text != "-/-/-" && dtp_end.Text != "-/-/-")
            {
                if (dtp_start.Value < dtp_end.Value)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;

        }
        private void bt_sub_Click(object sender, EventArgs e)
        {

            if (check_date() == true && connect())
            {
                int result;
                //submit_em
                string datestart = dtp_start.Text;
                string dateend = dtp_end.Text;
                string note = txt_note.Text;
                if (datestart == "-/-/-")
                {
                    datestart = null;
                }
                if (dateend == "-/-/-")
                {
                    dateend = null;
                }
                string sql_upd_em = "Update EMERGENCYMAINTENANCES " +
                                    "set EMSTARTDATE= " + (datestart != null ? "'" + datestart + "'" : "NULL") + "," +
                                    " EMENDDATE=" + (dateend != null ? "'" + dateend + "'" : "NULL") + "," +
                                    " EMTECHNICIANNOTE= '" + note + "'" +
                                    "where id=" + em_id;
                SqlCommand cmd_upd_em = new SqlCommand(sql_upd_em, conn);
                result = cmd_upd_em.ExecuteNonQuery();
                Random rd = new Random();
                int amount, id_part, idpartchanged;
                string sql = "";

                //submit_partchanged
                if (dgv_listpart.RowCount > 0)
                {
                    for (int i = 0; i < dgv_listpart.Rows.Count - 1; i++)
                    {
                        id_part = take_idpart(dgv_listpart.Rows[i].Cells[0].Value.ToString());
                        amount = Convert.ToInt32(dgv_listpart.Rows[i].Cells[1].Value.ToString());
                       
                        if (dgv_listpart.Rows[i].Tag.ToString() == "insert")
                        {
                            idpartchanged = rd.Next(10, 1000);
                            sql = "insert into CHANGEDPARTS " +
                                        "values(" + idpartchanged + "," + em_id + "," + id_part + "," + amount + ")";
                        }
                        else if (dgv_listpart.Rows[i].Tag.ToString() == "update")
                        {
                            sql = "update CHANGEDPARTS " +
                                "set amount=" + amount + " " +
                                "where EMERGENCYMAINTENANCESID=" + em_id + " and PARTID=" + id_part;
                        }
                        
                        else
                        {
                            continue;
                        }
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        result = cmd.ExecuteNonQuery();
                    }
                }
                if (result > 0)
                {
                    MessageBox.Show("Submit Successfull");
                    Emergency_management em = new Emergency_management();
                    em.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Submit Fail");
                }
                
            }
            else
            {
                MessageBox.Show("Start date must be less than End date");
            }

        }

        private void dgv_listpart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && connect())
            {

                DialogResult dr = MessageBox.Show("Do you want to remove this?", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    if (dgv_listpart.Rows[e.RowIndex].Tag.ToString() == "data" || dgv_listpart.Rows[e.RowIndex].Tag.ToString() == "update")
                    {
                        MessageBox.Show("here");
                        int id_part = take_idpart(dgv_listpart.Rows[e.RowIndex].Cells[0].ToString());  
                        string sql = "delete CHANGEDPARTS where EMERGENCYMAINTENANCESID=" + em_id + " and PARTID=" + id_part;
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                       
                        dgv_listpart.Rows.RemoveAt(e.RowIndex);
                       
                    }
                    else
                    {
                        dgv_listpart.Rows.RemoveAt(e.RowIndex);
                    }
                }
            }
        }

        private void dtp_start_ValueChanged(object sender, EventArgs e)
        {
            dtp_start.CustomFormat = "yyyy/MM/dd";
            dtp_start.Format = DateTimePickerFormat.Custom;
        }

        private void dtp_end_ValueChanged(object sender, EventArgs e)
        {
            dtp_end.CustomFormat = "yyyy/MM/dd";
            dtp_end.Format = DateTimePickerFormat.Custom;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

}
