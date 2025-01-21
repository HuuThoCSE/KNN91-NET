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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        private void Login_Load(object sender, EventArgs e)
        {
            tb_account.Text = "johndoe";
            tb_password.Text = "password123";

        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                string conString = "server=QBGamer;database=ITSS02DATA;uid=sa;pwd=1";
                conn = new SqlConnection(conString);
                conn.Open();
                string query = "select ID from Employees where Username='" + tb_account.Text + "' and Password='" + tb_password.Text + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    EmergencyMaintenancesManagement form = new EmergencyMaintenancesManagement(rdr[0].ToString());
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Wrong username or password!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
