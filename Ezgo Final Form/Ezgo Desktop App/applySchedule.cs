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
    public partial class applySchedule : Form
    {
        tour emp;
        TourGuide tour;
        reportMaker form;
        int kode = 0;

        public applySchedule(employee emp, TourGuide tour, int kode)
        {
            InitializeComponent();
            this.emp = new tour(emp);
            this.tour = tour;
            this.kode = kode;
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataTable ds = new DataTable();
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            string[] add = { row.Cells["tourID"].Value.ToString() };
            MessageBox.Show(add[0]);
            ReportDocument rprt = emp.CreateReport1(1, kode, 1, ref ds, add);
            form = new reportMaker(rprt, ds, 1, emp);
            form.MdiParent = tour;
            form.Show();
        }
    }
}
