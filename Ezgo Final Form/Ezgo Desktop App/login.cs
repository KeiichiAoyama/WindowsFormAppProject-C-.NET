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
    public partial class login : Form
    {
        public employee employee = new employee();
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            string nik = textBox1.Text;
            string password = textBox2.Text;
            employee.login(nik, password);
        }
    }
}
