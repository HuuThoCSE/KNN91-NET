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

namespace ITSS03
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection conn;

        public Boolean connect()
        {
            try
            {
                conn = new SqlConnection();
                conn.ConnectionString = "server=thisPC\\THISPC;uid=sa;pwd=123456;database=ITS03DATA";
                conn.Open();
                return true;
            }
            catch
            {
                MessageBox.Show("Connect database failed");
                return false;
            }
            
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want exit?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes) {
                Application.Exit();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string pwd = txtPassword.Text;
            if (connect()) {
                string user_type = "";
                int id_emp;
                string check_emp = "SELECT * FROM EMPLOYEES WHERE USERNAME = '" + username + "' AND PASSWORD ='" + pwd + "'";
                SqlCommand cm = new SqlCommand(check_emp, conn);
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    if (Convert.ToInt32(dr["ISADMIN"]) == 1)
                        user_type = "man";
                    else
                        user_type = "emp";

                    id_emp = Convert.ToInt32(dr["ID"].ToString());
                    login_data.type_user = user_type;
                    login_data.id_emp = id_emp;
                    
                    Emergency_Maintenance_Management emm = new Emergency_Maintenance_Management();
                    emm.Show();
                    this.Hide();
                }
            } else
            {
                MessageBox.Show("Login failed");
            }
            
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }
    }
}
