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

namespace ITSS05
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn;
        string strConn = "server=thisPC\\THISPC;uid=sa;pwd=123456;database=ITSS05DATA";

        private void Form1_Load(object sender, EventArgs e)
        {
            try {
                conn = new SqlConnection(strConn);
                conn.Open();
                string qr = @"select format(o.Date, 'yyyy-MM') as date
	            from Orders o
	            group by format(o.Date, 'yyyy-MM')
	            order by date desc
            ";
                SqlCommand cmd = new SqlCommand(qr, conn);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    dgv1.Columns.Add(sdr[0].ToString(), sdr[0].ToString());
                    dgv2.Columns.Add(sdr[0].ToString(), sdr[0].ToString());
                    dgv3.Columns.Add(sdr[0].ToString(), sdr[0].ToString());
                }
                sdr.Close();
                qr = @"select
	            format(o.Date, 'yyyy-MM') as date,
	            d.Name,
	            format(sum(oi.UnitPrice), '0') as cost
	
	            from Orders o
	            join OrderItems oi on oi.OrderID = o.ID
	            left join EmergencyMaintenances em on em.ID = o.EmergencyMaintenancesID
	            left join Assets a on a.ID = em.AssetID
	            left join DepartmentLocations dl on dl.ID = a.DepartmentLocationID
	            left join Departments d on d.ID = dl.DepartmentID
	            group by format(o.Date, 'yyyy-MM'), d.Name
	            order by date desc
            ";
                cmd = new SqlCommand(qr, conn);
                sdr = cmd.ExecuteReader();
                bool mycheck = false;
                while (sdr.Read()) {
                    if (dgv1.Rows.Count == 0)
                    {
                        dgv1.Rows.Add(sdr[1]);
                        dgv1.Rows[0].Cells[dgv1.Columns[sdr[0].ToString()].Index].Value = sdr[2].ToString();
                    } else
                    {
                        mycheck = true;
                        foreach (DataGridViewRow rw in dgv1.Rows)
                        {
                            if (rw.Cells[0].Value.ToString() == sdr[1].ToString())
                            {
                                mycheck = false;
                                dgv1.Rows[rw.Index].Cells[dgv1.Columns[sdr[0].ToString()].Index].Value = sdr[2].ToString();
                                break;
                            }

                        }
                        if (mycheck)
                        {
                            dgv1.Rows.Add(sdr[1]);
                            dgv1.Rows[dgv1.Rows.Count - 1].Cells[dgv1.Columns[sdr[0].ToString()].Index].Value = sdr[2].ToString();
                        }
                    }
                } sdr.Close();
                for (int i = 1; i < dgv1.Columns.Count; i++)
                {
                    int min = int.MaxValue;
                    int max = -1;
                    int minidx = -1;
                    int maxidx = -1;
                    for (int j = 0; j < dgv1.Rows.Count; j++)
                    {
                        if (dgv1.Rows[j].Cells[i].Value != null && min > int.Parse(dgv1.Rows[j].Cells[i].Value.ToString()))
                        {
                            min = int.Parse(dgv1.Rows[j].Cells[i].Value.ToString());
                            minidx = j;
                        }
                        if (dgv1.Rows[j].Cells[i].Value != null && max < int.Parse(dgv1.Rows[j].Cells[i].Value.ToString()))
                        {
                            max = int.Parse(dgv1.Rows[j].Cells[i].Value.ToString());
                            maxidx = j;
                        }
                    }
                    if (minidx != -1)
                    {
                        dgv1.Rows[minidx].Cells[i].Style.ForeColor = Color.Green;
                    }
                    if (maxidx != -1)
                    {
                        dgv1.Rows[maxidx].Cells[i].Style.ForeColor = Color.Red;
                    }
                }
                foreach (DataGridViewRow rw in dgv1.Rows)
                {
                    for (int i = 0; i < dgv1.Columns.Count; i++)
                    {
                        if (rw.Cells[i].Value == null)
                        {
                            rw.Cells[i].Value = 0;
                        }
                    }
                }

                dgv2.Rows.Add("Higest Cost");
                dgv2.Rows.Add("Most Number");

                qr = @"select
	                format(o.Date, 'yyyy-MM') as date,
	                p.Name,
	                cast(sum(oi.UnitPrice) as int) as cost
	
	                from Orders o
	                join OrderItems oi on oi.OrderID = o.ID
	                left join EmergencyMaintenances em on em.ID = o.EmergencyMaintenancesID
	                left join Assets a on a.ID = em.AssetID
	                left join DepartmentLocations dl on dl.ID = a.DepartmentLocationID
	                left join Departments d on d.ID = dl.DepartmentID
	                left join Parts p on p.ID = oi.PartID
	                group by format(o.Date, 'yyyy-MM'), p.Name
	                order by date desc, cost desc
                ";
                cmd = new SqlCommand(qr, conn);
                sdr = cmd.ExecuteReader();
                int temp = 0;
                while (sdr.Read()) {
                    if (dgv2.Rows[0].Cells[sdr[0].ToString()].Value == null)
                    {
                        temp = int.Parse(sdr[2].ToString());
                        dgv2.Rows[0].Cells[sdr[0].ToString()].Value = sdr[1].ToString();
                    } else
                    {
                        if (temp == int.Parse(sdr[2].ToString()))
                        {
                            dgv2.Rows[0].Cells[sdr[0].ToString()].Value = dgv2.Rows[0].Cells[sdr[0].ToString()].Value.ToString() + ", " + sdr[1].ToString();
                        }
                    }
                }
                sdr.Close();

                qr = @"select
	                format(o.Date, 'yyyy-MM') as date,
	                p.Name,
	                count(p.Name) as count
	
	                from Orders o
	                join OrderItems oi on oi.OrderID = o.ID
	                left join EmergencyMaintenances em on em.ID = o.EmergencyMaintenancesID
	                left join Assets a on a.ID = em.AssetID
	                left join DepartmentLocations dl on dl.ID = a.DepartmentLocationID
	                left join Departments d on d.ID = dl.DepartmentID
	                left join Parts p on p.ID = oi.PartID
	                group by format(o.Date, 'yyyy-MM'), p.Name
	                order by date desc, count desc
                ";
                cmd = new SqlCommand(qr, conn);
                sdr = cmd.ExecuteReader();
                temp = 0;
                while (sdr.Read())
                {
                    if (dgv2.Rows[1].Cells[sdr[0].ToString()].Value == null)
                    {
                        temp = int.Parse(sdr[2].ToString());
                        dgv2.Rows[1].Cells[sdr[0].ToString()].Value = sdr[1].ToString();
                    }
                    else
                    {
                        if (temp == int.Parse(sdr[2].ToString()))
                        {
                            dgv2.Rows[1].Cells[sdr[0].ToString()].Value = dgv2.Rows[1].Cells[sdr[0].ToString()].Value.ToString() + ", " + sdr[1].ToString();
                        }
                    }
                }
                sdr.Close();

                dgv3.Rows.Add("Asset");
                dgv3.Rows.Add("Department");
                qr = @"
                select
	                format(o.Date, 'yyyy-MM') as date,
	                a.AssetName,
	                d.Name,
	                count(a.AssetName) as count
	            from Orders o
	            join OrderItems oi on oi.OrderID = o.ID
	            left join EmergencyMaintenances em on em.ID = o.EmergencyMaintenancesID
	            left join Assets a on a.ID = em.AssetID
	            left join DepartmentLocations dl on dl.ID = a.DepartmentLocationID
	            left join Departments d on d.ID = dl.DepartmentID
	            left join Parts p on p.ID = oi.PartID
	            group by format(o.Date, 'yyyy-MM'), a.AssetName, d.Name
	            order by date desc, count desc
                ";
                cmd = new SqlCommand(qr, conn);
                sdr = cmd.ExecuteReader();
                temp = 0;
                while (sdr.Read())
                {
                    if (dgv3.Rows[0].Cells[sdr[0].ToString()].Value == null)
                    {
                        temp = int.Parse(sdr[3].ToString());
                        dgv3.Rows[0].Cells[sdr[0].ToString()].Value = sdr[1].ToString();
                        dgv3.Rows[1].Cells[sdr[0].ToString()].Value = sdr[2].ToString();
                    } 
                    else if(temp ==  int.Parse(sdr[3].ToString()))
                    {
                        dgv3.Rows[0].Cells[sdr[0].ToString()].Value = dgv3.Rows[0].Cells[sdr[0].ToString()].Value.ToString() + ", " + sdr[1].ToString();
                        dgv3.Rows[1].Cells[sdr[0].ToString()].Value = dgv3.Rows[1].Cells[sdr[0].ToString()].Value.ToString() + ", " + sdr[1].ToString();
                    }
                }
                sdr.Close();

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInventoryControl_Click(object sender, EventArgs e)
        {
          FrmInventoryControl inventoryControl = new FrmInventoryControl();
            inventoryControl.Show();
            this.Hide();
        }
    }
}
