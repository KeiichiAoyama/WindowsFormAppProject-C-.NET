using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Label = System.Windows.Forms.Label;

namespace Ezgo_Desktop_App
{
    public partial class updateStaffChild : Form
    {
        employee emp;
        Methods mtd = new Methods();

        public updateStaffChild(employee emp)
        {
            InitializeComponent();
            this.emp = emp;
            string[] str1 = { "mng - manager", "sls - sales", "trg - tourguide" };
            string[] str2 = { "jkt - jakarta", "bdg - bandung", "sby - surabaya", "dps - denpasar" };
            comboBox4.Items.AddRange(str1);
            comboBox3.Items.AddRange(str2);
        }

        public void setData(DataGridViewRow row) { 
            textBox10.Text = row.Cells["employeeID"].Value.ToString();
            textBox1.Text = row.Cells["password"].Value.ToString();
            string role = row.Cells["roleID"].Value.ToString();
            for (int i = 0; i < comboBox4.Items.Count; i++)
            {
                string[] code = comboBox4.Items[i].ToString().Split(new[] { " - " }, StringSplitOptions.None);
                if (code[0] == role)
                {
                    comboBox4.SelectedIndex = i;
                    break;
                }
            }
            textBox9.Text = row.Cells["eName"].Value.ToString();
            string dest = row.Cells["destID"].Value.ToString();
            for (int i = 0; i < comboBox3.Items.Count; i++)
            {
                string[] code = comboBox3.Items[i].ToString().Split(new[] { " - " }, StringSplitOptions.None);
                if (code[0] == dest)
                {
                    comboBox3.SelectedIndex = i;
                    break;
                }
            }
            textBox8.Text = row.Cells["ePhone"].Value.ToString();
            dateTimePicker1.Value = (DateTime)row.Cells["eBirthDate"].Value;
            textBox6.Text = row.Cells["eSalary"].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Label[] labels = { label13, label12, label11, label10, label9, label8 };
            object[] inputs = { comboBox4, textBox9, comboBox3, textBox8, dateTimePicker1, textBox6 };
            string[] keys = { label14.Text };
            object[] values = { textBox10.Text };
            if (mtd.Update(labels, inputs, keys, values, 0, emp))
            {
                MessageBox.Show("Employee Updated Succesfully");
                this.Close();
            }
            else {
                MessageBox.Show("Employee Failed to be Updated.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (emp is manager mng)
            {
                string[] keys = { label14.Text };
                object[] values = { textBox10.Text };
                if (mng.DeleteStaff(0, keys, values))
                {
                    MessageBox.Show("Employee Has Been Deleted!");
                    this.Close();
                }
                else {
                    MessageBox.Show("Employee Failed to be Deleted.");
                }
            }
            else if (emp is sales sls) {
                string[] keys = { label14.Text };
                object[] values = { textBox10.Text };
                if (sls.DeleteStock(0, keys, values))
                {
                    MessageBox.Show("Employee Has Been Deleted!");
                    this.Close();
                }
                else {
                    MessageBox.Show("Employee Failed to be Deleted.");
                }
            }
        }
    }
}
