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
    public partial class WarehouseManagement : Form
    {
        public WarehouseManagement()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        string constring = "server=QBGamer;database=ITSS04DATA;uid=sa;pwd=1";
        private void WarehouseManagement_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(constring);
                conn.Open();
                string query = "select * from Warehouses";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt_source = new DataTable();
                DataTable dt_destination = new DataTable();
                dt_source.Load(reader);
                if (dt_source.Rows.Count > 0)
                {
                    cb_source.DataSource = dt_source;
                    cb_source.DisplayMember = "Name";
                    cb_source.ValueMember = "ID";
                }
                reader.Close();
                reader = cmd.ExecuteReader();
                dt_destination.Load(reader);
                if (dt_destination.Rows.Count > 0)
                {
                    cb_destination.DataSource = dt_destination;
                    cb_destination.DisplayMember = "Name";
                    cb_destination.ValueMember = "ID";
                }
                query = "select * from Parts";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                if (dt.Rows.Count > 0)
                {
                    cb_name.DataSource = dt;
                    cb_name.DisplayMember = "Name";
                    cb_name.ValueMember = "ID";
                }
                reader.Close();
                tb_amount.Controls[0].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void WarehouseManagement_FormClosed(object sender, FormClosedEventArgs e)
        {
            InventoryManagement inventoryManagement = new InventoryManagement();
            inventoryManagement.Show();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cb_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cb_name.Items.Count > 0)
                {
                    if (cb_name.SelectedValue is int)
                    {
                        string query = "select BatchNumberHasRequired from Parts where ID='" + cb_name.SelectedValue + "'";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        SqlDataReader reader = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        if (reader.Read())
                        {
                            if (reader[0].ToString() == "True")
                            {
                                reader.Close();
                                dt.Clear();
                                query = "select BatchNumber from OrderItems where PartID=" + cb_name.SelectedValue;
                                cmd = new SqlCommand(query, conn);
                                reader = cmd.ExecuteReader();
                                dt.Load(reader);
                                if (dt.Rows.Count > 0)
                                {
                                    cb_batchnumber.DataSource = dt;
                                    cb_batchnumber.DisplayMember = "BatchNumber";
                                    cb_batchnumber.ValueMember = "BatchNumber";
                                }
                            }
                            else
                            {
                                cb_batchnumber.DataSource = null;
                                cb_batchnumber.Items.Clear();
                            }
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if(row.Cells[0].Value.ToString()==cb_name.SelectedValue.ToString() && (row.Cells[2].Value is null || (row.Cells[2].Value.ToString() == cb_batchnumber.Text.ToString())))
                    {
                        row.Cells[3].Value = tb_amount.Value + int.Parse(row.Cells[3].Value.ToString());
                        return;
                    }
                }
                dataGridView.Rows.Add(cb_name.SelectedValue, cb_name.Text, cb_batchnumber.SelectedValue, tb_amount.Value, "Remove");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 4)
                {
                    dataGridView.Rows.Remove(dataGridView.Rows[e.RowIndex]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                string query;
                string bathnumber;
                SqlCommand cmd;
                SqlDataReader rdr;
                query = "insert into Orders values (2,NULL," + cb_source.SelectedValue + "," + cb_destination.SelectedValue + ",'" + dateTimePicker.Value + "');" +
                    "select SCOPE_IDENTITY();";
                cmd = new SqlCommand(query, conn);
                rdr = cmd.ExecuteReader();
                if(rdr.Read())
                {
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        if(row.Cells[2].Value is null)
                        {
                            bathnumber = "";
                        }
                        else
                        {
                            bathnumber = row.Cells[2].Value.ToString();
                        }
                        query = "insert into OrderItems values (" + rdr[0].ToString() + "," + row.Cells[0].Value.ToString() + ",'" + bathnumber + "'," + row.Cells[3].Value.ToString() + ")";
                        cmd = new SqlCommand(query, conn);
                        rdr.Close();
                        cmd.ExecuteNonQuery();
                    }
                }
                rdr.Close();
                MessageBox.Show("Submit success!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
