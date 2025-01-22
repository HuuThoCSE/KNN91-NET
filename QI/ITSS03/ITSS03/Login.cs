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
        string connString = "server=THISPC\\THISPC;database=ITS03DATA;uid=sa;pwd=123456";
        private void Login_Load(object sender, EventArgs e)
        {
            tb_username.Text = "johndoe";
            tb_password.Text = "password123";
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            string Id;
            conn=new SqlConnection(connString);
            conn.Open();
            string query = "select ID,isAdmin from Employees where Username='"+tb_username.Text+"' and password='"+tb_password.Text+"'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                if (sqlDataReader[1].ToString() != "True")
                {
                    MessageBox.Show("You are not manager!", "Aler!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Id = sqlDataReader[0].ToString();
                EmergencyMaintenancesManagement form = new EmergencyMaintenancesManagement();
                //this.Hide();
                //form.Show();
                form.tet();
            }
            sqlDataReader.Close();
            conn.Close();
        }
    }
}
