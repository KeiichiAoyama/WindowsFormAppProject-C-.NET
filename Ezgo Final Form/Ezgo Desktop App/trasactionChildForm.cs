using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Ezgo_Desktop_App
{
    public partial class trasactionChildForm : Form
    {
        employee emp;
        int kode = 0;
        string id = "";
        Dictionary<string, object> item;
        int price = 0;
        string column = "";
        string key = "productID";

        public trasactionChildForm(employee emp, int kode, string id)
        {
            InitializeComponent();
            this.emp = emp;
            this.kode = kode;
            this.id = id;
            tabPage2.Enabled = false;

            if (emp is sales sls) {
                dataGridView1.DataSource = sls.ViewCustomer();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (emp is sales sls) {
                string[] lbl = { label1.Text, label2.Text, label3.Text };
                string[] tb = { textBox1.Text, BCryptNet.HashPassword(textBox2.Text), textBox3.Text };
                bool test = false;

                foreach (string tbs in tb)
                {
                    if (string.IsNullOrWhiteSpace(tbs))
                    {
                        test = false;
                        break;
                    }
                    test = true;
                }

                if (test == true)
                {
                    if (sls.InsertCustomer(lbl, tb)) {
                        tabPage2.Enabled = true;
                        item = sls.SelectItem(kode, id);

                        switch (kode) {
                            case 1:
                                price = int.Parse(item["tcSellingPrice"].ToString());
                                column = "tcAmount";
                                break;
                            case 2:
                                price = int.Parse(item["hPrice"].ToString());
                                column = "hAmount";
                                break;
                            case 3:
                                price = int.Parse(item["tpPrice"].ToString());
                                column = "tpSlot";
                                break;
                        }

                        textBox4.Text = textBox1.Text;
                        textBox5.Text = sls.branch;
                        textBox7.Text = id;
                    }
                    else
                    {
                        MessageBox.Show("Issue on inserting customer");
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int curr = int.Parse(label12.Text);
            int currPrice = int.Parse(label11.Text);
            curr++;
            currPrice += price;
            label11.Text = currPrice.ToString();
            label12.Text = curr.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int curr = int.Parse(label12.Text);
            int currPrice = int.Parse(label11.Text);

            if (curr != 0) {
                curr--;
                currPrice -= price;
                label11.Text = currPrice.ToString();
                label12.Text = curr.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (emp is sales sls) {
                string[] lbl1 = { label4.Text, label5.Text };
                string[] lbl2 = { "orderID", label7.Text, label8.Text, label9.Text };
                string[] tb1 = { textBox4.Text, textBox5.Text };

                if (sls.InsertOrders(lbl1, tb1, "Orders")) {
                    object[] tb2 = { sls.GetLastOrderID(), textBox7.Text, int.Parse(label12.Text), int.Parse(label11.Text) };
                    if (sls.InsertOrders(lbl2, tb2, "OrderDetails")){
                        string[] fields = {column};
                        object[] content = { (int.Parse(item[column].ToString()) - int.Parse(label12.Text)) };
                        MessageBox.Show((int.Parse(item[column].ToString()) - int.Parse(label12.Text)).ToString());
                        string[] key = {this.key};
                        string[] value = { textBox7.Text };
                        if (sls.UpdateStock(kode, fields, content, key, value)){
                            Random random = new Random();
                            int randomNumber = random.Next(1, 999999);
                            string filePath = $"C:\\Users\\DELL\\Documents\\VSCODE\\Ezgo Final Form\\Receipt\\{randomNumber}{textBox4.Text}.txt";
                           
                            using (FileStream fs = new FileStream(filePath, FileMode.Create))
                            {
                                using (StreamWriter writer = new StreamWriter(fs))
                                {
                                    writer.WriteLine("EZGO");
                                    writer.WriteLine("Rt 6 rw 9, jalan planet 69, Pondok Cina, Depok, Indonesia");
                                    writer.WriteLine("+62 813-8061-1660");
                                    writer.WriteLine("======================================================================================");
                                    writer.WriteLine($"Product ID: {textBox7.Text}");
                                    writer.WriteLine($"Total Price: Rp. {label11.Text}");
                                    writer.WriteLine($"Customer: {textBox4.Text}");
                                }
                            }

                            MessageBox.Show("Order has been inserted!");
                            this.Close();
                            return;
                        }
                    }
                }
                MessageBox.Show("Order failed to be inserted");
            }  
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0){
                if (emp is sales sls)
                {
                    tabPage2.Enabled = true;
                    item = sls.SelectItem(kode, id);

                    switch (kode)
                    {
                        case 1:
                            price = int.Parse(item["tcSellingPrice"].ToString());
                            column = "tcAmount";
                            break;
                        case 2:
                            price = int.Parse(item["hPrice"].ToString());
                            column = "hAmount";
                            break;
                        case 3:
                            price = int.Parse(item["tpPrice"].ToString());
                            column = "tpSlot";
                            break;
                    }

                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                    DataGridViewCell selectedCell = selectedRow.Cells["custID"];
                    string cellValue = selectedCell.Value.ToString();

                    textBox4.Text = cellValue;
                    textBox5.Text = sls.branch;
                    textBox7.Text = id;
                }
                else
                {
                    MessageBox.Show("Issue on selecting customer");
                }
            }
        }
    }
}
