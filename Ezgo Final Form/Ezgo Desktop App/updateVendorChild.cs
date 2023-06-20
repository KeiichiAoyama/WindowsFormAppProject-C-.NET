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
    public partial class updateVendorChild : Form
    {
        employee emp;
        Methods mtd = new Methods();

        public updateVendorChild(employee emp)
        {
            InitializeComponent();
            this.emp = emp;
        }

        public void setData(DataGridViewRow dgv) {
            textBox10.Text = dgv.Cells["vendorID"].Value.ToString();
            textBox1.Text = dgv.Cells["vName"].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Label[] label = { label1 };
            TextBox[] obj = { textBox1 };
            string[] keys = { label14.Text };
            string[] values = { textBox10.Text };

            if (mtd.Update(label, obj, keys, values, 5, emp))
            {
                MessageBox.Show("Vendor Succesfully Updated.");
                this.Close();
            }
            else {
                MessageBox.Show("Vendor Failed to be Updated.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] str = { label14.Text };
            string[] obj = { textBox10.Text };
            if (emp is manager mng) {
                if (mng.DeleteVendor(str, obj))
                {
                    MessageBox.Show("Vendor Succesfully Deleted.");
                    this.Close();
                }
                MessageBox.Show("Vendor Failed to be Deleted.");
            }       
        }
    }
}
