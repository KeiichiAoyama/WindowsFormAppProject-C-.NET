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
    public partial class updateVendor : Form
    {
        employee emp;
        Methods mtd = new Methods();

        public updateVendor(employee emp)
        {
            InitializeComponent();
            this.emp = emp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Label[] label = { label1, label14 };
            TextBox[] obj = { textBox1, textBox10 };

            if (mtd.Insert(label, obj, emp, 4))
            {
                MessageBox.Show("Vendor Succesfully Inserted.");
                foreach (TextBox tb in obj)
                {
                    tb.Text = null;
                }
                return;
            }
            MessageBox.Show("Vendor Failed to be Inserted.");
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                updateVendorChild uvc = new updateVendorChild(emp);
                uvc.setData(row);
                uvc.Show();
            }
        }
    }
}
