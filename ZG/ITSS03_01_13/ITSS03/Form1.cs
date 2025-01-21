using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ITSS03
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string name = txtUser.Text;
            string pass = txtPwd.Text;
            if (SqlConnManager.IsConnected())
            {
                MessageBox.Show("Connected");

                string user_type = ""; // Kiểu người dùng ở đây là "man" hoặc "emp"
                int id_emp;
                string check_emp = "select * from EMPLOYEES where USERNAME = '" + name + "' and PASSWORD = '" + pass + "'";
                SqlCommand scm = new SqlCommand(check_emp, SqlConnManager.conn);
                SqlDataReader sdr = scm.ExecuteReader();
                if (sdr.Read())
                {
                    // Kiểm tra kiểu người dùng (user type)
                    if (Convert.ToInt32(sdr["ISADMIN"]) == 1)
                    {
                        user_type = "man";
                    }
                    else
                    {
                        user_type = "emp";
                    }

                    // Đăng nhập vào Form Asset List
                    id_emp = Convert.ToInt32(sdr["ID"].ToString());
                    login_info.type_user = user_type;
                    login_info.id_emp = id_emp;

                    EmergencyMaintenanceManagement em = new EmergencyMaintenanceManagement();
                    em.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Login failed");
                }
            }
            else
            {
                MessageBox.Show("Not connected");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to exit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dr == DialogResult.Yes)
            {
                Application.Exit();
                this.Close();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
