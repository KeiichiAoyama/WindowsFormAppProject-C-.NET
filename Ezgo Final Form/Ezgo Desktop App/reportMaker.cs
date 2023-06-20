using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.IO;
using CrystalDecisions.Shared;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Ezgo_Desktop_App
{
    public partial class reportMaker : Form
    {
        ReportDocument report = new ReportDocument();
        Methods mtd = new Methods();
        employee emp;
        int kode = 0;
        string saveFolderPath = @"C:\Users\DELL\Documents\VSCODE\Ezgo Final Form\Ezgo Desktop App\reports\";
        DataTable dt = new DataTable();
        string rptName = "";
        bool saved = false;
        string sort = "";
        bool sent = false;

        public reportMaker(ReportDocument rpt, DataTable ds, int kode, employee emp)
        {
            InitializeComponent();
            report = rpt;
            this.kode = kode;
            crystalReportViewer1.ReportSource = report;
            this.dt = ds;

            string[] str1 = new string[0];
            string[] str2 = new string[0];

            switch (this.kode)
            {
                case 1:
                    str1 = mtd.ArrayFromTable(ds, "tourID");
                    str2 = mtd.ArrayFromTable(ds, "tpName");
                    sort = "Tour.tourID";
                    break;
                case 2:
                    str1 = mtd.ArrayFromTable(ds, "ticketID");
                    str2 = mtd.ArrayFromTable(ds, "tcName");
                    sort = "Tickets.ticketID";
                    break;
                case 3:
                    str1 = mtd.ArrayFromTable(ds, "hotelID");
                    str2 = mtd.ArrayFromTable(ds, "hName");
                    sort = "Hotel.hotelID";
                    break;
                case 4:
                    str1 = mtd.ArrayFromTable(ds, "vendorID");
                    str2 = mtd.ArrayFromTable(ds, "vName");
                    sort = "Vendor.vendorID";
                    break;
                case 5:
                    str1 = mtd.ArrayFromTable(ds, "keyID");
                    str2 = mtd.ArrayFromTable(ds, "kProductType");
                    sort = "ProductKey.keyID";
                    break;
                case 6:
                    str1 = mtd.ArrayFromTable(ds, "orderID");
                    str2 = new string[str1.Length];

                    for (int i = 0; i < str1.Length; i++)
                    {
                        str2[i] = $"{i}.";
                    }
                    sort = "Orders.orderID";
                    break;
                case 7:
                    str1 = mtd.ArrayFromTable(ds, "productID");
                    str2 = new string[str1.Length];

                    for (int i = 0; i < str1.Length; i++)
                    {
                        str2[i] = $"{i}.";
                    }
                    sort = "Products.productID";
                    break;
                case 8:
                    str1 = mtd.ArrayFromTable(ds, "tgID");
                    str2 = mtd.ArrayFromTable(ds, "tourID");
                    sort = "TourGroup.tgID";
                    break;
            }

            comboBox1.Items.AddRange(mtd.CombineArrayTwo(str1, str2));
            this.emp = emp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                string str = comboBox1.SelectedItem.ToString();
                string[] arr = str.Split(new string[] { " - " }, StringSplitOptions.None);
                string val = arr[0];

                crystalReportViewer1.SelectionFormula = "{" + sort + "} = '" + val + "'";

                report.Refresh();
                crystalReportViewer1.ReportSource = report;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = "";
            string[] names = new string[0];

            if (saved != true)
            {
                name = Interaction.InputBox("Please Name Your Report", "Save", "");
                names = mtd.ArrayFromTable(mtd.GetColumn("rName", "Reports"), "rName");
            }

            bool check = true;

            do
            {
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Please name your file!");
                    name = Interaction.InputBox("Please Name Your Report", "Save", "");
                    check = false;
                }
                else if (names.Contains(name))
                {
                    MessageBox.Show("Name already exists");
                    name = Interaction.InputBox("Please Name Your Report", "Save", "");
                    check = false;
                }
                else
                {
                    check = true;
                }
            } while (check == false);

            rptName = name;
            if (saved != true) {
                saveFolderPath += $"{name}.txt";
            }

            try
            {
                report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Text, saveFolderPath);
                MessageBox.Show("Report Saved as TXT");
            }
            catch (Exception)
            {
                MessageBox.Show("File failed to be saved");
            }

            string[] keys = { "rName", "rAddress", "statusID", "roleID", "destID", "writerEmployeeID" };
            object[] values = { name, saveFolderPath, "1", emp.key, emp.branch, emp.id };

            if (saved == true) {
                MessageBox.Show("Report Saved");
                return;
            }

            if (emp.InsertReport(keys, values))
            {
                MessageBox.Show("Report Saved");
                saved = true;
                button3.Enabled = true;
                return;
            }

            MessageBox.Show("File failed to be saved");         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string name = Interaction.InputBox("Recipient's ID", "Send", "");
            string[] names = mtd.ArrayFromTable(mtd.GetColumn("employeeID", "Employee"), "employeeID");

            bool check = true;

            do
            {
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Please enter an ID!");
                    name = Interaction.InputBox("Recipient", "Send", "");
                    check = false;
                }
                else if (!names.Contains(name))
                {
                    MessageBox.Show("ID does not exist");
                    name = Interaction.InputBox("Recipient", "Send", "");
                    check = false;
                }
                else
                {
                    check = true;
                }
            } while (check == false);

            string[] field = { "recipientEmployeeID" };
            object[] values = { name };
            string[] keys = { "rName" };
            object[] objects = { rptName };

            if (emp.UpdateReport(field, values, keys, objects))
            {
                MessageBox.Show("File has been sent");
                sent = true;
                this.Close();
            }
            else {
                MessageBox.Show("File failed to be sent");
            }
        }

        private void reportMaker_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sent == false) {
                DialogResult result = MessageBox.Show("Are you sure you want to quit? Your report will be gone!", "Exit", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (saved == true)
                    {
                        File.Delete(saveFolderPath);
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
