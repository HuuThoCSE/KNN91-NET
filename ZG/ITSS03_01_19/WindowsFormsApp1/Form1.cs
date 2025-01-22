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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cnn;
        SqlCommand cmd;

        public Boolean Connect()
        {
            try
            {
                (cnn = new SqlConnection(sql_cf.strConn)).Open();
                return true;
            } catch {
                MessageBox.Show("Error connect database.");
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Connect()) { 
                return;
            }

            string username = tbUsername.Text;
            string pwd = tbPassword.Text;
            string user_type = "";
            int emp_id;
            //string query = "SELECT * FROM EMPLOYEES where USERNAME = '" + username + "' AND PASSWORD = '" + pwd + "'";

            string query = "select * from EMPLOYEES where USERNAME = @username AND PASSWORD = @password";
            cmd = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", pwd);

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                if (Convert.ToInt32(sdr["ISADMIN"]) == 1)
                    user_type = "man";
                else
                    user_type = "emp";
                emp_id = Convert.ToInt32(sdr["ID"].ToString());
                emp_info.emp_type = user_type;
                emp_info.emp_id = emp_id;

                Form2 form2 = new Form2();
                form2.Show();
                this.Hide();
            } 
            else MessageBox.Show("Username and password invalid");

            sdr.Close(); // đóng SqlDataReader sau khi sử dụng
            cnn.Close(); // Đóng kết nối sau khi hoàn thành

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
