using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITSS03
{
    public static class SqlConnManager
    {
        public static SqlConnection conn { get; private set; }

        static SqlConnManager()
        {
            conn = new SqlConnection("server=thisPC\\THISPC; database=ITS03DATA; uid=sa; pwd=123456; MultipleActiveResultSets=True");
            try
            {
                //if(conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                //MessageBox.Show("Connected");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public static bool IsConnected() => conn?.State == System.Data.ConnectionState.Open;
    }
}
