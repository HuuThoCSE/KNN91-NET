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

namespace ITSS02
{
    public partial class form_login : Form
    {
        public form_login()
        {
            InitializeComponent();
        }
        SqlConnection conn;
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

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txt_name.Text;
            string pw = txt_pass.Text;
            if(connect())
            {
                string user_type = "";
                int id_emp;
                string check_emp = "SELECT * FROM EMPLOYEES WHERE USERNAME = '"+name+"' AND PASSWORD ='"+pw+"'";
                SqlCommand cm = new SqlCommand(check_emp, conn);
                SqlDataReader rdr = cm.ExecuteReader();
                if(rdr.Read())
                {
                    //kiem tra user type
                    if (Convert.ToInt32(rdr["ISADMIN"]) == 1)
                    {
                        user_type = "man";
                        
                    }
                    else
                    {
                        user_type ="emp";
                       
                    }
                    //dang nhap vao form asset list
                    id_emp = Convert.ToInt32(rdr["ID"].ToString());
                    login_info.type_user = user_type;
                    login_info.id_emp = id_emp;

                    Emergency_management em = new Emergency_management();
                    em.Show();
                    this.Hide();     
                }
                else
                {
                    MessageBox.Show("Login failed");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("do you want to exit?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if(dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void form_login_Load(object sender, EventArgs e)
        {

        }
    }
}
