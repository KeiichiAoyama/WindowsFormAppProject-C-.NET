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
    public partial class reportView : Form
    {
        public int id = 0;
        Methods mtd = new Methods();
        employee emp;

        public reportView(employee emp)
        {
            InitializeComponent();
            this.emp = emp;
        }

        public void deniedReport() {
            button3.Enabled = true;
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Label[] lbl = { label1 };
            object[] obj = { "2" };
            string[] field = { "reportID" };
            object[] where = { id };

            if (mtd.Update(lbl, obj, field, where, 4, emp))
            {
                MessageBox.Show("Report Confirmed");
                this.Close();
            }
            else {
                MessageBox.Show("Report Unable to be Confirmed");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Label[] lbl = { label1 };
            object[] obj = { "3" };
            string[] field = { "reportID" };
            object[] where = { id };

            if (mtd.Update(lbl, obj, field, where, 4, emp))
            {
                ReportDenyReason form = new ReportDenyReason();
                form.setup(field, where, emp, this);
                form.Show();
            }
            else
            {
                MessageBox.Show("Report Unable to be Denied");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt = mtd.getReport(id);
            string reason = dt.Rows[0]["rReason"].ToString();
            ViewReason form = new ViewReason();
            form.textBox1.Text = reason;
            form.Show();
        }
    }
}
