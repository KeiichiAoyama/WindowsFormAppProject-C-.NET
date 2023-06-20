using CrystalDecisions.CrystalReports.Engine;
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
    public partial class Transaction : Form
    {
        employee emp;
        int code = 0;
        string column = "productID";

        public Transaction(employee emp)
        {
            InitializeComponent();
            this.emp = emp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (emp is sales sls) {
                int index = comboBox1.SelectedIndex;
                DataTable dt = new DataTable();

                switch (index)
                {
                    case -1:
                        return;
                    case 0:
                        dt = sls.ViewStock(1, 1);
                        code = 1;
                        break;
                    case 1:
                        dt = sls.ViewStock(1, 2);
                        code = 1;
                        break;
                    case 2:
                        dt = sls.ViewStock(1, 3);
                        code = 1;
                        break;
                    case 3:
                        dt = sls.ViewStock(2);
                        code = 2;
                        break;
                    case 4:
                        dt = sls.ViewStock(3);
                        code = 3;
                        break;
                }
                dataGridView1.DataSource = dt;
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                DataGridViewCell selectedCell = selectedRow.Cells[column];
                string cellValue = selectedCell.Value.ToString();

                trasactionChildForm form = new trasactionChildForm(emp, code, cellValue);
                form.Show();
            }
        }
    }
}
