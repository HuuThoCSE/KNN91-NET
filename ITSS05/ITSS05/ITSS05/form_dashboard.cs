using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ITSS05
{
    public partial class form_dasboard : Form
    {
        public form_dasboard()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        public Boolean connect()
        {
            try
            {
                conn = new SqlConnection();
                conn.ConnectionString = "server= NGANAMY-PC; database=ITSS05; uid=sa; pwd=123";
                conn.Open();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void load_list1()
        {
            if (connect())
            {
                // load month
                string sql = "select distinct " +
                    "convert(varchar,MONTH(date))+'/'+convert(varchar,year(date))" +
                    "from orders";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgv_spend.Columns.Add("columnde", "Department / Month");
                foreach (DataRow dr in dt.Rows)
                {
                    dgv_spend.Columns.Add("column" + dr[0].ToString(), dr[0].ToString());
                }
                string date = "";
                string month = "";
                string year = "";
            
                // Hiển thị trước 2 cột đầu tiên
                date = dgv_spend.Columns[1].HeaderText;
                month = date.Split('/')[0] ;
                year = date.Split('/')[1] ;
                string sql_displayrow = "SELECT dp.name," +
                    "SUM(CASE WHEN YEAR(od.date) = " + year + " AND MONTH(od.date) = " + month + " THEN odi.amout * odi.unitprice ELSE 0 END) " +
                    " FROM departments dp" +
                    " JOIN deparmentlocations dpl ON dpl.departmentid = dp.id " +
                    " JOIN assets ass ON ass.departmentlocationid = dpl.id " +
                    " JOIN emergencymaintenances em ON em.assetid = ass.id " +
                    " JOIN orders od ON em.id = od.emergencymaintanancesid" +
                    " JOIN orderitems odi ON odi.orderid = od.id" +
                    " WHERE enddate IS NOT NULL " +
                    " GROUP BY dp.name";
                SqlCommand cmd_1 = new SqlCommand(sql_displayrow, conn);
                SqlDataAdapter adapter_1 = new SqlDataAdapter(cmd_1);
                DataTable dt_1 = new DataTable();
                adapter_1.Fill(dt_1);
                foreach (DataRow dr in dt_1.Rows)
                {
                    int n = dgv_spend.Rows.Add();
                    dgv_spend.Rows[n].Cells[0].Value = dr[0].ToString();
                    dgv_spend.Rows[n].Cells[1].Value = dr[1].ToString();
                }
                // hiển thị những cột tiếp theo
               for(int i = 2;i<dgv_spend.ColumnCount;i++)
               {
                    date = dgv_spend.Columns[i].HeaderText;
                    month = date.Split('/')[0];
                    year = date.Split('/')[1];
                    string sql_displayrow_2 = "SELECT dp.name," +
                        "SUM(CASE WHEN YEAR(od.date) = " + year + " AND MONTH(od.date) = " + month + " THEN odi.amout * odi.unitprice ELSE 0 END) " +
                        " FROM departments dp" +
                        " JOIN deparmentlocations dpl ON dpl.departmentid = dp.id " +
                        " JOIN assets ass ON ass.departmentlocationid = dpl.id " +
                        " JOIN emergencymaintenances em ON em.assetid = ass.id " +
                        " JOIN orders od ON em.id = od.emergencymaintanancesid" +
                        " JOIN orderitems odi ON odi.orderid = od.id" +
                        " WHERE enddate IS NOT NULL " +
                        " GROUP BY dp.name";
                    SqlCommand cmd_2 = new SqlCommand(sql_displayrow_2, conn);
                    SqlDataAdapter adapter_2 = new SqlDataAdapter(cmd_2);
                    DataTable dt_2 = new DataTable();
                    adapter_2.Fill(dt_2);
                    int j = 0;
                    foreach (DataRow dr in dt_2.Rows)
                    {
                        
                        dgv_spend.Rows[j].Cells[i].Value = dr[1].ToString();
                        j++;
                    }
                }
               int max, min;
               //mark color for highest and lowest
               for(int i = 1; i<dgv_spend.ColumnCount; i++)
                {
                    max = min = 0;
                    
                    for (int j = 0;j<dgv_spend.Rows.Count-1; j++)
                    {
                        //find max
                        if(Convert.ToInt32( dgv_spend.Rows[j].Cells[i].Value ) > Convert.ToInt32(dgv_spend.Rows[max].Cells[i].Value))
                        {
                            max = j;
                        }

                        //find min
                        if (Convert.ToInt32(dgv_spend.Rows[j].Cells[i].Value) < Convert.ToInt32(dgv_spend.Rows[min].Cells[i].Value))
                        {
                            min = j;
                        }
                        
                    }
                    if (Convert.ToInt32(dgv_spend.Rows[max].Cells[i].Value) != 0)
                    {
                        dgv_spend.Rows[max].Cells[i].Style.ForeColor = Color.Red;
                    }
                    if(Convert.ToInt32(dgv_spend.Rows[min].Cells[i].Value) != 0)
                    {
                        dgv_spend.Rows[min].Cells[i].Style.ForeColor = Color.Green;

                    }
                }
            }
        }
       
        public void load_list2()
        {
            if(connect())
            {
                // load month and title 
                string sql = "select distinct " +
                    "convert(varchar,MONTH(date))+'/'+convert(varchar,year(date))" +
                    "from orders";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgv_most.Columns.Add("column0", "Notes / Month");
                foreach (DataRow dr in dt.Rows)
                {
                    dgv_most.Columns.Add("column" + dr[0].ToString(), dr[0].ToString());
                }
                int n = dgv_most.Rows.Add();
                dgv_most.Rows[n].Cells[0].Value = "Highest Cost";
                dgv_most.Rows[n+1].Cells[0].Value = "Most Number";

                //load part theo yêu cầu
                string date = "";
                string month = "";
                string year = "";
                for (int i=1; i< dgv_most.Columns.Count; i++)
                {
                    date = dgv_most.Columns[i].HeaderText;
                    month = date.Split('/')[0];
                    year = date.Split('/')[1];
                    
                    //highest cost
                    string sql_hightcost = "SELECT top(1) p.name," +
                        "sum(CASE WHEN YEAR(od.date) = "+year+" AND MONTH(od.date) = "+month+" THEN odi.amout * odi.unitprice ELSE 0 END) AS sum_cost" +
                        "\r\nFROM parts p" +
                        "\r\nJOIN orderitems odi ON odi.partid = p.id" +
                        "\r\nJOIN orders od ON odi.orderid = od.id" +
                        "\r\n JOIN emergencymaintenances em ON em.id = od.emergencymaintanancesid" +
                        "\r\nWHERE em.emenddate IS NOT NULL" +
                        "\r\nGROUP BY p.name" +
                        "\r\norder by  sum_cost desc";
                    SqlCommand cmd_cost = new SqlCommand(sql_hightcost, conn);
                    SqlDataAdapter adt_cost = new SqlDataAdapter(cmd_cost);
                    DataTable dt_cost = new DataTable();
                    adt_cost.Fill(dt_cost);
                   
                    foreach(DataRow dr in dt_cost.Rows) {
                        if (dr[0].ToString() == "0")
                        {
                            dgv_most.Rows[0].Cells[i].Value = "";
                        }
                        else
                        {
                            dgv_most.Rows[0].Cells[i].Value = dr[0].ToString();
                        }
                    }

                    //most number
                    string sql_mostnumber = "SELECT top(1) p.name," +
                        "sum(CASE WHEN YEAR(od.date) = " + year + " AND MONTH(od.date) = " + month + " THEN odi.amout ELSE 0 END) AS count_part" +
                        "\r\nFROM parts p" +
                        "\r\nJOIN orderitems odi ON odi.partid = p.id" +
                        "\r\nJOIN orders od ON odi.orderid = od.id" +
                        "\r\n JOIN emergencymaintenances em ON em.id = od.emergencymaintanancesid" +
                        "\r\nWHERE em.emenddate IS NOT NULL" +
                        "\r\nGROUP BY p.name" +
                        "\r\norder by  count_part desc"; ;
                    SqlCommand cmd_count = new SqlCommand(sql_mostnumber, conn);
                    SqlDataAdapter adt_count = new SqlDataAdapter(cmd_count);
                    DataTable dt_count = new DataTable();
                    adt_count.Fill(dt_count);

                    foreach (DataRow dr in dt_count.Rows)
                    {
                        if (dr[0].ToString() == "0")
                        {
                            dgv_most.Rows[1].Cells[i].Value = "";
                        }
                        else
                        {
                            dgv_most.Rows[1].Cells[i].Value = dr[0].ToString();
                        }
                    }
                }
            }    
        }

        public void load_list3()
        {
            if (connect())
            {
                // load month and title 
                string sql = "select distinct " +
                    "convert(varchar,MONTH(date))+'/'+convert(varchar,year(date))" +
                    "from orders";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgv_cost.Columns.Add("column0", "Asset Name / Month");
                foreach (DataRow dr in dt.Rows)
                {
                    dgv_cost.Columns.Add("column" + dr[0].ToString(), dr[0].ToString());
                }
                int n = dgv_cost.Rows.Add();
                dgv_cost.Rows[n].Cells[0].Value = "Asset";
                dgv_cost.Rows[n + 1].Cells[0].Value = "Deparment";

                string date = "";
                string month = "";
                string year = "";
                for (int i = 1; i < dgv_cost.Columns.Count; i++)
                {
                    date = dgv_cost.Columns[i].HeaderText;
                    month = date.Split('/')[0];
                    year = date.Split('/')[1];

                    //most Asset
                    string sql_asset = "SELECT top(1) ass.assetname," +
                        "sum(CASE WHEN YEAR(od.date) = " + year + " AND MONTH(od.date) = " + month + " THEN odi.amout ELSE 0 END) AS count_part" +
                        "\r\nFROM assets ass" +
                       "\r\nJOIN emergencymaintenances em ON em.assetid = ass.id " +
                        "\r\nJOIN orders od ON em.id = od.emergencymaintanancesid " +
                        "\r\nJOIN orderitems odi ON odi.orderid = od.id" +
                        "\r\nWHERE em.emenddate IS NOT NULL" +
                        "\r\nGROUP BY ass.assetname" +
                        "\r\norder by  count_part desc"; 
                    SqlCommand cmd_asset = new SqlCommand(sql_asset, conn);
                    SqlDataAdapter adt_asset = new SqlDataAdapter(cmd_asset);
                    DataTable dt_asset = new DataTable();
                    adt_asset.Fill(dt_asset);

                    foreach (DataRow dr in dt_asset.Rows)
                    {
                        if (dr[1].ToString() == "0")
                        {
                            dgv_cost.Rows[0].Cells[i].Value = "";
                        }
                        else
                        {
                            dgv_cost.Rows[0].Cells[i].Value = dr[0].ToString();
                        }
                    }

                    //Most Department
                    string sql_depart = "SELECT top (1) dp.name," +
                    "SUM(CASE WHEN YEAR(od.date) = " + year + " AND MONTH(od.date) = " + month + " THEN odi.amout * odi.unitprice ELSE 0 END) as count_part " +
                    " FROM departments dp" +
                    " JOIN deparmentlocations dpl ON dpl.departmentid = dp.id " +
                    " JOIN assets ass ON ass.departmentlocationid = dpl.id " +
                    " JOIN emergencymaintenances em ON em.assetid = ass.id " +
                    " JOIN orders od ON em.id = od.emergencymaintanancesid" +
                    " JOIN orderitems odi ON odi.orderid = od.id" +
                    " WHERE enddate IS NOT NULL " +
                    " GROUP BY dp.name"+
                    " order by  count_part desc"; ;
                    SqlCommand cmd_depart = new SqlCommand(sql_depart, conn);
                    SqlDataAdapter adt_depart = new SqlDataAdapter(cmd_depart);
                    DataTable dt_depart = new DataTable();
                    adt_depart.Fill(dt_depart);

                    foreach (DataRow dr in dt_depart.Rows)
                    {
                        if (dr[1].ToString() == "0")
                        {
                            dgv_cost.Rows[1].Cells[i].Value = "";
                        }
                        else
                        {
                            dgv_cost.Rows[1].Cells[i].Value = dr[0].ToString();
                        }
                    }
                }
            }
        }
        public void load_language(string language)
        {
            string filepath = $"D:\\KỸ NĂNG NGHỀ\\ONTHI\\Module2\\ITSS05\\ITSS05\\ITSS05\\Languages\\{language}.xml";
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filepath);

               XmlElement buttonelement = xmlDocument.SelectSingleNode("//button") as XmlElement;
               string buttonvalue = buttonelement.GetAttribute("value").ToString();

                bt_inven.Text = buttonvalue;

            }
            catch
            {
                MessageBox.Show("Error when load language");
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string language = cbb_lang.SelectedItem.ToString();
            load_language(language);

        }

        private void form_dasboard_Load(object sender, EventArgs e)
        {
            load_list1();
            load_list2();
            load_list3();

            string folderpath = "D:\\KỸ NĂNG NGHỀ\\ONTHI\\Module2\\ITSS05\\ITSS05\\ITSS05\\Languages\\";
            if(Directory.Exists(folderpath))
            {
                string[] xmlfile = Directory.GetFiles(folderpath,"*.xml");

                foreach (string file in xmlfile) { 
                    string filename = Path.GetFileNameWithoutExtension(file);
                    cbb_lang.Items.Add(filename);
                }
            }
            else
            {
                MessageBox.Show("ko tim thay");
            }
        }
    }
}
