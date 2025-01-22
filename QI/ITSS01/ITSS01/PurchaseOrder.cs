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
    public partial class PurchaseOrder : Form
    {
        public PurchaseOrder()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        private void PurchaseOrder_Load(object sender, EventArgs e)
        {
            try
            {
                dateTimePicker.Value = DateTime.Now;
                tb_amount.Controls[0].Visible = false;
                string connectString = "server=QBGamer;database=ITSS01DATA;uid=sa;pwd=1";
                conn = new SqlConnection(connectString);
                conn.Open();
                string query = "select * from Parts";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cbx_partname.Items.Add(reader[1]);
                }
                reader.Close();

                query = "select * from Warehouses";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cbx_warehouse.Items.Add(reader[1]);
                }
                reader.Close();

                query = "select * from Suppliers";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cbx_suppliers.Items.Add(reader[1]);
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbx_partname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (e != null)
                {
                    string query = "select BatchNumberHasRequired from Parts where Name='" + cbx_partname.SelectedItem + "'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader[0].ToString() == "True")
                        {
                            tb_batchnumber.ReadOnly = false;
                        }
                        else
                        {
                            tb_batchnumber.Clear();
                            tb_batchnumber.ReadOnly = true;
                        }
                    }
                    reader.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if(cbx_partname.SelectedItem == null || tb_amount.Text == "" || (tb_batchnumber.ReadOnly == false && tb_batchnumber.Text == ""))
                {
                    MessageBox.Show("Please fill all data on the left before add!", "Warning!",MessageBoxButtons.OK);
                    return;
                }
                if (tb_amount.Value < 1)
                {
                    MessageBox.Show("Amount much bigger than 0!", "Warning!");
                    tb_amount.Focus();
                    return;
                }


                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells[0].Value.ToString() == cbx_partname.SelectedItem && row.Cells[1].Value.ToString() == tb_batchnumber.Text)
                    {
                        row.Cells[2].Value = Convert.ToInt32(row.Cells[2].Value) + tb_amount.Value;
                        return;
                    }
                }
                dataGridView.Rows.Add(cbx_partname.SelectedItem,tb_batchnumber.Text,tb_amount.Value,"Remove");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e != null)
                {
                    if (e.ColumnIndex == 3)
                    {
                        if (MessageBox.Show("Are you sure want to remove this!", "Aler!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            dataGridView.Rows.Remove(dataGridView.Rows[e.RowIndex]);
                            //remove database
                            //
                            //
                            //
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbx_partname.SelectedItem is null || cbx_warehouse is null || dataGridView.Rows.Count == 0)
                {
                    MessageBox.Show("Please fill all data before submit!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string suppliersid = "";
                string warehouseid = "";
                string transectiontypeid = "1";
                string query = "select ID from Suppliers where Name='" + cbx_suppliers.Text + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    suppliersid = reader[0].ToString();
                }
                reader.Close();

                query = "select ID from Warehouses where Name='" + cbx_warehouse.Text + "'";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    warehouseid = reader[0].ToString();
                }
                reader.Close();

                query = "insert into Orders values (" + transectiontypeid + "," + suppliersid + ",NULL," + warehouseid + ",'" + dateTimePicker.Text + "');SELECT SCOPE_IDENTITY();";
                cmd = new SqlCommand(query, conn);
                string orderid = "";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    orderid = reader[0].ToString();
                }
                reader.Close();

                string partid = "";
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    query = "select ID from Parts where Name='" + row.Cells[0].Value + "'";
                    cmd = new SqlCommand(query, conn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        partid = reader[0].ToString();
                    }
                    reader.Close();
                    query = "insert into OrderItems values (" + orderid + "," + partid + ",'" + row.Cells[1].Value + "'," + row.Cells[2].Value + ")";
                    cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Submit success!","Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Close();
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
    }
}
