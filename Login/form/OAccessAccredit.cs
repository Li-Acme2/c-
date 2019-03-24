using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class AccessAccredit : Form
    {
        public AccessAccredit()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new AccessManage().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
