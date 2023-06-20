using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ezgo_Desktop_App
{
    public partial class updateStock : Form
    {
        public int kode;
        employee empl;
        Methods mtd = new Methods();
        public string image;

        public updateStock(employee employee, int kode)
        {
            InitializeComponent();
            this.empl = employee;
            this.kode = kode;

            switch (kode) {
                case 1:
                    tabPage2.Enabled = true;   
                    tabPage3.Enabled = false;  
                    tabPage4.Enabled = false;  
                    break;
                case 2:
                    tabPage2.Enabled = false;
                    tabPage3.Enabled = true;
                    tabPage4.Enabled = false;
                    break;
                case 3:
                    tabPage2.Enabled = false;
                    tabPage3.Enabled = false;
                    tabPage4.Enabled = true;
                    break;
            }

            string[] vendors = mtd.VendorList();
            comboBox2.Items.AddRange(vendors);
            comboBox3.Items.AddRange(vendors);

            string[] empl = mtd.EmployeeList();
            comboBox7.Items.AddRange(empl);
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            updateStockChild form = new updateStockChild(empl);
            DataGridViewRow dgr = dataGridView1.Rows[e.RowIndex];
            form.kode = this.kode;
            form.setData(dgr);
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (image == null) {
                MessageBox.Show("Please pick an image!");
                return;
            }

            Label[] labels = {label1, label2, label43, label6, label3, label37, label4, label34, label5, label7, label8, label12, label11, label13, label38};
            object[] inputs = {textBox1, textBox2, textBox6, textBox3, dateTimePicker1, numericUpDown4, numericUpDown3, textBox5, textBox4, numericUpDown1, numericUpDown2, textBox7, textBox8, textBox10, comboBox1, comboBox2, image};

            if (empl is employee mng) {
                if (mtd.Insert(labels, inputs, mng, kode))
                {
                    MessageBox.Show("New Ticket Added Succesfully");
                    foreach (object input in inputs)
                    {
                        if (input is TextBox)
                        {
                            ((TextBox)input).Text = string.Empty;
                        }
                        else if (input is DateTimePicker)
                        {
                            ((DateTimePicker)input).Value = DateTime.Now;
                        }
                        else if (input is NumericUpDown)
                        {
                            ((NumericUpDown)input).Value = ((NumericUpDown)input).Minimum;
                        }
                        else if (input is ComboBox)
                        {
                            ((ComboBox)input).SelectedIndex = -1;
                        }
                        else if (ReferenceEquals(input, this.image))
                        {
                            this.image = null;
                        }
                    }
                    return;
                }
            }
            MessageBox.Show("New Ticket Failed to be Added.");
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                MessageBox.Show("Please pick an image!");
                return;
            }

            Label[] labels = { label26, label25, label21, label24, label23, label44, label22, label20, label19, label15, label14, label39 };
            object[] inputs = { textBox20, textBox19, textBox18, textBox17, textBox16, dateTimePicker3, textBox15, textBox14, textBox13, comboBox4, comboBox3, image };

            if (mtd.Insert(labels, inputs, empl, kode))
            {
                MessageBox.Show("New Hotel Added Succesfully");
                foreach (object input in inputs)
                {
                    if (input is TextBox)
                    {
                        ((TextBox)input).Text = string.Empty;
                    }
                    else if (input is ComboBox)
                    {
                        ((ComboBox)input).SelectedIndex = -1;
                    }
                    else if (ReferenceEquals(input, this.image))
                    {
                        this.image = null;
                    }
                }
                return;
            }

            MessageBox.Show("New Hotel Failed to be Added.");
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
                labels = new Label[] { label33, label32, label28, label31, label42, label30, label29, label27, label18, label17, label16, label45 };
                inputs = new object[] { textBox26, textBox25, textBox24, dateTimePicker2, numericUpDown6, numericUpDown5, comboBox7, textBox21, textBox12, textBox11, comboBox6, image, label46.Text };
            }
            else
            {
                labels = new Label[] { label33, label32, label28, label31, label42, label29, label27, label18, label17, label16, label45 };
                inputs = new object[] { textBox26, textBox25, textBox24, dateTimePicker2, numericUpDown6, numericUpDown5, textBox21, textBox12, textBox11, comboBox6, image, label46.Text };
            }

            if (mtd.Insert(labels, inputs, empl, kode))
            {
                MessageBox.Show("New Tour Package Added Succesfully");
                foreach (object input in inputs)
                {
                    if (input is TextBox)
                    {
                        ((TextBox)input).Text = string.Empty;
                    }
                    else if (input is DateTimePicker)
                    {
                        ((DateTimePicker)input).Value = DateTime.Now;
                    }
                    else if (input is NumericUpDown)
                    {
                        ((NumericUpDown)input).Value = ((NumericUpDown)input).Minimum;
                    }
                    else if (input is ComboBox)
                    {
                        ((ComboBox)input).SelectedIndex = -1;
                    }
                    else if (ReferenceEquals(input, this.image))
                    {
                        this.image = null;
                    }
                }
                return;
            }

            MessageBox.Show("New Tour Package Failed to be Added.");
        }
    }
}
