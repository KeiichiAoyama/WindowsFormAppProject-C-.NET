using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ezgo_Desktop_App
{
    public partial class viewCustomer : Form
    {
        Methods mtd = new Methods();
        DataTable dt = new DataTable();

        public viewCustomer(DataTable dt)
        {
            InitializeComponent();
            this.dt = dt;
            dataGridView1.DataSource = this.dt;

            string[] columnNames = this.dt.Columns.Cast<DataColumn>()
                                .Select(column => column.ColumnName)
                                .ToArray();
            string[] str = { "ASC", "DESC" };
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
        }
    }
}
