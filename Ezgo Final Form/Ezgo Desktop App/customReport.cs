using CrystalDecisions.CrystalReports.Engine;
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
    public partial class customReport : Form
    {
        employee emp;
        reportMaker form6;
        Sales dash;
        int kode;

        public customReport(int code, employee emp, Sales dash)
        {
            InitializeComponent();
            this.emp = emp;

            switch (code)
            {
                case 1:
                    label3.Text = "New Product";
                    label1.Text = "Product Type";
                    label2.Text = "Name of product";
                    kode = 5;
                    break;
                case 2:
                    label3.Text = "Proposal for new Tour Package";
                    label1.Text = "Destination City";
                    label2.Text = "Number of Destinations";
                    kode = 6;
                    break;
                case 3:
                    label3.Text = "Proposal for new Marketing Plan";
                    label1.Text = "Product Type";
                    label2.Text = "Destination City";
                    kode = 7;
                    break;
            }

            this.dash = dash;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] arr = { textBox1.Text, textBox2.Text };

            if (emp is sales sls) {
                DataTable ds = new DataTable();
                ReportDocument rprt = sls.CreateReport8(4, kode, kode, ref ds, arr);
                form6 = new reportMaker(rprt, ds, 0, sls);
                form6.MdiParent = dash;
                form6.Show();
                this.Close();
            }
        }
    }
}
