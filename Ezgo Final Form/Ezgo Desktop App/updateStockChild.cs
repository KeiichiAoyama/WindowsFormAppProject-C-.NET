using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ezgo_Desktop_App
{
    public partial class updateStockChild : Form
    {
        employee empl;
        Methods mtd = new Methods();
        public string image;
        public int kode;

        public updateStockChild(employee employee)
        {
            InitializeComponent();
            this.empl = employee;

            switch (kode)
            {
                case 1:
                    tabPage1.Enabled = true;
                    tabPage2.Enabled = false;
                    tabPage3.Enabled = false;
                    break;
                case 2:
                    tabPage1.Enabled = false;
                    tabPage2.Enabled = true;
                    tabPage3.Enabled = false;
                    break;
                case 3:
                    tabPage1.Enabled = false;
                    tabPage2.Enabled = false;
                    tabPage3.Enabled = true;
                    break;
            }

            string[] vendors = mtd.VendorList();
            comboBox2.Items.AddRange(vendors);
            comboBox3.Items.AddRange(vendors);
        }

        public void setData(DataGridViewRow dgv) {
            switch (this.kode) {
                case 1:
                    textBox1.Text = dgv.Cells["productID"].Value.ToString();
                    textBox2.Text = dgv.Cells["ticketID"].Value.ToString();
                    textBox6.Text = dgv.Cells["tcType"].Value.ToString();
                    textBox3.Text = dgv.Cells["tcName"].Value.ToString();
                    dateTimePicker1.Value = (DateTime)dgv.Cells["tcDate"].Value;
                    TimeSpan ts1 = (TimeSpan)dgv.Cells["tcDeparture"].Value;
                    int hrs1 = ts1.Hours;
                    int mnt1 = ts1.Minutes;
                    numericUpDown4.Value = hrs1;
                    numericUpDown3.Value = mnt1;
                    textBox5.Text = dgv.Cells["tcFrom"].Value.ToString();
                    textBox4.Text = dgv.Cells["tcDestination"].Value.ToString();
                    string ttime = dgv.Cells["tcTravelTime"].Value.ToString();
                    MessageBox.Show(ttime);
                    string[] arr = ttime.Split('.');
                    numericUpDown1.Value = int.Parse(arr[0]);
                    numericUpDown2.Value = int.Parse(arr[1]);
                    textBox7.Text = dgv.Cells["tcSellingPrice"].Value.ToString();
                    textBox8.Text = dgv.Cells["tcBuyingPrice"].Value.ToString();
                    textBox10.Text = dgv.Cells["tcAmount"].Value.ToString();
                    string dest1 = dgv.Cells["destID"].Value.ToString();
                    for (int i = 0; i < comboBox1.Items.Count; i++)
                    {
                        string[] code = comboBox1.Items[i].ToString().Split(new[] { " - " }, StringSplitOptions.None);
                        if (code[0] == dest1)
                        {
                            comboBox1.SelectedIndex = i;
                            break;
                        }
                    }
                    string vend1 = dgv.Cells["vendorID"].Value.ToString();
                    for (int i = 0; i < comboBox2.Items.Count; i++)
                    {
                        string[] code = comboBox2.Items[i].ToString().Split(new[] { " - " }, StringSplitOptions.None);
                        if (code[0] == vend1)
                        {
                            comboBox2.SelectedIndex = i;
                            break;
                        }
                    }
                    this.image = dgv.Cells["tcImage"].Value.ToString();
                    label44.Text = this.image;

                    break;
                case 2:
                    textBox20.Text = dgv.Cells["productID"].Value.ToString();
                    textBox19.Text = dgv.Cells["hotelID"].Value.ToString();
                    textBox18.Text = dgv.Cells["hName"].Value.ToString();
                    textBox17.Text = dgv.Cells["hAddress"].Value.ToString();
                    textBox16.Text = dgv.Cells["hRoomType"].Value.ToString();
                    dateTimePicker3.Value = (DateTime)dgv.Cells["hDate"].Value;
                    textBox15.Text = dgv.Cells["hPrice"].Value.ToString();
                    textBox14.Text = dgv.Cells["hSalesCut"].Value.ToString();
                    textBox13.Text = dgv.Cells["hAmount"].Value.ToString();
                    string dest2 = dgv.Cells["destID"].Value.ToString();
                    for (int i = 0; i < comboBox4.Items.Count; i++)
                    {
                        string[] code = comboBox4.Items[i].ToString().Split(new[] { " - " }, StringSplitOptions.None);
                        if (code[0] == dest2)
                        {
                            comboBox4.SelectedIndex = i;
                            break;
                        }
                    }
                    string vend2 = dgv.Cells["vendorID"].Value.ToString();
                    for (int i = 0; i < comboBox3.Items.Count; i++)
                    {
                        string[] code = comboBox3.Items[i].ToString().Split(new[] { " - " }, StringSplitOptions.None);
                        if (code[0] == vend2)
                        {
                            comboBox3.SelectedIndex = i;
                            break;
                        }
                    }
                    this.image = dgv.Cells["hImage"].Value.ToString();
                    label45.Text = this.image;

                    break;
                case 3:
                    textBox26.Text = dgv.Cells["productID"].Value.ToString();
                    textBox25.Text = dgv.Cells["tourID"].Value.ToString();
                    textBox24.Text = dgv.Cells["tpName"].Value.ToString();
                    dateTimePicker2.Value = (DateTime)dgv.Cells["tpDate"].Value;
                    TimeSpan ts2 = (TimeSpan)dgv.Cells["tpMeeting"].Value;
                    int hrs2 = ts2.Hours;
                    int mnt2 = ts2.Minutes;
                    numericUpDown6.Value = hrs2;
                    numericUpDown5.Value = mnt2;
                    string emp = dgv.Cells["employeeID"].Value.ToString();
                    for (int i = 0; i < comboBox7.Items.Count; i++)
                    {
                        string[] code = comboBox7.Items[i].ToString().Split(new[] { " - " }, StringSplitOptions.None);
                        if (code[0] == emp)
                        {
                            comboBox7.SelectedIndex = i;
                            break;
                        }
                    }
                    textBox21.Text = dgv.Cells["tpPrice"].Value.ToString();
                    textBox12.Text = dgv.Cells["tpSaleCut"].Value.ToString();
                    textBox11.Text = dgv.Cells["tpSlot"].Value.ToString();
                    string dest3 = dgv.Cells["destID"].Value.ToString();
                    for (int i = 0; i < comboBox6.Items.Count; i++)
                    {
                        string[] code = comboBox6.Items[i].ToString().Split(new[] { " - " }, StringSplitOptions.None);
                        if (code[0] == dest3)
                        {
                            comboBox6.SelectedIndex = i;
                            break;
                        }
                    }
                    this.image = dgv.Cells["tpImage"].Value.ToString();
                    label46.Text = this.image;

                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mtd.PickImage(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mtd.PickImage(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mtd.PickImage(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                MessageBox.Show("Please pick an image!");
                return;
            }

            Label[] labels = { label3, label37, label4, label34, label5, label7, label8, label12, label11, label13, label38 };
            object[] inputs = { dateTimePicker1, numericUpDown4, numericUpDown3, textBox5, textBox4, numericUpDown1, numericUpDown2, textBox7, textBox8, textBox10, comboBox1, comboBox2, image };
            string[] keys = { label2.Text };
            object[] values = { textBox2.Text };


            if (mtd.Update(labels, inputs, keys, values, kode, empl))
            {
                MessageBox.Show("Ticket Updated Succesfully");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ticket Failed to be Updated.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                MessageBox.Show("Please pick an image!");
                return;
            }

            Label[] labels = { label24, label23, label47, label22, label20, label19, label15, label14, label39 };
            object[] inputs = { textBox17, textBox16, dateTimePicker3, textBox15, textBox14, textBox13, comboBox4, comboBox3, image };
            string[] keys = { label25.Text };
            object[] values = { textBox19.Text };

            if (mtd.Update(labels, inputs, keys, values, kode, empl))
            {
                MessageBox.Show("Hotel Updated Succesfully");
                this.Close();
            }
            else {
                MessageBox.Show("Hotel Failed to be Updated.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                MessageBox.Show("Please pick an image!");
                return;
            }

            Label[] labels;
            object[] inputs;

            if (comboBox7.SelectedIndex != -1)
            {
                labels = new Label[] { label28, label31, label42, label30, label29, label27, label18, label17, label16 };
                inputs = new object[] { textBox24, dateTimePicker2, numericUpDown6, numericUpDown5, comboBox7, textBox21, textBox12, textBox11, comboBox6, image };
            }
            else
            {
                labels = new Label[] { label28, label31, label42, label29, label27, label18, label17, label16 };
                inputs = new object[] { textBox24, dateTimePicker2, numericUpDown6, numericUpDown5, textBox21, textBox12, textBox11, comboBox6, image };
            }

            string[] keys = { label32.Text };
            object[] values = { textBox25.Text };

            if (mtd.Update(labels, inputs, keys, values, kode, empl))
            {
                MessageBox.Show("Tour Package Updates Succesfully");
                this.Close();
            }
            else {
                MessageBox.Show("Tour Package Failed to be Updated.");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (empl is manager mng) {
                string[] keys = { label2.Text };
                string[] field = { label1.Text };
                object[] values = { textBox2.Text };
                object[] content = { textBox1.Text };
                if (mng.DeleteStock(this.kode, keys, values, field, content))
                {
                    MessageBox.Show("Ticket Has Been Deleted!");
                    this.Close();
                }
                else {
                    MessageBox.Show("Ticket Failed to be Deleted.");
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (empl is manager mng)
            {
                string[] keys = { label25.Text };
                object[] values = { textBox19.Text };
                string[] field = { label26.Text };
                object[] content = { textBox20.Text };
                if (mng.DeleteStock(this.kode, keys, values, field, content))
                {
                    MessageBox.Show("Hotel Has Been Deleted!");
                    this.Close();
                }
                else {
                    MessageBox.Show("Hotel Failed to be Deleted.");
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (empl is manager mng)
            {
                string[] keys = { label32.Text };
                object[] values = { textBox25.Text };
                string[] field = { label33.Text };
                object[] content = { textBox26.Text };
                if (mng.DeleteStock(this.kode, keys, values, field, content))
                {
                    MessageBox.Show("Tour Package Has Been Deleted!");
                    this.Close();
                }
                else {
                    MessageBox.Show("Tour Package Failed to be Deleted.");
                }
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void updateStockChild_Load(object sender, EventArgs e)
        {

        }
    }
}
