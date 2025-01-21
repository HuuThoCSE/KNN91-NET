using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITSS04
{
    internal class conn
    {
        public static string StrCnn = @"Server=thisPC\THISPC; uid=sa; pwd=123456; database=ITSS04DATA";


        public static SqlConnection GetCnn()
        {
            try
            {
                SqlConnection conn = new SqlConnection(StrCnn);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }
    }
}
