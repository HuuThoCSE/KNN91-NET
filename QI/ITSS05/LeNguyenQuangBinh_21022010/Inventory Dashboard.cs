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
    public partial class InventoryDashboard : Form
    {
        public InventoryDashboard()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        string connString = "server=QBGamer;database=ITSS05DATA;uid=sa;pwd=1";
        private void InventoryDashboard_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(connString);
                conn.Open();
                string query = "select format(o.Date,'yyyy-MM') as date from Orders o group by format(o.Date,'yyyy-MM') order by date desc";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    dataGridView1.Columns.Add(sqlDataReader[0].ToString(), sqlDataReader[0].ToString());
                    dataGridView2.Columns.Add(sqlDataReader[0].ToString(), sqlDataReader[0].ToString());
                    dataGridView3.Columns.Add(sqlDataReader[0].ToString(), sqlDataReader[0].ToString());
                }
                sqlDataReader.Close();
                query = "select\r\n\tformat(o.Date,'yyyy-MM') as date,\r\n\td.Name,\r\n\tformat(sum(oi.UnitPrice),'0') as cost\r\nfrom Orders o\r\njoin OrderItems oi on oi.OrderID=o.ID\r\nleft join EmergencyMaintenances em on em.ID=o.EmergencyMaintenancesID\r\nleft join Assets a on a.ID=em.AssetID\r\nleft join DepartmentLocations dl on dl.ID=a.DepartmentLocationID\r\nleft join Departments d on d.ID=dl.DepartmentID\r\ngroup by format(o.Date,'yyyy-MM'),d.Name\r\norder by date desc";
                cmd = new SqlCommand(query, conn);
                sqlDataReader = cmd.ExecuteReader();
                bool mycheck = true;
                while (sqlDataReader.Read())
                {
                    if (dataGridView1.Rows.Count == 0)
                    {
                        dataGridView1.Rows.Add(sqlDataReader[1]);
                        dataGridView1.Rows[0].Cells[dataGridView1.Columns[sqlDataReader[0].ToString()].Index].Value = sqlDataReader[2].ToString();
                    }
                    else
                    {
                        mycheck = true;
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[0].Value.ToString() == sqlDataReader[1].ToString())
                            {
                                mycheck = false;
                                dataGridView1.Rows[row.Index].Cells[dataGridView1.Columns[sqlDataReader[0].ToString()].Index].Value = sqlDataReader[2].ToString();
                                break;
                            }
                        }
                        if (mycheck)
                        {
                            dataGridView1.Rows.Add(sqlDataReader[1]);
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dataGridView1.Columns[sqlDataReader[0].ToString()].Index].Value = sqlDataReader[2].ToString();
                        }
                    }
                }
                sqlDataReader.Close();
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    int min = int.MaxValue;
                    int max = -1;
                    int minidx = -1;
                    int maxidx = -1;
                    for (int j = 0; j < dataGridView1.Rows.Count; j++)
                    {
                        if (dataGridView1.Rows[j].Cells[i].Value != null && min > int.Parse(dataGridView1.Rows[j].Cells[i].Value.ToString()))
                        {
                            min = int.Parse(dataGridView1.Rows[j].Cells[i].Value.ToString());
                            minidx = j;
                        }
                        if (dataGridView1.Rows[j].Cells[i].Value != null && max < int.Parse(dataGridView1.Rows[j].Cells[i].Value.ToString()))
                        {
                            max = int.Parse(dataGridView1.Rows[j].Cells[i].Value.ToString());
                            maxidx = j;
                        }
                    }
                    if (minidx != -1)
                    {
                        dataGridView1.Rows[minidx].Cells[i].Style.ForeColor = Color.Green;
                    }
                    if (maxidx != -1)
                    {
                        dataGridView1.Rows[maxidx].Cells[i].Style.ForeColor = Color.Red;
                    }
                }
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    for (int i = 1; i < dataGridView1.Columns.Count; i++)
                    {
                        if (row.Cells[i].Value == null)
                        {
                            row.Cells[i].Value = 0;
                        }
                    }
                }
                dataGridView2.Rows.Add("Highest Cost");
                dataGridView2.Rows.Add("Most Number");

                query = "select\r\n\tformat(o.Date,'yyyy-MM') as date,\r\n\tp.Name,\r\n\tcast(sum(oi.UnitPrice) as int) as cost\r\nfrom Orders o\r\njoin OrderItems oi on oi.OrderID=o.ID\r\nleft join EmergencyMaintenances em on em.ID=o.EmergencyMaintenancesID\r\nleft join Assets a on a.ID=em.AssetID\r\nleft join DepartmentLocations dl on dl.ID=a.DepartmentLocationID\r\nleft join Departments d on d.ID=dl.DepartmentID\r\nleft join Parts p on p.ID=oi.PartID\r\ngroup by format(o.Date,'yyyy-MM'),p.Name\r\norder by date desc,cost desc";
                cmd = new SqlCommand(query, conn);
                sqlDataReader = cmd.ExecuteReader();
                int temp = 0;
                while (sqlDataReader.Read())
                {
                    if (dataGridView2.Rows[0].Cells[sqlDataReader[0].ToString()].Value == null)
                    {
                        temp = int.Parse(sqlDataReader[2].ToString());
                        dataGridView2.Rows[0].Cells[sqlDataReader[0].ToString()].Value = sqlDataReader[1].ToString();
                    }
                    else
                    {
                        if (temp == int.Parse(sqlDataReader[2].ToString()))
                        {
                            dataGridView2.Rows[0].Cells[sqlDataReader[0].ToString()].Value = dataGridView2.Rows[0].Cells[sqlDataReader[0].ToString()].Value.ToString() + ", " + sqlDataReader[1].ToString();
                        }
                    }
                }
                sqlDataReader.Close();

                query = "select\r\n\tformat(o.Date,'yyyy-MM') as date,\r\n\tp.Name,\r\n\tcount(p.name) as count\r\nfrom Orders o\r\njoin OrderItems oi on oi.OrderID=o.ID\r\nleft join EmergencyMaintenances em on em.ID=o.EmergencyMaintenancesID\r\nleft join Assets a on a.ID=em.AssetID\r\nleft join DepartmentLocations dl on dl.ID=a.DepartmentLocationID\r\nleft join Departments d on d.ID=dl.DepartmentID\r\nleft join Parts p on p.ID=oi.PartID\r\ngroup by format(o.Date,'yyyy-MM'),p.Name\r\norder by date desc,count desc";
                cmd = new SqlCommand(query, conn);
                sqlDataReader = cmd.ExecuteReader();
                temp = 0;
                while (sqlDataReader.Read())
                {
                    if (dataGridView2.Rows[1].Cells[sqlDataReader[0].ToString()].Value == null)
                    {
                        temp = int.Parse(sqlDataReader[2].ToString());
                        dataGridView2.Rows[1].Cells[sqlDataReader[0].ToString()].Value = sqlDataReader[1].ToString();
                    }
                    else
                    {
                        if (temp == int.Parse(sqlDataReader[2].ToString()))
                        {
                            dataGridView2.Rows[1].Cells[sqlDataReader[0].ToString()].Value = dataGridView2.Rows[1].Cells[sqlDataReader[0].ToString()].Value.ToString() + ", " + sqlDataReader[1].ToString();
                        }
                    }
                }sqlDataReader.Close();

                dataGridView3.Rows.Add("Asset");
                dataGridView3.Rows.Add("Department");
                query = "select\r\n\tformat(o.Date,'yyyy-MM') as date,\r\n\ta.AssetName,\r\n\td.Name,\r\n\tcount(a.AssetName) as count\r\nfrom Orders o\r\nleft join EmergencyMaintenances em on em.ID=o.EmergencyMaintenancesID\r\nleft join Assets a on a.ID=em.AssetID\r\nleft join DepartmentLocations dl on dl.ID=a.DepartmentLocationID\r\nleft join Departments d on d.ID=dl.DepartmentID\r\ngroup by format(o.Date,'yyyy-MM'),a.AssetName,d.Name\r\norder by date desc,count desc";
                cmd = new SqlCommand(query, conn);
                sqlDataReader = cmd.ExecuteReader();
                temp = 0;
                while (sqlDataReader.Read())
                {
                    if (dataGridView3.Rows[0].Cells[sqlDataReader[0].ToString()].Value == null)
                    {
                        temp = int.Parse(sqlDataReader[3].ToString());
                        dataGridView3.Rows[0].Cells[sqlDataReader[0].ToString()].Value=sqlDataReader[1].ToString();
                        dataGridView3.Rows[1].Cells[sqlDataReader[0].ToString()].Value = sqlDataReader[2].ToString();
                    }
                    else if (temp == int.Parse(sqlDataReader[3].ToString()))
                    {
                        dataGridView3.Rows[0].Cells[sqlDataReader[0].ToString()].Value = dataGridView3.Rows[0].Cells[sqlDataReader[0].ToString()].Value.ToString() + ", " + sqlDataReader[1].ToString();
                        dataGridView3.Rows[1].Cells[sqlDataReader[0].ToString()].Value = dataGridView3.Rows[1].Cells[sqlDataReader[0].ToString()].Value.ToString() + ", " + sqlDataReader[1].ToString();
                    }
                }sqlDataReader.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InventoryDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btn_invenControl_Click(object sender, EventArgs e)
        {
            InventoryControl f = new InventoryControl();
            this.Hide();
            f.Show();
        }
    }
}
