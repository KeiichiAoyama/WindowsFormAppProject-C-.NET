using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Ezgo_Desktop_App
{
    public partial class viewStaff : Form
    {
        Methods mtd = new Methods();
        DataTable dt;

        public viewStaff()
        {
            InitializeComponent();
            dt = dataGridView1.DataSource as DataTable;
            string[] str1 = { "destID" };
            string[] str2 = { "jkt", "bdg", "sby", "dps" };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow[] selectedRows = dt.Select($"");
            DataTable newTable = selectedRows.CopyToDataTable();
            dataGridView1.DataSource = newTable;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
        }
    }
}
