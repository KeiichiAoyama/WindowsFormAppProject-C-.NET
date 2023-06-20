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
    public partial class ReportDenyReason : Form
    {
        Label[] lbl;
        public string[] str;
        public object[] obj, where;
        Methods mtd = new Methods();
        employee emp;
        reportView rv;

        public ReportDenyReason()
        {
            InitializeComponent();
            Label label = new Label();
            label.Text = "rReason";
            lbl = new Label[] { label };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            obj = new object[] { textBox1.Text };
            if (mtd.Update(lbl, obj, str, where, 4, emp)) {
                MessageBox.Show("The Report Has Been Denied");
                rv.Close();
                this.Close();
            }
        }

        public void setup (string[] str, object[] where, employee emp, reportView rv) {
            this.str = str;
            this.where = where;
            this.emp = emp;
            this.rv = rv;
        }
    }
}
