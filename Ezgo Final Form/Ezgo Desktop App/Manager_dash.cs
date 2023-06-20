using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrystalDecisions.Shared;
using System.Windows.Forms;

namespace Ezgo_Desktop_App
{
    public partial class Manager : Form
    {
        manager employee;
        viewStock form;
        updateStock form2;
        viewStaff form3;
        updateStaff form4;
        viewReport form5;
        reportMaker form6;
        viewCustomer form7;
        viewVendor form8;
        updateVendor form9;


        public Manager(employee employeeGet)
        {
            InitializeComponent();
            employee = new manager(employeeGet);
        }

        private void viewStocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewStock(1,1);
            form = new viewStock();
            form.MdiParent = this;
            form.dataGridView1.DataSource = dt;
            form.Show();
        }

        private void viewStocksToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewStock(1,2);
            form = new viewStock();
            form.MdiParent = this;
            form.dataGridView1.DataSource = dt;
            form.Show();
        }

        private void viewStocksToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewStock(1,3);
            form = new viewStock();
            form.MdiParent = this;
            form.dataGridView1.DataSource = dt;
            form.Show();
        }

        private void viewStocksToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewStock(2);
            form = new viewStock();
            form.MdiParent = this;
            form.dataGridView1.DataSource = dt;
            form.Show();
        }

        private void viewStocksToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewStock(3);
            form = new viewStock();
            form.MdiParent = this;
            form.dataGridView1.DataSource = dt;
            form.Show();
        }

        private void updateStocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewStock(1,1);
            form2 = new updateStock(employee, 1);
            form2.MdiParent = this;
            form2.dataGridView1.DataSource = dt;
            form2.Show();
        }

        private void updateStocksToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewStock(1,2);
            form2 = new updateStock(employee,1);
            form2.MdiParent = this;
            form2.dataGridView1.DataSource = dt;
            form2.Show();
        }

        private void updateStocksToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewStock(1,3);
            form2 = new updateStock(employee, 1);
            form2.MdiParent = this;
            form2.dataGridView1.DataSource = dt;
            form2.Show();
        }

        private void updateStocksToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewStock(2);
            form2 = new updateStock(employee, 2);
            form2.MdiParent = this;
            form2.dataGridView1.DataSource = dt;
            form2.Show();
        }

        private void updateStocksToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewStock(3);
            form2 = new updateStock(employee, 3);
            form2.MdiParent = this;
            form2.dataGridView1.DataSource = dt;
            form2.Show();
        }

        private void seeStaffListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewStaff(2);
            form3 = new viewStaff();
            form3.MdiParent = this;
            form3.dataGridView1.DataSource = dt;
            form3.Show();
        }

        private void seeListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewStaff(2,1);
            form3 = new viewStaff();
            form3.MdiParent = this;
            form3.dataGridView1.DataSource = dt;
            form3.Show();
        }

        private void seeListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewStaff(2, 2);
            form3 = new viewStaff();
            form3.MdiParent = this;
            form3.dataGridView1.DataSource = dt;
            form3.Show();
        }

        private void seeListToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewStaff(2, 3);
            form3 = new viewStaff();
            form3.MdiParent = this;
            form3.dataGridView1.DataSource = dt;
            form3.Show();
        }

        private void seeListToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewStaff(2, 4);
            form3 = new viewStaff();
            form3.MdiParent = this;
            form3.dataGridView1.DataSource = dt;
            form3.Show();
        }

        private void modifyStaffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewStaff(2,1);
            form4 = new updateStaff(employee);
            form4.MdiParent = this;
            form4.dataGridView1.DataSource = dt;
            form4.Show();
        }

        private void modifyStaffToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewStaff(2, 2);
            form4 = new updateStaff(employee);
            form4.MdiParent = this;
            form4.dataGridView1.DataSource = dt;
            form4.Show();
        }

        private void modifyStaffToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewStaff(2, 3);
            form4 = new updateStaff(employee);
            form4.MdiParent = this;
            form4.dataGridView1.DataSource = dt;
            form4.Show();
        }

        private void modifyStaffToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewStaff(2, 4);
            form4 = new updateStaff(employee);
            form4.MdiParent = this;
            form4.dataGridView1.DataSource = dt;
            form4.Show();
        }

        private void jakartaToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewReport(1);
            form5 = new viewReport(dt, employee);
            form5.MdiParent = this;
            form5.Show();
        }

        private void bandungToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewReport(2);
            form5 = new viewReport(dt, employee);
            form5.MdiParent = this;
            form5.Show();
        }

        private void baliToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewReport(3);
            form5 = new viewReport(dt, employee);
            form5.MdiParent = this;
            form5.Show();
        }

        private void surabayaToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewReport(4);
            form5 = new viewReport(dt, employee);
            form5.MdiParent = this;
            form5.Show();   
        }

        private void getGroupMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport1(1, 1, 1, ref ds);
            form6 = new reportMaker(rprt, ds, 1, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void ticketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport2(2, 1, 2, ref ds);
            form6 = new reportMaker(rprt, ds, 2, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void hotelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport3(3, 1, 2, ref ds);
            form6 = new reportMaker(rprt, ds, 3, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void tourPackageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport1(1, 1, 2, ref ds);
            form6 = new reportMaker(rprt, ds, 1, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void marketVendorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport4(4, 0, 3, ref ds);
            form6 = new reportMaker(rprt, ds, 4, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void findNewProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport5(5, 0, 4, ref ds);
            form6 = new reportMaker(rprt, ds, 5, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void findTourGroupMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport1(1, 2, 1, ref ds);
            form6 = new reportMaker(rprt, ds, 1, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void ticketsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport2(2, 2, 2, ref ds);
            form6 = new reportMaker(rprt, ds, 2, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void hotelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport3(3, 2, 2, ref ds);
            form6 = new reportMaker(rprt, ds, 3, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void tourPackageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport1(1, 2, 2, ref ds);
            form6 = new reportMaker(rprt, ds, 1, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void marketVendorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport4(4, 0, 3, ref ds);
            form6 = new reportMaker(rprt, ds, 4, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void findNewProductToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport5(5, 0, 4, ref ds);
            form6 = new reportMaker(rprt, ds, 5, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void makeTourGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport1(1, 4, 1, ref ds);
            form6 = new reportMaker(rprt, ds, 1, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void ticketsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport2(2, 4, 2, ref ds);
            form6 = new reportMaker(rprt, ds, 2, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void hotelToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport3(3, 4, 2, ref ds);
            form6 = new reportMaker(rprt, ds, 3, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void tourPackageToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport1(1, 4, 2, ref ds);
            form6 = new reportMaker(rprt, ds, 1, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void marketVendorToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport4(4, 0, 3, ref ds);
            form6 = new reportMaker(rprt, ds, 4, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void findNewProductToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport5(5, 0, 4, ref ds);
            form6 = new reportMaker(rprt, ds, 5, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void makeTourGroupToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport1(1, 3, 1, ref ds);
            form6 = new reportMaker(rprt, ds, 1, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void ticketsToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport2(2, 3, 2, ref ds);
            form6 = new reportMaker(rprt, ds, 2, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void hotelToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport3(3, 3, 2, ref ds);
            form6 = new reportMaker(rprt, ds, 3, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void tourPackageToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport1(1, 3, 2, ref ds);
            form6 = new reportMaker(rprt, ds, 1, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void marketVendorToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport4(4, 0, 3, ref ds);
            form6 = new reportMaker(rprt, ds, 4, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void findNewProductToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport5(5, 0, 4, ref ds);
            form6 = new reportMaker(rprt, ds, 5, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewReport(5);
            form5 = new viewReport(dt, employee);
            form5.MdiParent = this;
            form5.Show();
        }

        private void receivedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewReport(0);
            form5 = new viewReport(dt, employee);
            form5.MdiParent = this;
            form5.Show();
        }

        private void stockReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport6(6, 0, 5, ref ds);
            form6 = new reportMaker(rprt, ds, 7, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void salesReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ReportDocument rprt = employee.CreateReport7(7, 0, 6, ref ds);
            form6 = new reportMaker(rprt, ds, 6, employee);
            form6.MdiParent = this;
            form6.Show();
        }

        private void seeAllCustomersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewCustomer(0);
            form7 = new viewCustomer(dt);
            form7.MdiParent = this;
            form7.dataGridView1.DataSource = dt;
            form7.Show();
        }

        private void jakartaToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewCustomer(1);
            form7 = new viewCustomer(dt);
            form7.MdiParent = this;
            form7.dataGridView1.DataSource = dt;
            form7.Show();

        }

        private void bandungToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewCustomer(2);
            form7 = new viewCustomer(dt);
            form7.MdiParent = this;
            form7.dataGridView1.DataSource = dt;
            form7.Show();
        }

        private void baliToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewCustomer(3);
            form7 = new viewCustomer(dt);
            form7.MdiParent = this;
            form7.dataGridView1.DataSource = dt;
            form7.Show();
        }

        private void surabayaToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewCustomer(4);
            form7 = new viewCustomer(dt);
            form7.MdiParent = this;
            form7.dataGridView1.DataSource = dt;
            form7.Show();
        }

        private void seeListToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewVendor();
            form8 = new viewVendor();
            form8.MdiParent = this;
            form8.dataGridView1.DataSource = dt;
            form8.Show();
        }

        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = employee.ViewVendor();
            form9 = new updateVendor(employee);
            form9.MdiParent = this;
            form9.dataGridView1.DataSource = dt;
            form9.Show();
        }
    }
}
