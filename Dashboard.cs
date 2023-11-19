using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace KSmarketing_system
{
    public partial class Dashboard : Form
    {
        SqlConnection cn = new SqlConnection();
        DBConnect dbcon = new DBConnect();

        public Dashboard()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            string sdate = DateTime.Now.ToShortDateString();
           
        }

    }
}
