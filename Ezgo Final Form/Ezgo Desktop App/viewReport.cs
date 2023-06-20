using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Ezgo_Desktop_App
{
    public partial class viewReport : Form
    {
        Methods mtd = new Methods();
        employee emp;
        DataTable dt;

        public viewReport(DataTable dt, employee emp)
        {
            InitializeComponent();
            this.dt = dt;
            this.emp = emp;
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            string address = mtd.getAddress(row);
            string fileContent;

            using (FileStream fileStream = new FileStream(address, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }
            }

            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load("C:\\Users\\DELL\\Documents\\VSCODE\\Ezgo Final Form\\Ezgo Desktop App\\Ezgo Desktop App\\CrystalReport9.rpt");

            TextObject textObject = (TextObject)reportDocument.ReportDefinition.ReportObjects["Text1"];
            textObject.Text = fileContent;

            reportView form = new reportView(emp);
            form.id = int.Parse(row.Cells["reportID"].Value.ToString());
            form.crystalReportViewer1.ReportSource = reportDocument;

            int code = int.Parse(row.Cells["statusID"].Value.ToString());

            if (code == 3) {
                form.deniedReport();
            }
            form.Show();
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
