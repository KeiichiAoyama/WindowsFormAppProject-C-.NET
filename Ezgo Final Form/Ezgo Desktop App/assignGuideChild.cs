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
    public partial class assignGuideChild : Form
    {
        employee emp;
        Methods mtd = new Methods();
        string id = "";
        string pid = "";

        public assignGuideChild(employee emp)
        {
            InitializeComponent();
            this.emp = emp;
        }

        public void setData(DataGridViewRow row) { 
            id = row.Cells["tourID"].Value.ToString();
            pid = row.Cells["productID"].Value.ToString();
            DataTable dt = mtd.GetGuidesWhere(emp.branch);
            string[] eID = mtd.ArrayFromTable(dt, "employeeID");
            string[] eName = mtd.ArrayFromTable(dt, "eName");
            string[] list = mtd.CombineArrayTwo(eID, eName);
            comboBox7.Items.AddRange(list);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox7.SelectedIndex != -1) { 
                string[] arr = comboBox7.Items[comboBox7.SelectedIndex].ToString().Split(' ');
                string eID = arr[0];
                if (emp is sales sls) {
                    if (sls.AssignGuide(eID, id, pid)) {
                        MessageBox.Show("Guide Has Been Assigned");
                        return;
                    }
                }
                MessageBox.Show("Guide Failed To Be Assigned");
                return;
            }
            MessageBox.Show("Please Choose a Guide");
            return;
        }
    }
}
