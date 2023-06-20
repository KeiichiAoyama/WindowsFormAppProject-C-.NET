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
    public partial class Sales : Form
    {
        sales sls;
        viewStock form;
        updateStock form2;
        viewStaff form3;
        updateStaff form4;
        viewReport form5;
        reportMaker form6;
        viewCustomer form7;
        customReport form10;
        assignGuide form11;
        Transaction form12;
        int kode = 0;

        public Sales(employee employeeGet)
        {
            InitializeComponent();
            sls = new sales(employeeGet);
            switch (sls.branch)
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

        private void viewStocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = sls.ViewStock(1, 1);
            form = new viewStock();
            form.MdiParent = this;
            form.dataGridView1.DataSource = dt;
            form.Show();
        }

        private void updateStocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = sls.ViewStock(1, 1);
            form2 = new updateStock(sls, 1);
            form2.MdiParent = this;
            form2.dataGridView1.DataSource = dt;
            form2.Show();
        }

        private void viewStocksToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataTable dt = sls.ViewStock(1, 2);
            form = new viewStock();
            form.MdiParent = this;
            form.dataGridView1.DataSource = dt;
            form.Show();
        }

        private void updateStocksToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataTable dt = sls.ViewStock(1, 2);
            form2 = new updateStock(sls, 1);
            form2.MdiParent = this;
            form2.dataGridView1.DataSource = dt;
            form2.Show();
        }

        private void viewStocksToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataTable dt = sls.ViewStock(1, 3);
            form = new viewStock();
            form.MdiParent = this;
            form.dataGridView1.DataSource = dt;
            form.Show();
        }

        private void updateStocksToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataTable dt = sls.ViewStock(1, 3);
            form2 = new updateStock(sls, 1);
            form2.MdiParent = this;
            form2.dataGridView1.DataSource = dt;
            form2.Show();
        }

        private void viewStocksToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DataTable dt = sls.ViewStock(2);
            form = new viewStock();
            form.MdiParent = this;
            form.dataGridView1.DataSource = dt;
            form.Show();
        }

        private void updateStocksToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DataTable dt = sls.ViewStock(2);
            form2 = new updateStock(sls, 1);
            form2.MdiParent = this;
            form2.dataGridView1.DataSource = dt;
            form2.Show();
        }

        private void viewStocksToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DataTable dt = sls.ViewStock(3);
            form = new viewStock();
            form.MdiParent = this;
            form.dataGridView1.DataSource = dt;
            form.Show();
        }

        private void updateStocksToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DataTable dt = sls.ViewStock(3);
            form2 = new updateStock(sls, 1);
            form2.MdiParent = this;
            form2.dataGridView1.DataSource = dt;
            form2.Show();
        }

        private void seeListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = sls.ViewStaff(3,kode);
            form3 = new viewStaff();
            form3.MdiParent = this;
            form3.dataGridView1.DataSource = dt;
            form3.Show();
        }

        private void modifyGuidesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = sls.ViewStaff(3, kode);
            form4 = new updateStaff(sls);
            form4.MdiParent = this;
            form4.dataGridView1.DataSource = dt;
            form4.Show();
        }

        private void jobConfirmationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = sls.CreateReport1(1, kode, 1, ref ds);
            form6 = new reportMaker(rprt, ds, 1, sls);
            form6.MdiParent = this;
            form6.Show();
        }

        private void jobAssignmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = sls.CreateReport1(1, kode, 2, ref ds);
            form6 = new reportMaker(rprt, ds, 1, sls);
            form6.MdiParent = this;
            form6.Show();
        }

        private void sentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = sls.ViewReport(5);
            form5 = new viewReport(dt, sls);
            form5.MdiParent = this;
            form5.Show();
        }

        private void receivedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = sls.ViewReport(0);
            form5 = new viewReport(dt, sls);
            form5.MdiParent = this;
            form5.Show();
        }

        private void stockReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = sls.CreateReport6(6, kode, 3, ref ds);
            form6 = new reportMaker(rprt, ds, 7, sls);
            form6.MdiParent = this;
            form6.Show();
        }

        private void salesReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = sls.CreateReport7(7, kode, 6, ref ds);
            form6 = new reportMaker(rprt, ds, 6, sls);
            form6.MdiParent = this;
            form6.Show();
        }

        private void newProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form10 = new customReport(1, sls, this);
            form10.MdiParent = this;
            form10.Show();
        }

        private void allTourPackagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = sls.ViewGuides(kode, 3);
            form3 = new viewStaff();
            form3.MdiParent = this;
            form3.dataGridView1.DataSource = dt;
            form3.Show();
        }

        private void unfilledTourPackagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = sls.ViewGuides(kode, 1);
            form3 = new viewStaff();
            form3.MdiParent = this;
            form3.dataGridView1.DataSource = dt;
            form3.Show();
        }

        private void filledTourPackagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = sls.ViewGuides(kode, 2);
            form3 = new viewStaff();
            form3.MdiParent = this;
            form3.dataGridView1.DataSource = dt;
            form3.Show();
        }

        private void seeAllCustomersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = sls.ViewCustomer();
            form7 = new viewCustomer(dt);
            form7.MdiParent = this;
            form7.dataGridView1.DataSource = dt;
            form7.Show();
        }

        private void assignGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = sls.ViewGuides(kode, 1);
            form11 = new assignGuide(sls);
            form11.MdiParent = this;
            form11.dataGridView1.DataSource = dt;
            form11.Show();
        }

        private void transactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form12 = new Transaction(sls);
            form12.MdiParent = this;
            form12.Show();
        }
    }
}
