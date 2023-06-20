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
    public partial class TourGuide : Form
    {
        tour employee;
        viewSchedule form;
        applySchedule form1;
        reportMaker form2;
        clearMembers form3;
        viewReport form4;
        int kode = 0;

        public TourGuide(employee employeeGet)
        {
            InitializeComponent();
            employee = new tour(employeeGet);
            switch (employee.branch)
            {
                case "jkt":
                    kode = 1;
                    break;
                case "bdg":
                    kode = 2;
                    break;
                case "dps":
                    kode = 4;
                    break;
                case "sby":
                    kode = 3;
                    break;
            }
        }

        private void myScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.viewSchedule();
            form = new viewSchedule();
            form.MdiParent = this;
            form.dataGridView1.DataSource = dt;
            form.Show();
        }

        private void applyScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.applySchedule();
            form1 = new applySchedule(employee, this, kode);
            form1.MdiParent = this;
            form1.dataGridView1.DataSource = dt;
            form1.Show();
        }

        private void jobDoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport1(2, kode, 2, ref ds);
            form2 = new reportMaker(rprt, ds, 1, employee);
            form2.MdiParent = this;
            form2.Show();
        }

        private void memberListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport11(3, kode, 3, ref ds);
            form2 = new reportMaker(rprt, ds, 8, employee);
            form2.MdiParent = this;
            form2.Show();
        }

        private void clearMembersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable ds = employee.getTourGroup();
            form3 = new clearMembers();
            form3.dataGridView1.DataSource = ds;
            form3.MdiParent = this;
            form3.Show();
        }

        private void sentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewReport(5);
            form4 = new viewReport(dt, employee);
            form4.MdiParent = this;
            form4.Show();
        }

        private void receivedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewReport(0);
            form4 = new viewReport(dt, employee);
            form4.MdiParent = this;
            form4.Show();
        }
    }
}
