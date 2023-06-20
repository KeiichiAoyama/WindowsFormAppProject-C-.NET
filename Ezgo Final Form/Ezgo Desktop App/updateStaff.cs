using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ezgo_Desktop_App
{
    public partial class updateStaff : Form
    {
        employee empl;
        DataTable dt;
        Methods mtd = new Methods();

        public updateStaff(employee employee)
        {
            InitializeComponent();
            empl = employee;
            string[] str1 = { "destID" };
            string[] str2 = { "jkt", "bdg", "sby", "dps" };

            string[] roles = mtd.RolesList();
            comboBox4.Items.AddRange(roles);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow[] selectedRows = dt.Select($"");
            DataTable newTable = selectedRows.CopyToDataTable();
            dataGridView1.DataSource = newTable;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SHA256 hash = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(textBox1.Text);
                byte[] hashBytes = hash.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                string hashed = builder.ToString();

                Label[] labels = { label14, label1, label13, label3, label12, label11, label10, label9, label8 };
                object[] objs = { textBox10, hashed, comboBox4, textBox2, textBox9, comboBox3, textBox8, dateTimePicker1, textBox6 };

                if (empl is employee)
                {
                    if (mtd.Insert(labels, objs, empl))
                    {
                        MessageBox.Show("New Employee Added Succesfully");
                        foreach (object obj in objs)
                        {
                            if (obj is TextBox)
                            {
                                ((TextBox)obj).Text = string.Empty;
                            }
                            else if (obj is DateTimePicker)
                            {
                                ((DateTimePicker)obj).Value = DateTime.Now;
                            }
                            else if (obj is ComboBox)
                            {
                                ((ComboBox)obj).SelectedIndex = -1;
                            }
                        }
                        return;
                    }
                }
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            updateStaffChild usd = new updateStaffChild(empl);
            usd.setData(dataGridView1.Rows[e.RowIndex]);
            usd.Show();
        }
    }
}
