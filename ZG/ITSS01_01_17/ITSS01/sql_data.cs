using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSS01
{
    internal class sql_data
    {
        public static string strConn { get; } = "server=thisPC\\THISPC;database=ITS01DATA;uid=sa;pwd=123456";
    
        public static bool Connect(out SqlConnection cnn)
        {
            cnn = new SqlConnection(strConn);

            try
            {
                cnn.Open();
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine("Connect error: " + ex.Message);
                return false;
            }
        }
    
    
    }
}
