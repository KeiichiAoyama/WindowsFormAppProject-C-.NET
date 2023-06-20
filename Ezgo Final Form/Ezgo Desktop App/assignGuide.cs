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
    public partial class assignGuide : Form
    {
        employee empl;

        public assignGuide(employee employee)
        {
            InitializeComponent();
            empl = employee;
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
           assignGuideChild agc = new assignGuideChild(empl);
           agc.setData(dataGridView1.Rows[e.RowIndex]);
            agc.Show();
        }
    }
}
