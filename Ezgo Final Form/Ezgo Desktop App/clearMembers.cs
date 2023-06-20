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
    public partial class clearMembers : Form
    {
        Methods mtd = new Methods();
        DB DB = new DB();

        public clearMembers()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow Row = dataGridView1.Rows[e.RowIndex];
            string[] key = { "tgID" };
            string[] value = { Row.Cells["tgID"].Value.ToString() };
            Dictionary<string, object> parameter = mtd.ArrayMaker(key, value);
            if (DB.Delete("TourGroupMember", parameter) > 0)
            {
                MessageBox.Show("Members of the selected Tour Group has been deleted.");
                this.Close();
                return;
            }
            MessageBox.Show("Deletion Failed");
            return;
        }
    }
}
